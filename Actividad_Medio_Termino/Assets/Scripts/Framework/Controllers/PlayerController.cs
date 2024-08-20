using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This player controller class will update the events from the vehicle player.
/// Standar coding documentarion can be found in 
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class PlayerController : MonoBehaviour
{
    // Velocidad movimiento y giro de la nave
    public float speed = 5.0f;
    public float turnSpeed = 0.0f;


    // Inputs de jugador
    public float horizontalInput;
    public float forwardInput;

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
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Ajuste de movimiento: // La nave debe moverse en torno al eje x y z
        transform.Translate(Vector3.down * Time.deltaTime * speed * forwardInput);

        // Rotar nave en torno al eje "z"
        transform.Rotate(Vector3.forward, Time.deltaTime * turnSpeed * horizontalInput);
    }
}
