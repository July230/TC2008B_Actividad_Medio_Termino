using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase enemy actualiza los eventos de los enemigos.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Enemy : MonoBehaviour
{
    public float speed = 50.0f;
    public GameObject projectilePrefab;
    public Transform attackPoint;
    public int health = 100;
    public int currentHealth; 
    public float shootInterval = 1.5f;
    public float nextShootTime = 0f;
    private float timeBetweenShoots = 0.2f;
    private int currentPattern = 0;
    private float patternPause = 2.0f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Camera mainCamera;
    private float cameraWidth;
    private bool movingRight = true;


    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        currentHealth = health;
        mainCamera = Camera.main;
        CalculateLimit();
        StartCoroutine(MovePattern());
        StartCoroutine(ShottingPattern());
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        if(Time.time >= nextShootTime)
        {
            StartCoroutine(ShottingPattern());
            nextShootTime = Time.time + shootInterval;
        }
    }

    /// <summary>
    /// MovePattern es una corrutina que se encarga del movimiento del enemigo
    /// </summary>
    private IEnumerator MovePattern()
    {
        while (true)
        {
            // Mover al enemigo a lo largo del eje x
            transform.Translate(Vector3.right * speed * Time.deltaTime * (movingRight ? 1 : -1));

            // Comprobar si ha llegado al límite
            if(movingRight && transform.position.x >= endPosition.x)
            {
                movingRight = false; // Cambiar direccion hacia la izquierda
            }
            else if (movingRight && transform.position.x <= startPosition.x)
            {
                movingRight = true; // Cambiar direccion hacia la derecha
            }
            yield return null; // Esperar al proximo frame
        }
    }

    /// <summary>
    /// ShottingPattern tiene la logica para los patrones de los enemigos
    /// </summary>
    private IEnumerator ShottingPattern()
    {
        switch(currentPattern)
        {
            // Aqui van los patrones de ataque
            case 0:
                yield return StartCoroutine(PatternOne());
                break;
            case 1:
                yield return StartCoroutine(PatternTwo());
                break;
        }
        yield return StartCoroutine(ChangeAttackPattern());
    }

    /// <summary>
    /// ChangeAttackPattern tiene la logica para cambiar de patrones de disparo de forma aleatoria
    /// </summary>
    private IEnumerator ChangeAttackPattern()
    {
        // Pausa antes de cambiar de patron
        yield return new WaitForSeconds(patternPause);

        // Cambiar al siguiente patron
        int numberOfPatterns = 2;
        currentPattern = Random.Range(0, numberOfPatterns);
    }

    /// <summary>
    /// PatternOne tiene la logica para un patron en rafagas 
    /// </summary>
    private IEnumerator PatternOne()
    {
        shootInterval = 2.0f;
        int numberOfProjectiles = 30;
        float angleStep = 360f / numberOfProjectiles; 

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(direction); // Configurar la direccion del proyectil
            }
        }

        yield return new WaitForSeconds(shootInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// PatternTwo tiene la logica para un patron de disparo consecutivo
    /// </summary>
    private IEnumerator PatternTwo()
    {
        shootInterval = 0.5f;
        int numberOfProjectiles = 18;
        float angleStep = 360f / numberOfProjectiles; 

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(direction);
            }
            yield return new WaitForSeconds(timeBetweenShoots);
        }

        yield return new WaitForSeconds(shootInterval);
    }

     /// <summary>
    /// CalculateLimit obtiene los limites de la camara
    /// </summary>
    private void CalculateLimit()
    {
        // Obtener el tamaño de la camara en el mundo
        float cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;

        // Definir los limites en base a la camara
        startPosition = new Vector3(-cameraWidth / 2, transform.position.y, transform.position.z);
        endPosition = new Vector3(cameraWidth / 2, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// ReceiveDamage es llamado cada vez que el jefe es impactado por un proyectil
    /// </summary>
    private void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Die es llamado cuando el objeto llega a 0 de HP
    /// </summary>
    private void Die()
    {
        Debug.Log($"Enemigo {gameObject} destruido");
        Destroy(gameObject);
    }
}
