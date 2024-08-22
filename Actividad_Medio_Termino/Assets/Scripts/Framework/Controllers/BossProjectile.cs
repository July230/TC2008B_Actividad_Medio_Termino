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
    public float speed = 200.0f;

    // Direccion del proyectil
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
        // Mover el proyectil hacia adelante
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
    /// Configura la direccion del laser
    /// </summary>
    public void SetDirection(Vector3 dir)
    {
        // Normalizar la direccion para mantener velocidad constante
        direction = dir.normalized;
    }
    
    /// <summary>
    /// OnTriggerEnter maneja la logica de las colisiones del proyectil de enemigos y jefes
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Ignorar colisiones con otros proyectiles, misiles y laseres
        if (other.gameObject.CompareTag("Projectile") || 
            other.gameObject.CompareTag("Laser") || 
            other.gameObject.CompareTag("Missile") ||
            other.gameObject.CompareTag("Enemy") ||
            other.gameObject.CompareTag("Boss"))
        {
            return;
        }

        // Si el proyectil impacta contra un jugador
        if(other.gameObject.CompareTag("Player"))
        {
            Health healthPlayer = other.gameObject.GetComponent<Health>();
            if(healthPlayer != null)
            {
                healthPlayer.TakeDamage(1);
            }
        }
        // Debug.Log("Proyectil impactó en el objetivo: " + other.gameObject.name);
        // Destruir proyectil al colisionar
        Destroy(gameObject);
    }

}
