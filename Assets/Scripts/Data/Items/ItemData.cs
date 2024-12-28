using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ItemData
{
    public string itemName;
    public Sprite itemSprite;
    public int itemDamages;

    [Serializable]
    public struct ResourceToCraft
    {
        public int resourceId;
        public int resourceCost;
    }
    public List<ResourceToCraft> resourcesToCraft;

    public GameObject prefabToSpawnIfBuilding;
    public EventSO craftEvent;

    public enum CraftLevel
    {
        DEFAULT,
        HIGH,
        MAGIC
    }
    public CraftLevel craftLevel;

    public enum ItemType
    {
        NONE,
        RESOURCE,
        TOOL,
        BUILDING,
        OBJECTIVE
    }
    public ItemType itemType;
}
