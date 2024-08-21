using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    //public TextMeshProUGUI projectileCountText; // Referencia al texto que mostrara el contador de proyectiles
    //public TextMeshProUGUI missileCooldownText; // Referencia al texto que mostrara el cooldown del misil
    //public TextMeshProUGUI timeText;
    public TextMeshProUGUI infoText;

    private int projectileCount = 0;
    private float missileCooldown = 15.0f;
    private float nextMissileTime = 0f;

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
        // Actualizar el contador de proyectiles basado en la cantidad en escena
        projectileCount = FindObjectsOfType<Laser>().Length + FindObjectsOfType<Missile>().Length + FindObjectsOfType<BossProjectile>().Length;

        // Actualiza la informacion mostrada
        UpdateInfoText();
    }

    public void SetNextMissileTime()
    {
        nextMissileTime = missileCooldown;
    }

    private void OnEnable()
    {
        TimeManager.OnSecondChanged += UpdateInfoText;
        TimeManager.OnMinuteChanged += UpdateInfoText;
    }

    private void OnDisable()
    {
        TimeManager.OnSecondChanged -= UpdateInfoText;
        TimeManager.OnMinuteChanged -= UpdateInfoText;
    }

    public void UpdateInfoText()
    {
        if (infoText != null)
        {
            float remainingCooldown = Mathf.Max(0, nextMissileTime - Time.time);
            string info = $"Proyectiles: {projectileCount}\n" +
                          $"Missile cooldown: {remainingCooldown:F1}s\n" +
                          $"Tiempo: {TimeManager.Minute.ToString("00")}:{TimeManager.Second:00}";

            if (remainingCooldown <= 0)
            {
                // Misil listo para disparar
                infoText.color = Color.green;
            }
            else
            {
                // Misil cargando
                infoText.color = Color.red;
            }

            infoText.text = info;
        }
        else
        {
            Debug.Log("infoText no esta asignado");
        }
    }
}
