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
    public Transform[] spawnPoints; // Puntos de aparicion de los enemigos

    private List<GameObject> activeEnemies = new List<GameObject>();
    
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
