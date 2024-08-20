using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase player controller class actualiza los eventos del objeto jetfighter.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class PlayerController : MonoBehaviour
{
    // Velocidad movimiento y giro de la nave
    public float speed = 10.0f;
    public float turnSpeed = 5.0f;

    // Inputs de jugador
    public float horizontalInput;
    public float forwardInput;

    // Variables globales para los proyectiles disparados
    public GameObject projectilePrefab;
    public Transform shootingPoint1; // Punto desde donde se dispara el proyectil
    public Transform shootingPoint2; // Punto desde donde se dispara el proyectil

    public float shootInterval = 0.25f; // Intervalo entre disparos
    public float nextShootTime = 0f;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        speed = 200;
        turnSpeed = 50;
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

        if (Input.GetMouseButtonDown(0) && Time.time > nextShootTime) // Click izquierdo para disparar
        {
            ShootLaser();
            nextShootTime = Time.deltaTime * shootInterval;
        }
    }

    private void ShootLaser()
    {
        if (projectilePrefab)
        {
            // Correcion de direccion de los proyectiles
            Vector3 shootDirection = Vector3.down;

            Debug.Log("Click izquierdo");

            // Disparar desde el primer punto de disparo
            GameObject projectile1 = Instantiate(projectilePrefab, shootingPoint1.position, shootingPoint1.rotation);
            Rigidbody rb1 = projectile1.GetComponent<Rigidbody>();
            rb1.velocity = shootDirection * speed;

            GameObject projectile2 = Instantiate(projectilePrefab, shootingPoint2.position, shootingPoint2.rotation);
            Rigidbody rb2 = projectile2.GetComponent<Rigidbody>();
            rb2.velocity = shootDirection * speed;
        }
    }
}
