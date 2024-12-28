using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField] private EventSO winEvent;
    [SerializeField] private EventSO deathEvent;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject deathScreen;

    private void Awake()
    {
        winEvent.eventToInvoke.AddListener(Win);
        deathEvent.eventToInvoke.AddListener(Death);
    }

    void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    void Death()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
