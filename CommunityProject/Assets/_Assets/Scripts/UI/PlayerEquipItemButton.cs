using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEquipItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Button button;


    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Item itemToEquip = GetComponent<ItemSlot>().GetItem();

            EquipmentMenuUI.Instance.SetItemToEquip(itemToEquip);
            EquipmentTooltipUI.Instance.EnableToolTip(false);
        });
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Item.ItemEquipmentCategory equipmentCategory = ItemAssets.Instance.GetItemSO(GetComponent<ItemSlot>().GetItem().itemType).itemEquipmentCategory;

        EquipmentTooltipUI.Instance.SetToolTip(GetComponent<ItemSlot>().GetItem());
        EquipmentTooltipUI.Instance.EnableToolTip(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.EnableToolTip(false);
    }
}
