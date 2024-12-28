using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentController : MonoBehaviour
{
    private ItemData currentTool;
    
    [SerializeField] private Material toolMat;
    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        UpdateEquipmentSprite();
    }

    public void EquipItem(ItemData item)
    {
        currentTool = item;
        UpdateEquipmentSprite();
    }
    
    void UpdateEquipmentSprite()
    {
        //Si on a aucun outil, on cache le sprite
        if (currentTool == null)
        {
            meshRenderer.enabled = false;
            return;
        }

        meshRenderer.enabled = true;
        toolMat.mainTexture = currentTool.itemSprite.texture;
    }
}
