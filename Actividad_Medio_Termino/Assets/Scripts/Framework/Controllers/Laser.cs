using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase Laser actualiza los eventos del objeto Laser.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Laser : MonoBehaviour
{
    // Velocidad movimiento y tiempo de vida del proyectil
    public float speed = 500.0f;
    public float lifetime = 5.0f;

    // Direccion del proyectil
    private Vector3 direction;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        // Destruir el proyectil despues de un tiempo
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        // Mover el proyectil hacia adelante
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
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
        direction = dir;
    }
}
