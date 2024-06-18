using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidEquipItemButton : MonoBehaviour
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
}
