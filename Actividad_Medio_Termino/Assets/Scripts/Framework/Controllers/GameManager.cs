using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para usar SceneManager

/// <summary>
/// La clase game manager reestablece la escena a su estado inicial
/// Documentación estándar de código aquí
/// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
/// </summary>

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// RestartGame recarga la escena actual
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
