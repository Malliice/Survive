using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : InventoryController
{
    [SerializeField] private PlayerData playerData;
    public void InitInventory()
    {
        LoadSlotsData();
        foreach (var slot in slots)
        {
            slot.UpdateUi();
        }
    }

    public override void AddItem(int itemId)
    {
        base.AddItem(itemId);
    }

    public override void RemoveItem(int itemId)
    {
        base.RemoveItem(itemId);
    }

    public void SaveSlotsData()
    {
        for (int i = 0; i < 6; i++)
        {
            if (slots[i].currentHeldItem.Count <= 0)
            {
                playerData.saveItemDatas[i] = new PlayerData.SaveItemData(-1, 0);
                continue;
            }
            
            var playerDataSaveItemData = playerData.saveItemDatas[i];
            playerDataSaveItemData.itemNbr = slots[i].currentHeldItem.Count;
            playerDataSaveItemData.itemId = slots[i].itemId;
            playerData.saveItemDatas[i] = playerDataSaveItemData;
        }
    }

    public void LoadSlotsData()
    {
        for (int i = 0; i < 6; i++)
        {
            if(playerData.saveItemDatas[i].itemId == -1)
                continue;

            for (int j = 0; j < playerData.saveItemDatas[i].itemNbr; j++)
            {
                AddItem(playerData.saveItemDatas[i].itemId);
            }
        }
    }

    [ContextMenu("Clear Inventory")]
    void ClearInventory()
    {
        foreach (var s in slots)
        {
            s.ClearSlot();
        }
        
        SaveSlotsData();
    }

    public void EquipItem(ItemData item)
    {
        
    }
}
