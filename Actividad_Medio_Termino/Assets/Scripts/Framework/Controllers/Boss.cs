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
    public float attackInterval = 0.5f; // Tiempo entre cada disparo del jefe
    public float moveSpeed = 1.0f; // Velocidad de movimiento del jefe

    private int currentPattern = 0; // Patron del ataque actual
    private float attackTimer;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        attackTimer = attackInterval;
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0)
        {
            // Ejecutar patron de ataque
            StartCoroutine(ExecuteAttackPattern());
            attackTimer = attackInterval;
        }

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
            case 0:
                yield return StartCoroutine(PatternOne());
                break;
        }
    }

    /// <summary>
    /// PatternOne tiene la logica para un patron de disparo
    /// </summary>
    private IEnumerator PatternOne()
    {
        if (attackPoints.Length == 0)
        {
            Debug.LogWarning("No attack points assigned!");
            yield break; // Salir si no hay puntos de ataque asignados
        }

        // Patron de ataque 1: disparar desde diferentes puntos
        foreach (var point in attackPoints)
        {
            if(point != null)
            {
                Debug.Log($"Instantiating projectile at {point.position}");
                GameObject projectile = Instantiate(projectilePrefab, point.position, point.rotation);
                BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
                if(bossProjectile != null)
                {
                    bossProjectile.SetDirection(point.forward); // Configurar la direccion del proyectil
                }
                
            }
            else 
            {
                Debug.LogWarning("Attack point is null");
            }
        }

        yield return new WaitForSeconds(attackInterval); // Tiempo entre disparos
    }


    /// <summary>
    /// MoveBoss tiene la logica del movimiento del jefe
    /// </summary>
    private void MoveBoss()
    {
        // Si hay tiempo, implementar logica para que jefe se mueva
    }
}
