using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Items", fileName = "ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> items;
}
