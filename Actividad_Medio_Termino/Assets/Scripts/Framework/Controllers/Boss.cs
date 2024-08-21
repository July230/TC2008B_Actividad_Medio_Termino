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
    public Transform[] attackPoints; // Puntos desde donde el jefe dispara
    public float attackInterval = 2.0f; // Tiempo entre cada serie de disparos
    public float timeBetweenShoots = 0.2f; // Tiempo entre disparos consecutivos
    public float moveSpeed = 1.0f; // Velocidad de movimiento del jefe

    private int currentPattern = 0; // Patron del ataque actual
    private float attackTimer;
    public float patternChangeInterval = 15.0f;
    public float patternPause = 2.0f;
    public float patternChangeTimer;

    // Parametros para mover los puntos de disparo
    private Vector3[] initialPositions; // Posiciones iniciales de los puntos de disparos
    public float attackPointRotation = 1000.0f; // Velocidad de rotacion para los puntos de disparos

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        attackTimer = attackInterval;
        patternChangeTimer = patternChangeInterval;

        // Guardar posiciones iniciales de los puntos de disparo del jefe
        initialPositions = new Vector3[attackPoints.Length];
        for(int i = 0; i < attackPoints.Length; i++)
        {
            // Calcular la posicion inicial con respecto al jefe
            initialPositions[i] = attackPoints[i].position - transform.position;
        }
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

        // Mover puntos de disparo en patron circular
        RotateAttackPoints();

        // Si hay tiempo, agregar logica para mover al jefe
        // MoveBoss();
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
        }
    }

    /// <summary>
    /// ChangeAttackPattern tiene la logica para cambiar de patrones de disparo
    /// </summary>
    private IEnumerator ChangeAttackPattern()
    {
        Debug.Log("Cambio de patron");

        // Pausa antes de cambiar de patron
        yield return new WaitForSeconds(patternPause);

        // Cambiar al siguiente patron
        currentPattern++;
        if(currentPattern >= 3)
        {
            currentPattern = 0;
        }
    }

    /// <summary>
    /// PatternOne tiene la logica para un patron de disparo
    /// </summary>
    private IEnumerator PatternOne()
    {
        // Patron de ataque 1: disparar desde diferentes puntos
        foreach (var point in attackPoints)
        {
            GameObject projectile = Instantiate(projectilePrefab, point.position, point.rotation);
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(point.forward); // Configurar la direccion del proyectil
            }
        }

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// PatternTwo tiene la logica para un patron de disparo
    /// </summary>
    private IEnumerator PatternTwo()
    {
        // Patron de ataque 1: disparar desde diferentes puntos
        foreach (var point in attackPoints)
        {
            GameObject projectile = Instantiate(projectilePrefab, point.position, point.rotation);
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                // Configurar la direccion del proyectil
                Vector3 spiralDirection = new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time), 0).normalized;
                bossProjectile.SetDirection(spiralDirection);
            }
        }

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// PatternThree tiene la logica para un patron de disparo en rafagas
    /// </summary>
    private IEnumerator PatternThree()
    {
        // Patron de ataque 1: disparar desde diferentes puntos
        foreach (var point in attackPoints)
        {
            GameObject projectile = Instantiate(projectilePrefab, point.position, point.rotation);
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(point.forward);
            }
            yield return new WaitForSeconds(timeBetweenShoots);
        }

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// MoveAttackPoints tiene la logica para la rotacion de los puntos de disparo alrededor del jefe
    /// </summary>
    private void RotateAttackPoints()
    {
        
        for (int i = 0; i < attackPoints.Length; i++)
        {
            float angle = attackPointRotation * Time.deltaTime;

            // Calcular la nueva posicion
            Vector3 offset = initialPositions[i];
            float x = offset.x * Mathf.Cos(angle) - offset.z * Mathf.Sin(angle);
            float z = offset.x * Mathf.Cos(angle) - offset.z * Mathf.Sin(angle);

            attackPoints[i].position = new Vector3(x, offset.y, z) + transform.position;
        }
    }

    /// <summary>
    /// MoveBoss tiene la logica del movimiento del jefe
    /// </summary>
    private void MoveBoss()
    {
        // Si hay tiempo, implementar logica para que jefe se mueva
    }
}
