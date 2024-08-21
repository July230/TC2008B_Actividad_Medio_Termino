using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase bossprojectile actualiza los eventos del objeto Projectile
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class BossProjectile : MonoBehaviour
{

    // Velocidad movimiento y tiempo de vida del proyectil
    public float speed = 500.0f;
    public float lifetime = 60.0f;

    // Direccion del proyectil
    private Vector3 direction;

    public float patternSpeed = 500.0f; // Velocidad del patron
    public float patternDuration = 60.0f; // Duracion del patron

    private float patternTimer; // Temporizador para el patron
    
    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        // Destruir el proyectil despues de un tiempo
        Destroy(gameObject, lifetime);
        patternTimer = patternDuration; // Iniciar temporizador del patron
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        // Mover el proyectil hacia adelante
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Actulizar temporizador del patron
        patternTimer -= Time.deltaTime;

        if (patternTimer > 0)
        {
            ApplyPattern();
        }
    }
    /*
    /// <summary>
    /// OnCollisionEnter maneja la logica de las colisiones de laser
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // Ignorar colisiones con otros proyectiles, misiles y laseres
        if (collision.gameObject.CompareTag("Projectile") || 
            collision.gameObject.CompareTag("Laser") || 
            collision.gameObject.CompareTag("Missile"))
        {
            return;
        }

        Debug.Log("Proyectil impactó en el objetivo: " + collision.gameObject.name);
        // Si choca con un enemigo, destruir proyectil y enemigo
        Destroy(gameObject);
    }
    */
    /// <summary>
    /// Configura la direccion del laser
    /// </summary>
    public void SetDirection(Vector3 dir)
    {
        // Normalizar la direccion para mantener velocidad constante
        direction = dir.normalized;
    }

    /// <summary>
    /// ApplyPattern aplica un patron a los proyectiles
    /// </summary>
    private void ApplyPattern()
    {
        // Patron: Movimiento en espiral
        float angle = patternSpeed * Time.time;
        Vector3 patternDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // Movimiento en espiral
        
        transform.Translate(patternDirection * Time.deltaTime * speed);
    }
}
