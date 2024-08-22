using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase Health actualiza los eventos para los puntos de vida de los objetos
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject explosionEffectPrefab;

    /// <summary>
    /// Start configura la vida maxima de los objetos
    /// </summary>
    void Start()
    {
        if(gameObject.CompareTag("Player"))
        {
            maxHealth = 50;
        }
        else if(gameObject.CompareTag("Boss"))
        {
            maxHealth = 1000;
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            maxHealth = 100;
        }
        currentHealth = maxHealth;
    }

    /// <summary>
    /// TakeDamage reduce la salud del objeto
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //Debug.Log($"{gameObject.tag} recibio {damageAmount} puntos de daño.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Die maneja la muerte del objeto cuando su vida llega a 0
    /// </summary>
    public void Die()
    {
        Debug.Log($"{gameObject.tag} ha muerto");
        // Instanciar el efecto de explosión
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        if (gameObject.CompareTag("Player"))
        {
            // Funcion para reiniciar el juego
            GameManager gameManager = FindObjectOfType<GameManager>();
            if(gameManager != null)
            {
                gameManager.RestartGame();
            }
            else
            {
                Debug.LogError("No se encontro GameManager en la escena");
            }
        }
        Destroy(gameObject);
    }
}
