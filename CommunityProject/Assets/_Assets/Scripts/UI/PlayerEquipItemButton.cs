using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEquipItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Button button;

    public static event EventHandler OnAnyPlayerEquipmentItemEquipped;
    public static event EventHandler OnAnyPlayerEquipmentItemHovered;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Item itemToEquip = GetComponent<ItemSlot>().GetItem();

            EquipmentMenuUI.Instance.SetItemToEquip(itemToEquip);
            EquipmentTooltipUI.Instance.EnableToolTip(false);
            OnAnyPlayerEquipmentItemEquipped?.Invoke(this, EventArgs.Empty);
        });
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Item.ItemEquipmentCategory equipmentCategory = ItemAssets.Instance.GetItemSO(GetComponent<ItemSlot>().GetItem().itemType).itemEquipmentCategory;

        EquipmentTooltipUI.Instance.SetToolTip(GetComponent<ItemSlot>().GetItem());
        EquipmentTooltipUI.Instance.EnableToolTip(true);
        OnAnyPlayerEquipmentItemHovered?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.EnableToolTip(false);
    }
}
