using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftSlotController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image imgBg;
    [SerializeField] private Image imgSprite;
    [SerializeField] private Color colorSelection;
    private CraftUiController craftUi;

    public ItemData itemToCraft;
    public int itemId;

    public void InitSlot(CraftUiController craftUiController)
    {
        craftUi = craftUiController;
        imgSprite.sprite = itemToCraft.itemSprite;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        craftUi.OpenCraftInfo(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgBg.color = colorSelection;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgBg.color = Color.white;
    }
}
