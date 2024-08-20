using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Velocidad de la nave
    public float speed = 5.0f;

    // Start is llamado antes de la primera actualizacion del frame
    void Start()
    {
        
    }

    // Update es llamado una vez por frame
    void Update()
    {
        // Mover nave hacia adelante
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
