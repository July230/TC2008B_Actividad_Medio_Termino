using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase missile class actualiza los eventos del objeto missile.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Missile : MonoBehaviour
{
    // Velocidad y tiempo de vida del misil
    public float speed = 500.0f;

    // Direccion del misil
    private Vector3 direction;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
         
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        // Mover el misil hacia adelante
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    /// <summary>
    /// OnBecameInvisible es llamado cuando el proyectil sale de la camara
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Configura la direccion del misil
    /// </summary>
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    /*
    /// <summary>
    /// OnCollision maneja la logica de las colisiones del misil
    /// </summary>
    private void OnCollision(Collision collision)
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
}
