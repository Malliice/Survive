using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public int itemId = 0;
    public int itemNbr = 0;

    [SerializeField] private ItemDataBase itemDataBase;
    [SerializeField] private MeshRenderer lootMeshRenderer;

    private void Awake()
    {
        UpdateSprite();
    }

    public void AddItem(int id, int nbr = 1)
    {
        itemId = id;
        itemNbr = nbr;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        lootMeshRenderer.material.mainTexture = itemDataBase.items[itemId].itemSprite.texture;
    }
}
