using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image imgBar;
    private void Update()
    {
        imgBar.fillAmount = (float)playerData.health / playerData.healthMax;
    }
}
