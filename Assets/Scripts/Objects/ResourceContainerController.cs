using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteAlways]
public class ResourceContainerController : MonoBehaviour
{
    [Min(0)]public int itemId;
    [SerializeField, HideInInspector] public string stringValue;
    [SerializeField] private int containerHealth;
    [SerializeField] private GameObject lootPrefab;

    public enum ToolNeeded
    {
        None,
        Pickaxe,
        Axe
    }
    public ToolNeeded toolNeeded;
    
    [SerializeField] private Image imgResource;
    private Sprite spriteToDisplay;
    public Sprite SpriteToDisplay => spriteToDisplay;
    
    public ItemDataBase dataBase;

    public UnityEvent collectedEvent;

    private void Awake()
    {
        imgResource.sprite = dataBase.items[itemId].itemSprite;
    }

    private void Update()
    {
        if (dataBase == null || itemId >= dataBase.items.Count)
        {
            stringValue = "Null";
            spriteToDisplay = null;
            return;
        }
        stringValue = "Item: " + dataBase.items[itemId].itemName;
        spriteToDisplay = dataBase.items[itemId].itemSprite;
    }

    public void DamageResourceContainer(ToolNeeded tool)
    {
        //Si on a pas le bon outils et que la ressource demande un outil, on annule
        if(tool != toolNeeded && toolNeeded != ToolNeeded.None)
            return;
        
        containerHealth--;
        if (containerHealth <= 0)
        {
            gameObject.SetActive(false);
            collectedEvent.Invoke();
            LootController loot = Instantiate(lootPrefab, transform.position, lootPrefab.transform.rotation)
                .GetComponent<LootController>();
            loot.AddItem(itemId);
        }
    }
}
