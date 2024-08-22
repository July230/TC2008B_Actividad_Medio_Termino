using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI bossHealthText;
    public TextMeshProUGUI missileCooldownText; // Referencia al texto que mostrara el cooldown del misil

    private int projectileCount = 0;
    private int enemyCount = 0;
    private float missileCooldown = 15.0f;
    private float nextMissileTime = 0f;

    private Health playerHealth;
    private Health bossHealth;

    /// <summary>
    /// Start is llamado antes de la primera actualizacion del frame
    /// </summary>
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        UpdateInfoText();
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        // Buscar al jefe en cada frame y actualizar referencia de ser necesario
        if(bossHealth == null)
        {
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            if(boss != null)
            {
                bossHealth = boss.GetComponent<Health>();
            }
        }

        // Actualizar el contador de proyectiles basado en la cantidad en escena
        projectileCount = FindObjectsOfType<Laser>().Length + FindObjectsOfType<Missile>().Length + FindObjectsOfType<BossProjectile>().Length;
        enemyCount = FindObjectsOfType<Boss>().Length + FindObjectsOfType<Enemy>().Length;

        // Si el jefe ha sido destruido, ocultar el texto de su salud
        if(bossHealth.currentHealth <= 0)
        {
            bossHealthText.gameObject.SetActive(false);
        }

        // Actualiza la informacion mostrada
        UpdateInfoText();
    }

    /// <summary>
    /// SetNextMissileTime actualiza el cooldown del misil
    /// </summary>
    public void SetNextMissileTime()
    {
        nextMissileTime = Time.time + missileCooldown;
    }

    /// <summary>
    /// OnEnable 
    /// </summary>
    private void OnEnable()
    {
        TimeManager.OnSecondChanged += UpdateInfoText;
        TimeManager.OnMinuteChanged += UpdateInfoText;
    }

    /// <summary>
    /// OnDisable 
    /// </summary>
    private void OnDisable()
    {
        TimeManager.OnSecondChanged -= UpdateInfoText;
        TimeManager.OnMinuteChanged -= UpdateInfoText;
    }

    /// <summary>
    /// UpdateInfoText actualiza el objeto text para mostrar informacion en el UI 
    /// </summary>
    public void UpdateInfoText()
    {
        if (infoText != null)
        {
            string info = $"Proyectiles: {projectileCount}\n" +
                          $"Enemigos: {enemyCount}\n" +
                          $"Tiempo: {TimeManager.Minute.ToString("00")}:{TimeManager.Second:00}";
            infoText.text = info;
        }
        else
        {
            Debug.Log("infoText no esta asignado");
        }

        // Actualizar la vida del jugador y del jefe en la UI
        if (playerHealthText != null && playerHealth != null)
        {
            playerHealthText.text = $"HP: {playerHealth.currentHealth}/{playerHealth.maxHealth}";
        }
        if (bossHealthText != null && bossHealth != null)
        {
            bossHealthText.text = $"Boss HP: {bossHealth.currentHealth}/{bossHealth.maxHealth}";
        }

        // Actualizar cooldown del misil
        if (missileCooldownText != null)
        {
            float remainingCooldown = Mathf.Max(0, nextMissileTime - Time.time);
            //Debug.Log($"Cooldown misil: {remainingCooldown}");
            missileCooldownText.text = $"Missile cooldown: {remainingCooldown:F1}s\n";

            if (remainingCooldown <= 0)
            {
                // Misil listo para disparar
                missileCooldownText.color = Color.green;
            }
            else
            {
                // Misil cargando
                missileCooldownText.color = Color.red;
            }
        }
        else
        {
            Debug.Log("missileCooldownText no está asignado");
        }
    }
}
