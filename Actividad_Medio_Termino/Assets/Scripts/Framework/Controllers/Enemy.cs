using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// La clase enemy actualiza los eventos de los enemigos.
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class Enemy : MonoBehaviour
{
    public float speed = 100.0f;
    public GameObject projectilePrefab;
    public Transform attackPoint;
    public float shootInterval = 5.0f;
    public float nextShootTime = 0f;
    private float timeBetweenShoots = 0.2f;
    private int currentPattern = 0;
    private float patternPause = 2.0f;
    private bool isShootingPatternActive;
    private Health health;


    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        health = GetComponent<Health>();
        StartCoroutine(ShottingPattern());
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        if(Time.time >= nextShootTime && !isShootingPatternActive)
        {
            StartCoroutine(ShottingPattern());
            nextShootTime = Time.time + shootInterval;
        }
    }

    /// <summary>
    /// ShottingPattern tiene la logica para los patrones de los enemigos
    /// </summary>
    private IEnumerator ShottingPattern()
    {
        switch(currentPattern)
        {
            // Aqui van los patrones de ataque
            case 0:
                yield return StartCoroutine(PatternOne());
                break;
            case 1:
                yield return StartCoroutine(PatternTwo());
                break;
        }
        yield return StartCoroutine(ChangeAttackPattern());
        isShootingPatternActive = false;
    }

    /// <summary>
    /// ChangeAttackPattern tiene la logica para cambiar de patrones de disparo de forma aleatoria
    /// </summary>
    private IEnumerator ChangeAttackPattern()
    {
        // Pausa antes de cambiar de patron
        yield return new WaitForSeconds(patternPause);

        // Cambiar al siguiente patron
        int numberOfPatterns = 2;
        currentPattern = Random.Range(0, numberOfPatterns);
    }

    /// <summary>
    /// PatternOne tiene la logica para un patron en rafagas 
    /// </summary>
    private IEnumerator PatternOne()
    {
        shootInterval = 2.0f;
        int numberOfProjectiles = 30;
        float angleStep = 360f / numberOfProjectiles; 

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(direction); // Configurar la direccion del proyectil
            }
        }

        yield return new WaitForSeconds(shootInterval); // Tiempo entre disparos
    }

    /// <summary>
    /// PatternTwo tiene la logica para un patron de disparo consecutivo
    /// </summary>
    private IEnumerator PatternTwo()
    {
        shootInterval = 0.5f;
        int numberOfProjectiles = 18;
        float angleStep = 360f / numberOfProjectiles; 

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));
            BossProjectile bossProjectile = projectile.GetComponent<BossProjectile>();
            if(bossProjectile != null)
            {
                bossProjectile.SetDirection(direction);
            }
            yield return new WaitForSeconds(timeBetweenShoots);
        }

        yield return new WaitForSeconds(shootInterval);
    }

    /// <summary>
    /// OnCollisionEnter es llamado cada vez que el enemigo es impactado por un proyectil
    /// </summary>
    private void ReceiveDamage(int damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
