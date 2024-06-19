using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemsPanel : MonoBehaviour
{

    [SerializeField] protected Transform itemSlotContainer;
    [SerializeField] protected Transform itemSlotTemplate;

    private void Start() {
        RefreshUnlimitedInventoryUI();
    }

    protected void RefreshUnlimitedInventoryUI() {
        // Inventory is unlimited
        foreach (ItemSO itemSO in ItemAssets.Instance.GetItemSOList()) {


            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            ItemSlot inventoryItemSlot = itemSlotRectTransform.GetComponent<ItemSlot>();

            Item debugItem = new Item { itemType = itemSO.itemType, amount = 1 };
            if (itemSO.isStackable) {
                debugItem = new Item { itemType = itemSO.itemType, amount = itemSO.maxStackableAmount };
            }

            inventoryItemSlot.SetItem(debugItem);

            itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            inventoryItemSlot = itemSlotRectTransform.GetComponent<ItemSlot>();

            debugItem = new Item { itemType = itemSO.itemType, amount = 1 };
            if (itemSO.isStackable) {
                debugItem = new Item { itemType = itemSO.itemType, amount = 1 };
            }

            inventoryItemSlot.SetItem(debugItem);
        }
    }
}
