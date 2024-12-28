using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTableController : MonoBehaviour
{
    private CraftUiController craft;
    public ItemData.CraftLevel craftLevel;
    private void Awake()
    {
        craft = FindObjectOfType<CraftUiController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        craft.DisplayCraft(craftLevel, true);
    }

    private void OnTriggerExit(Collider other)
    {
        craft.DisplayCraft(craftLevel, false);
    }
}
