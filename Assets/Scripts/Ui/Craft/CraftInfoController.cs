using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftInfoController : MonoBehaviour
{
    public ItemData item;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private GameObject craftResourceSlotPrefab;
    [SerializeField] private Transform craftResourceSlotParent;

    [SerializeField] private ItemDataBase database;
    
    private List<CraftResourceSlotController> craftResourcesSlots = new List<CraftResourceSlotController>();
    
    public void UpdateCraftInfoUi()
    {
        txtName.text = item.itemName;

        List<CraftResourceSlotController> temp = new List<CraftResourceSlotController>(craftResourcesSlots);
        craftResourcesSlots.Clear();
        foreach (var slot in temp) Destroy(slot.gameObject);
        
        for (int i = 0; i < item.resourcesToCraft.Count; i++)
        {
            craftResourcesSlots.Add(Instantiate(craftResourceSlotPrefab, craftResourceSlotParent)
                .GetComponent<CraftResourceSlotController>());
            ItemData itemData = database.items[item.resourcesToCraft[i].resourceId];
            craftResourcesSlots[i].InitSlot(item.resourcesToCraft[i].resourceCost, itemData.itemSprite);
        }
    }
}
