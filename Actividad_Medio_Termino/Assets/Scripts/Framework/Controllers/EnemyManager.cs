using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase enemy manager se encarga de la aparicion de los enemigos
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Health bossHealth; // Referenciar al objeto health de boss
    public Transform[] spawnPoints; // Puntos de aparicion de los enemigos

    private List<GameObject> activeEnemies = new List<GameObject>();

    private bool enemiesSpawned750 = false;
    private bool enemiesSpawned500 = false;
    private bool enemiesSpawned250 = false;
    
    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    public void StartRound(int roundNumber)
    {
        // En la tercera ronda, aparece el jefe
        int enemiesPerRound = (roundNumber == 3) ? 1 : roundNumber * 3;
        StartCoroutine(SpawnEnemies(enemiesPerRound, roundNumber));
    }

    /// <summary>
    /// Update comprueba la vida del jefe periodicamente
    /// </summary>
    private void Update()
    {
        if(bossHealth != null)
        {
            CheckHealth();
        }
    }

    /// <summary>
    /// CheckHealth se usa para comprobar la vida del jefe
    /// </summary>
    private void CheckHealth()
    {
        if (bossHealth == null)
        {
            Debug.LogError("bossHealth no está asignado.");
            return;
        }

        // Invocar enemigos cuando la salud llegue a 750, 500 y 250
        if (bossHealth.currentHealth <= 750 && bossHealth.currentHealth > 500 && !enemiesSpawned750)
        {
            Debug.Log("Invocando enemigos");
            StartCoroutine(SpawnEnemies(4, 2));
            enemiesSpawned750 = true;
        }
        else if (bossHealth.currentHealth <= 500 && bossHealth.currentHealth > 250 && !enemiesSpawned500)
        {
            Debug.Log("Invocando enemigos");
            StartCoroutine(SpawnEnemies(6, 2));
            enemiesSpawned500 = true;
        }
        else if (bossHealth.currentHealth <= 250 && !enemiesSpawned250)
        {
            Debug.Log("Invocando enemigos");
            StartCoroutine(SpawnEnemies(8, 2));
            enemiesSpawned250 = true;
        }
    }
    
    /// <summary>
    /// SpawnEnemies es la corrutina encargada de aparecer a los enemigos
    /// </summary>
    private IEnumerator SpawnEnemies(int numberOfEnemies, int roundNumber)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            if (enemyPrefab != null && spawnPoints.Length > 0)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy;

                if (roundNumber == 3 && i == 0) // Si es la tercera ronda, solo el jefe aparece
                {
                    enemy = Instantiate(bossPrefab, new Vector3(0, 30, 0), Quaternion.Euler(0, 180, 0));
                    enemy.tag = "Boss";

                    // Asignar la referencia de bossHealth
                    bossHealth = enemy.GetComponent<Health>(); // Asegúrate de que el componente Health esté en el prefab
                    
                    // Verificar si bossHealth es null
                    if (bossHealth == null)
                    {
                        Debug.LogError("El prefab del jefe no tiene un componente Health.");
                    }
                }
                else
                {
                    enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.Euler(0, 180, 0));
                    enemy.tag = "Enemy";
                }
                

                activeEnemies.Add(enemy);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                Debug.LogError("Prefab o puntos de aparición no asignados.");
                yield break; // Salir de la corrutina si hay un problema
            }
        }

        Debug.Log("Todas las apariciones de enemigos completadas");
    }

    /// <summary>
    /// AreAllEnemiesDefeated determina si todos los enemigos han sido destruidos
    /// </summary>
    public bool AreAllEnemiesDefeated()
    {
        // Limpiar lista de enemigos destruidos
        activeEnemies.RemoveAll(enemy => enemy == null);

        // Devolver verdadero si no hay enemigos activos
        return activeEnemies.Count == 0;
    }
}
