using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player")]
public class PlayerData : ScriptableObject
{
    public int health;
    public int healthMax;
    
    [Serializable]
    public struct SaveItemData
    {
        public int itemId;
        public int itemNbr;
        
        public SaveItemData(int id, int nbr)
        {
            itemId = id;
            itemNbr = nbr;
        }
    }

    public List<SaveItemData> saveItemDatas;
}
