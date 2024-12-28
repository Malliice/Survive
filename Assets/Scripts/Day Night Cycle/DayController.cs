using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayController : MonoBehaviour
{
    [Header("Time")] 
    [SerializeField, Tooltip("Day Length in Minutes")] private float targetDayLength = 13f;
    [SerializeField, Range(0, 1)] private float timeOfDay;
    [SerializeField] private int dayNumber = 0;
    private float timeScale;
    public bool pause = false;
    private const int SecondsInDay = 86400;

    [Header("Sun Light")] 
    [SerializeField] private Light sun;
    [SerializeField] private Gradient sunColor;
    
    [Header("References")]
    [SerializeField] private Image imgTimer;
    [SerializeField] private UnityEvent endTimerEvent;
    
    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
        }
        AdjustSunColor();
    }
    
    private void UpdateTimeScale()
    {
        timeScale = 24 / (targetDayLength / 60);
    }

    private void UpdateTime()
    {
        timeOfDay += Time.deltaTime * timeScale / SecondsInDay;
        imgTimer.fillAmount = 1-timeOfDay;
        if (timeOfDay > 1)
        {
            dayNumber++;
            timeOfDay -= 1;
            endTimerEvent.Invoke();
        }
    }

    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(timeOfDay);
    }
}
