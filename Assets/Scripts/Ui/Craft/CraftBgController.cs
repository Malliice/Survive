using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftBgController : MonoBehaviour, IPointerClickHandler
{
    private CraftUiController craftUiController;
    private InventoryUiController inventoryUiController;

    private void Awake()
    {
        craftUiController = GetComponentInParent<CraftUiController>();
        inventoryUiController = FindObjectOfType<InventoryUiController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(craftUiController) craftUiController.CloseCraftInfo();
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //Drop
            if(inventoryUiController.inventoryHandController.itemId == -1)
                return;
            
            inventoryUiController.LootItem();
        }
        
        inventoryUiController.inventoryHandController.DisplaySlot(false);
        inventoryUiController.inventoryHandController.EmptySlot();
        Cursor.visible = true;
    }
}
