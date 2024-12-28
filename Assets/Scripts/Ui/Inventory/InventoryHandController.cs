using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandController : MonoBehaviour
{
    [SerializeField] private RectTransform handSlot;
    [SerializeField] private Image imgItemSprite;
    [SerializeField] private TextMeshProUGUI txtSlotCount;
    [SerializeField] private ItemDataBase itemDataBase;
    public int itemId;
    public int itemNbr;
    public InventorySlotController hoveredSlot;

    private void Awake()
    {
        DisplaySlot(false);
    }

    private void Update()
    {
        handSlot.position = Input.mousePosition;
    }

    public void DisplaySlot(bool isShown)
    {
        handSlot.gameObject.SetActive(isShown);
        Cursor.visible = !isShown;
        
        if(!isShown)
            return;

        imgItemSprite.sprite = itemDataBase.items[itemId].itemSprite;
        txtSlotCount.text = itemNbr.ToString("00");
    }

    public void EmptySlot()
    {
        itemId = -1;
        hoveredSlot = null;
    }
}
