using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HumanoidEquipItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Item.ItemType itemTypeToEquip = GetComponent<ItemSlot>().GetItem().itemType;
            Item itemToEquip = new Item { itemType = itemTypeToEquip, amount = 0};

            HumanoidUI.Instance.SetItemToEquip(itemToEquip);
        });
    }

    public void OnPointerExit(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.EnableToolTip(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.SetToolTip(GetComponent<ItemSlot>().GetItem());
        EquipmentTooltipUI.Instance.EnableToolTip(true);
    }
}
