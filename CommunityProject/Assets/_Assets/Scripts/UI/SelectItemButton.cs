using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    private Item item;

    [SerializeField] private Image itemImage;


    public void SetItem(Item item) {
        this.item = item;
        itemImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
    }

    public void SelectItem() {
        SelectItemUI.Instance.SelectItem(item);
    }

}
