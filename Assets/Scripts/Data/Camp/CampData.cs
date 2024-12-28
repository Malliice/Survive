using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Camp", fileName = "CampData")]
public class CampData : ScriptableObject
{
    [Serializable]
    public struct NpcData
    {
        public int health;
    }

    public List<NpcData> npcData = new List<NpcData>();
}
