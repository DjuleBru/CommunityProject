using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot_ProductionBuilding : ItemSlot_Inventory
{
    [SerializeField] private Image transparentItemIcon;

    public void SetItemSlotIcon(Item item) {
        transparentItemIcon.gameObject.SetActive(true);
        transparentItemIcon.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
    }

    protected override void SetItemSlotVisuals(Item item) {
        transparentItemIcon.gameObject.SetActive(false);
        itemSlotImage.enabled = true;
        itemSlotImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;

        if (item.amount > 1) {
            itemSlotAmountText.text = item.amount.ToString();
        }
        else {
            itemSlotAmountText.text = "";
        }
    }
}
