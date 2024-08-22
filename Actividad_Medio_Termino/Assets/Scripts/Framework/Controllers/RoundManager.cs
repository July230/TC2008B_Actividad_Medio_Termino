using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La clase round manager se encarga de administrar las rondas
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class RoundManager : MonoBehaviour
{
    public EnemyManager enemyManager; // Referencia al EnemyManager
    int totalRounds = 3;
    public float roundDelay = 5.0f; // Tiempo entre rondas
    private int currentRound = 0;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        if(enemyManager != null){
            StartCoroutine(ManageRounds());
        }
        else
        {
            Debug.LogError("EnemyManager no asignado en el RoundManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ManageRounds es una corrutina encargada de administrar las rondas
    /// </summary>
    private IEnumerator ManageRounds()
    {
        while(currentRound < totalRounds)
        {
            // Incrementar la ronda
            currentRound++;

            Debug.Log("Ronda" + currentRound + "ha comenzado");

            // Iniciar la aparicion de enemigos
            enemyManager.StartRound(currentRound);

            // Esperar a que todos los enemigos de la ronda sean derrotados
            yield return new WaitUntil(() => enemyManager.AreAllEnemiesDefeated());

            Debug.Log("Ronda" + currentRound + "Completada");

            // Pausa entre rondas
            yield return new WaitForSeconds(roundDelay);
        }
        Debug.Log("Todas las rondas completadas");
    }
}
