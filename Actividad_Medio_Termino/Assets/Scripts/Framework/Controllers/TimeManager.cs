using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{

    public static Action OnSecondChanged;
    public static Action OnMinuteChanged;

    public static int Second{get; private set;}
    public static int Minute{get;private set;}

    private float timer = 0f;

    /// <summary>
    /// Start inicia los calores y configura el temporizador
    /// </summary>
    void Start()
    {
        Second = 0;
        Minute = 0;
    }

    /// <summary>
    /// Update es llamado una vez por frame
    /// </summary>
    void Update()
    {
        // Acumular el tiempo real
        timer += Time.deltaTime;

        // Si ha pasado un segundo completo
        if(timer >= 1f)
        {
            Second++;

            // Evento de cambio de segundo
            OnSecondChanged?.Invoke();

            if(Second >= 60)
            {
                Minute++;
                OnMinuteChanged?.Invoke();
                Second = 0;
            }

            timer = 0f;
        }
    }
}
