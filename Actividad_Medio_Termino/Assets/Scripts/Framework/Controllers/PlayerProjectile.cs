using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase player controller class actualiza los eventos del objeto jetfighter.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Proyectile : MonoBehaviour
{
    // Velocidad movimiento y tiempo de vida del proyectil
    public float speed = 10.0f;
    public float lifetime = 5.0f;

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

    /// <summary>
    /// OnCollision maneja la logica de las colisiones de proyectiles
    /// </summary>
    private void OnCollision(Collision collision)
    {
        // Si choca con un enemigo, destruir proyectil y enemigo
        Destroy(gameObject);
    }
}
