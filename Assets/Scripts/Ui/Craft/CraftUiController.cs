using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CraftUiController : MonoBehaviour
{
    private CraftPlacementController craftPlacementController;
    
    [SerializeField] private CraftInfoController craftInfo;
    [SerializeField] private Transform craftInfoParent;
    [SerializeField] private Transform craftSlotsParent;
    [SerializeField] private GameObject craftSlotsPrefab;
    private List<CraftSlotController> craftSlots = new List<CraftSlotController>();

    private GameObject craftInfoFollow;
    private CraftSlotController currentSlot;

    [SerializeField] private ItemDataBase database;

    [SerializeField] private PlayerInventoryController inventory;

    private void Awake()
    {
        craftPlacementController = GetComponent<CraftPlacementController>();
        
        SpawnSlots();

        craftInfo.gameObject.SetActive(false);
    }

    public void SpawnSlots()
    {
        int t = 0;
        for (int i = 0; i < database.items.Count; i++)
        {
            if (database.items[i].resourcesToCraft.Count <= 0)
            {
                t++;
                continue;
            }
            
            craftSlots.Add(Instantiate(craftSlotsPrefab, craftSlotsParent).GetComponent<CraftSlotController>());
            craftSlots[i-t].itemToCraft = database.items[i];
            craftSlots[i-t].itemId = i;
            craftSlots[i-t].InitSlot(this);
        }

        foreach (var slot in craftSlots)
        {
            if (slot.itemToCraft.craftLevel == ItemData.CraftLevel.HIGH ||
                slot.itemToCraft.craftLevel == ItemData.CraftLevel.MAGIC)
            {
                slot.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(craftInfoFollow == null)
            return;
        
        craftInfo.transform.position = new Vector3(craftInfoParent.position.x, craftInfoFollow.transform.position.y);
    }

    public void OpenCraftInfo(CraftSlotController toFollow)
    {
        craftInfo.gameObject.SetActive(true);
        currentSlot = toFollow;
        craftInfo.item = toFollow.itemToCraft;
        craftInfo.UpdateCraftInfoUi();
        craftInfoFollow = toFollow.gameObject;
    }

    public void CloseCraftInfo()
    {
        currentSlot = null;
        craftInfo.gameObject.SetActive(false);
    }

    public void Craft()
    {
        foreach (var resource in currentSlot.itemToCraft.resourcesToCraft)
        {
            //Si l'inventaire n'a PAS les ressources nécessaires avec leur coût
            if (!inventory.HasItem(resource.resourceId, resource.resourceCost))
                return;
        }
        
        foreach (var resource in currentSlot.itemToCraft.resourcesToCraft)
        {
            for (int i = 0; i < resource.resourceCost; i++)
            {
                inventory.RemoveItem(resource.resourceId);
            }
        }

        //Si l'item a un prefab de building, on ne l'ajoute pas à l'inventaire, on le pose par terre
        if (currentSlot.itemToCraft.prefabToSpawnIfBuilding)
        {
            craftPlacementController.PreviewCraft(currentSlot.itemToCraft.prefabToSpawnIfBuilding);
        }
        else
        {
            inventory.AddItem(currentSlot.itemId);
        }

        if (currentSlot.itemToCraft.craftEvent)
        {
            currentSlot.itemToCraft.craftEvent.eventToInvoke.Invoke();
        }
    }

    public void DisplayCraft(ItemData.CraftLevel lvl, bool isShown)
    {
        List<CraftSlotController> slots = new List<CraftSlotController>();
        switch (lvl)
        {
            case ItemData.CraftLevel.HIGH:
                slots.AddRange(craftSlots.FindAll(x=>x.itemToCraft.craftLevel == ItemData.CraftLevel.HIGH));
                break;
            case ItemData.CraftLevel.MAGIC:
                slots.AddRange(craftSlots.FindAll(x=>x.itemToCraft.craftLevel == ItemData.CraftLevel.MAGIC));
                break;
        }

        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(isShown);
        }
    }
}
