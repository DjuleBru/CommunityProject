using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HumanoidEquipItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Button button;

    public static event EventHandler OnAnyEquipItemHovered;
    public static event EventHandler OnAnyEquipItemPressed;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Item.ItemType itemTypeToEquip = GetComponent<ItemSlot>().GetItem().itemType;
            Item itemToEquip = new Item { itemType = itemTypeToEquip, amount = 0};

            HumanoidUI.Instance.SetItemToEquip(itemToEquip);
            EquipmentTooltipUI.Instance.EnableToolTip(false);
        });
    }

    public void OnPointerExit(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.EnableToolTip(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.SetToolTip(GetComponent<ItemSlot>().GetItem());
        EquipmentTooltipUI.Instance.EnableToolTip(true);
        OnAnyEquipItemHovered?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnAnyEquipItemPressed?.Invoke(this, EventArgs.Empty);
    }
}
