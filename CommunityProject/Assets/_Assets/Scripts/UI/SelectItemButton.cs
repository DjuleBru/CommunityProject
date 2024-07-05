using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Item item;

    [SerializeField] private Image itemImage;

    public static event EventHandler OnAnySelectItemButtonHovered;
    public static event EventHandler OnAnySelectItemButtonPressed;

    public void SetItem(Item item) {
        this.item = item;
        itemImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
    }

    public void SelectItem() {
        SelectItemUI.Instance.SelectItem(item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        throw new NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        OnAnySelectItemButtonHovered?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnAnySelectItemButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}
