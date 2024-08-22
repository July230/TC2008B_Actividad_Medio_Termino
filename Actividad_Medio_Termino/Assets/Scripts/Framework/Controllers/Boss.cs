using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase boss actualiza los eventos del objeto boss.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Boss : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // Puntos donde aparecerán los enemigos
    public Transform attackPoint; // Puntos desde donde el jefe dispara
    public float attackInterval = 1.0f; // Tiempo entre cada serie de disparos
    public float timeBetweenShoots = 0.2f; // Tiempo entre disparos consecutivos

    private int currentPattern = 0; // Patron del ataque actual
    private float attackTimer;
    public float patternChangeInterval = 10.0f;
    public float patternPause = 1.0f;
    public float patternChangeTimer;

    // Parametros para mover los puntos de disparo
    private Vector3[] initialPositions; // Posiciones iniciales de los puntos de disparos
    public float attackPointRotation = 1000.0f; // Velocidad de rotacion para los puntos de disparos

    public Health bossHealth;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        bossHealth = GetComponent<Health>();
        if(bossHealth == null)
        {
            Debug.Log("No se encontro el componente Health para el jefe");
        }
        attackTimer = attackInterval;
        patternChangeTimer = patternChangeInterval;

        // Iniciar la posicion del punto de ataque
        initialPositions = new Vector3[1];
        initialPositions[0] = attackPoint.position - transform.position;

        transform.position = new Vector3(0, 30, 0);
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        attackTimer -= Time.deltaTime;
        patternChangeTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            // Ejecutar patron de ataque
            StartCoroutine(ExecuteAttackPattern());
            attackTimer = attackInterval;
        }

        if(patternChangeTimer <= 0)
        {
            StartCoroutine(ChangeAttackPattern());
            patternChangeTimer = patternChangeInterval;
        }

    }

    /// <summary>
    /// ExecuteAttackPattern tiene la logica para determinar el patron de disparo
    /// Utilizamos IEnumerator para crear corrutinas y asi ejecutar codigo de forma asincrona
    /// Para manejar procesos que toman tiempo como los disparos sin bloquear el flujo del juego
    /// </summary>
    private IEnumerator ExecuteAttackPattern()
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
            case 2:
                yield return StartCoroutine(PatternThree());
                break;
            case 3: 
                yield return StartCoroutine(PatternFour());
                break;
            case 4: 
                yield return StartCoroutine(PatternFive());
                break;
        }
    }

    /// <summary>
    /// ChangeAttackPattern tiene la logica para cambiar de patrones de disparo de forma aleatoria
    /// </summary>
    private IEnumerator ChangeAttackPattern()
    {
        Debug.Log("Cambio de patron");

        // Pausa antes de cambiar de patron
        yield return new WaitForSeconds(patternPause);

        // Cambiar al siguiente patron
        int numberOfPatterns = 5;
        currentPattern = Random.Range(0, numberOfPatterns);
    }

    /// <summary>
    /// PatternOne tiene la logica para un patron de disparo en cruz
    /// </summary>
    private IEnumerator PatternOne()
    {
        attackInterval = 0.3f;
        int numberOfProjectiles = 4;
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

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// PatternTwo tiene la logica para disparar en espiral
    /// </summary>
    private IEnumerator PatternTwo()
    {
        attackInterval = 0.3f;
        int numberOfProjectiles = 6;
        float angleStep = 360f / numberOfProjectiles; 
        float spiralSpeed = 30.0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep + (Time.time * spiralSpeed);
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                // Configurar la direccion del proyectil
                bossProjectile.SetDirection(direction);
            }
            yield return new WaitForSeconds(timeBetweenShoots);
        }

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// PatternThree tiene la logica para un patron de disparo consecutivo
    /// </summary>
    private IEnumerator PatternThree()
    {
        attackInterval = 0.5f;
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

        yield return new WaitForSeconds(attackInterval);
    }

    /// <summary>
    /// PatternFive tiene la logica para un patron oscilante
    /// </summary>
    private IEnumerator PatternFour()
    {
        attackInterval = 0.5f;
        int numberOfProjectiles = 8;
        float angleStep = 360f / numberOfProjectiles;
        float osillationAmplitude = 15.0f; // Amplitud de la oscilacion en grados
        float osillationFrequency = 2.0f; // Frecuencia de la oscilacion

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            float osillationAngle = angle + Mathf.Sin(Time.time * osillationFrequency) * osillationAmplitude;
            Vector3 direction = new Vector3(Mathf.Cos(osillationAngle * Mathf.Deg2Rad), 0, Mathf.Sin(osillationAngle * Mathf.Deg2Rad)).normalized;

            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(direction);
            }
            yield return new WaitForSeconds(timeBetweenShoots);
        }

        yield return new WaitForSeconds(attackInterval);
    }

    /// <summary>
    /// PatternFive tiene la logica para un patron en rafagas 
    /// </summary>
    private IEnumerator PatternFive()
    {
        int numberOfProjectiles = 30;
        float angleStep = 360f / numberOfProjectiles; 
        attackInterval = 3.0f;

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

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// ReceiveDamage es llamado cada vez que el jefe es impactado por un proyectil
    /// </summary>
    private void ReceiveDamage(int damage)
    {
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
        }
    }
}
