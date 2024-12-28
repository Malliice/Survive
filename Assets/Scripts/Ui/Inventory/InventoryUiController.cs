using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUiController : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject slotParent;
    [SerializeField, Range(1, 10)] private int slotNbr;

    [HideInInspector] public PlayerController playerController;
    private PlayerInventoryController playerInventory;
    public InventoryHandController inventoryHandController;
    public ItemDataBase ItemDataBase;
    [HideInInspector] public InventoryEquipmentController equipmentController;

    [SerializeField] private GameObject lootPrefab;

    private void Awake()
    {
        List<InventorySlotController> slots = new List<InventorySlotController>();
        for (int i = 0; i < slotNbr; i++)
        {
            slots.Add(Instantiate(slotPrefab, slotParent.transform).GetComponent<InventorySlotController>());
            slots[i].InitSlot(this);
        }

        equipmentController = FindObjectOfType<InventoryEquipmentController>();
        playerInventory = FindObjectOfType<PlayerInventoryController>();
        playerController = playerInventory.GetComponent<PlayerController>();
        playerInventory.slots.AddRange(slots);
        playerInventory.InitInventory();
    }

    public void LootItem()
    {
        //Spawn position
        Vector3 spawnPos =
            new Vector3(playerInventory.transform.position.x, -0.535f, playerInventory.transform.position.z);
        //Loot !
        LootController loot = Instantiate(lootPrefab, spawnPos, lootPrefab.transform.rotation).GetComponent<LootController>();
        loot.AddItem(inventoryHandController.itemId, inventoryHandController.itemNbr);
        
        //Supprimer les objets de l'inventaire
        for (int i = 0; i < inventoryHandController.itemNbr; i++)
        {
            playerInventory.RemoveItem(loot.itemId);
        }
    }
}
