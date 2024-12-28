using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<InventorySlotController> slots;
    [SerializeField] protected ItemDataBase itemDataBase;

    public virtual void AddItem(int itemId)
    {
        ItemData newItem = itemDataBase.items[itemId];
        InventorySlotController slot = null;
        
        //On dit que le slot qu'on remplit avec l'item c'est celui qui contient déjà un item de ce type
        slot = slots.Find(x => x.currentHeldItem.Contains(newItem));
        //Si y'en a pas, on prend le premier slot vide
        if (slot == null) slot = slots.Find(x => x.currentHeldItem.Count <= 0);
        
        //Si y'a pas de slot, c'est qu'ils sont tous remplits, on ne fait rien du coup
        if(slot == null)
            return;

        slot.AddItem(newItem);
        slot.itemId = itemId;
    }

    public virtual void RemoveItem(int itemId)
    {
        ItemData newItem = itemDataBase.items[itemId];
        InventorySlotController slot = null;
        
        slot = slots.Find(x => x.currentHeldItem.Contains(newItem));
        
        if(slot == null)
            return;
        
        slot.RemoveItem();
    }

    public virtual void RemoveItemByName(string itemName)
    {
        ItemData newItem = itemDataBase.items.Find(x => x.itemName == itemName);
        InventorySlotController slot = null;
        
        slot = slots.Find(x => x.currentHeldItem.Contains(newItem));
        
        if(slot == null)
            return;
        
        slot.RemoveItem();
    }

    public bool HasItem(int itemId, int itemNbr = 1)
    {
        //S'il n'existe pas de slot qui contienne au moins un objet, on retourne faux
        if (!slots.Exists(x => x.currentHeldItem.Count > 0))
            return false;
        
        //si on trouve un slot dont le premier objet est l'objet de la database
        
        //// On devrait pas avoir de problème, mais quand on essaye de craft sans matériaux, on obtient une erreur
        //// C'est possiblement parce qu'on passe la condition précédente grâce au fait qu'on ait d'autres items
        //// Mais dans cette condition-là, on demande s'il existe un item du genre
        
        foreach (var s in slots)
        {
            if (s.currentHeldItem.Count <= 0)
                continue;

            //Si un slot contient l'item désiré
            if (s.currentHeldItem[0] == itemDataBase.items[itemId])
            {
                //et qu'on a assez de nombre
                if (s.currentHeldItem.Count >= itemNbr)
                    return true;
                
                return false;
            }
        }
        //Si la boucle a skip tous les slots, ça veut dire qu'on n'a pas l'item
        return false;
    }

    public bool HasItemByName(string itemName, int itemNbr = 1)
    {
        //S'il n'existe pas de slot qui contienne au moins un objet, on retourne faux
        if (!slots.Exists(x => x.currentHeldItem.Count > 0))
        {
            return false;
        }

        //si on trouve un slot dont le premier objet est l'objet de la database
        //On devrait pas avoir de problème, mais quand on essaye de craft sans matériaux, on obtient une erreur
        bool hasItem = false;
        foreach (var s in slots)
        {
            if (s.currentHeldItem.Count <= 0)
                continue;

            if (s.currentHeldItem[0].itemName == itemName)
                hasItem = true;
        }
        if (hasItem)
        {
            if (slots.Find(x => x.currentHeldItem[0].itemName == itemName).currentHeldItem.Count < itemNbr)
                return false;
            return true;
        }

        return false;
    }
}
