using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour, IPointerClickHandler
{
    private InventoryUiController inventoryController;
    
    [SerializeField] private Image imgItemSprite;
    [SerializeField] private Image imgSlotBorder;
    [SerializeField] private TextMeshProUGUI txtSlotCount;

    public List<ItemData> currentHeldItem = new List<ItemData>();

    [HideInInspector] public int itemId;

    public void InitSlot(InventoryUiController inventory)
    {
        inventoryController = inventory;
    }

    public void AddItem(ItemData newItem, int itemNbr = 1)
    {
        //Si on ajoute un item différent, erreur. Mais pour ça il faut déjà avoir un item
        if (currentHeldItem.Count > 0 && newItem != currentHeldItem[0])
        {
            print("Trying to add different object to slot");
            return;
        }

        for (int i = 0; i < itemNbr; i++)
        {
            currentHeldItem.Add(newItem);
        }
        
        UpdateUi();
    }

    public void RemoveItem()
    {
        if(currentHeldItem.Count <= 0)
            return;
        
        currentHeldItem.RemoveAt(0);

        if (currentHeldItem.Count <= 0)
            itemId = -1;
        
        UpdateUi();
    }

    public void UpdateUi()
    {
        if (currentHeldItem.Count > 0)
        {
            txtSlotCount.text = currentHeldItem.Count.ToString("00");
            imgItemSprite.gameObject.SetActive(true);
            imgItemSprite.sprite = currentHeldItem[0].itemSprite;
            return;
        }

        txtSlotCount.text = "";
        imgItemSprite.gameObject.SetActive(false);
    }

    public void ClearSlot()
    {
        currentHeldItem.Clear();
        UpdateUi();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnLeftClick();
                break;
            case PointerEventData.InputButton.Right:
                OnRightClick();
                break;
        }
    }

    void OnLeftClick()
    {
        //Si le slot est vide
        if (currentHeldItem.Count <= 0)
        {
            //Si le slot de la main est vide
            if (inventoryController.inventoryHandController.itemId == -1)
                return;
            //Si le slot de la main contient un item
            else
            {
                itemId = inventoryController.inventoryHandController.itemId;
                AddItem(inventoryController.ItemDataBase.items[itemId],
                    inventoryController.inventoryHandController.itemNbr);
                inventoryController.inventoryHandController.hoveredSlot.ClearSlot();
                inventoryController.inventoryHandController.EmptySlot();
                inventoryController.inventoryHandController.DisplaySlot(false);
                Cursor.visible = true;
            }
        }
        else
        {
            //Si le slot de la main est vide
            if (inventoryController.inventoryHandController.itemId == -1)
            {
                Cursor.visible = false;
                inventoryController.inventoryHandController.itemId = itemId;
                inventoryController.inventoryHandController.itemNbr = currentHeldItem.Count;
                inventoryController.inventoryHandController.hoveredSlot = this;
                inventoryController.inventoryHandController.DisplaySlot(true);
            }
            //Si le slot de la main est plein ET qu'on clique sur un slot avec un item
            else
            {
                int temp = itemId;
                int tempNbr = currentHeldItem.Count;
                
                itemId = inventoryController.inventoryHandController.itemId;
                ClearSlot();
                AddItem(inventoryController.ItemDataBase.items[itemId],
                    inventoryController.inventoryHandController.itemNbr);

                inventoryController.inventoryHandController.hoveredSlot.ClearSlot();
                inventoryController.inventoryHandController.hoveredSlot.AddItem(
                    inventoryController.ItemDataBase.items[temp], tempNbr);
                inventoryController.inventoryHandController.hoveredSlot.itemId = temp;
                
                inventoryController.inventoryHandController.EmptySlot();
                inventoryController.inventoryHandController.DisplaySlot(false);
                
                Cursor.visible = true;
            }
        }
    }

    void OnRightClick()
    {
        if (currentHeldItem.Count <= 0)
        {
            inventoryController.equipmentController.EquipItem(null);
            inventoryController.playerController.currentTool = ResourceContainerController.ToolNeeded.None;
            
            return;
        }

        if (currentHeldItem[0].itemType == ItemData.ItemType.TOOL)
        {
            //Equip
            inventoryController.equipmentController.EquipItem(currentHeldItem[0]);
            inventoryController.playerController.damage = currentHeldItem[0].itemDamages;
            
            if (currentHeldItem[0].itemName.Contains("Pickaxe"))
                inventoryController.playerController.currentTool = ResourceContainerController.ToolNeeded.Pickaxe;
            else if (currentHeldItem[0].itemName.Contains("Axe"))
                inventoryController.playerController.currentTool = ResourceContainerController.ToolNeeded.Axe;
            
        }
        else
        {
            //On déséquipe l'objet
            inventoryController.equipmentController.EquipItem(null);
            inventoryController.playerController.currentTool = ResourceContainerController.ToolNeeded.None;
        }
    }
}
