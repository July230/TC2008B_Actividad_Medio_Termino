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
    public GameObject laserPrefab;
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

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        // Apuntar la nave hacia el cursor
        PointAtCursor();

        if (Input.GetMouseButtonDown(0) && Time.time > nextShootTime) // Click izquierdo para disparar
        {
            ShootLaser();
            nextShootTime = Time.deltaTime * shootInterval;
        }
    }


    private void PointAtCursor()
    {        
        // Obtener la posicion actual del cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Asegurarse de que la coordenada y es la misma que el objeto
        mousePosition.y = transform.position.y; 

        // Calcular la direccion desde el objeto al cursor
        Vector3 direction = mousePosition - transform.position; 

        // Asegurarse de que la direccion esta en el plano xz
        //direction.z = 0;

        // Calcular el angulo de rotacion en el plano xz
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // Crear la rotacion deseada alrededor del eje y
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, angle, 0));

        // Aplicar la rotacion suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);


    }

    private void ShootLaser()
    {
        if (laserPrefab)
        {
            Debug.Log("Click izquierdo");

            // Disparar desde el primer punto de disparo
            GameObject projectile1 = Instantiate(laserPrefab, shootingPoint1.position, shootingPoint1.rotation);
            Laser laser1 = projectile1.GetComponent<Laser>();
            if (laser1 == null)
            {
                laser1.SetDirection(shootingPoint1.forward);
            }

            // Disparar desde el segundo punto de disparo
            GameObject projectile2 = Instantiate(laserPrefab, shootingPoint2.position, shootingPoint2.rotation);
            Laser laser2 = projectile2.GetComponent<Laser>();
            if (laser2 == null)
            {
                laser2.SetDirection(shootingPoint2.forward);
            }
        }
    }
}
