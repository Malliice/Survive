using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalWorldInitializer : MonoBehaviour
{
    [SerializeField] private SurvivalWorldData survivalWorldData;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        playerTransform.position = survivalWorldData.playerLastPos;
    }
    
    public void SavePlayerPosition(GameObject pos)
    {
        survivalWorldData.playerLastPos = pos.transform.position;
    }
}
