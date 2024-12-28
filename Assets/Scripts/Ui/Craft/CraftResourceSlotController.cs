using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftResourceSlotController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtResourceCost;
    [SerializeField] private Image imgSprite;

    public void InitSlot(int cost, Sprite sprite)
    {
        txtResourceCost.text = cost.ToString();
        imgSprite.sprite = sprite;
    }
}