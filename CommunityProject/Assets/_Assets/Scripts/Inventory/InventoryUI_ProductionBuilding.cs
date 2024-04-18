using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI_ProductionBuilding : InventoryUI_Interactable
{
    protected override void Awake() {
        interactionImage = GetComponent<Image>();
        interactionImage.enabled = true;
    }

    protected override void Update() {

    }

    public override void SetInventory(Inventory inventory) {

        if (inventory != null) {
            this.inventory = inventory;

            inventory.OnItemListChanged += Inventory_OnItemListChanged;
            RefreshInventoryUI();

        } else {
            foreach(Transform child in itemSlotContainer) {
                if (child.GetComponent<ItemSlot_Inventory>() != null) {
                    if (child != itemSlotTemplate) {
                        Destroy(child.gameObject);
                    }
                }

            }
        }
    }

    protected override void RefreshInventoryUI() {
        if (itemSlotContainer == null) return;

        foreach (Transform child in itemSlotContainer) {
            if (child == null) continue;
            if (child.GetComponent<ItemSlot_Inventory>() != null) {
                if (child != itemSlotTemplate) {
                    Destroy(child.gameObject);
                }
            }
        }

        if (!inventory.HasLimitedSlots()) {
            RefreshUnlimitedInventoryUI();
        }
        else {
            RefreshLimitedInventoryUI();
        }
    }

    protected override void RefreshLimitedInventoryUI() {
        int itemNumber = 0;

        for (int x = 0; x < inventory.GetSlotNumberX(); x++) {
            for (int y = 0; y < inventory.GetSlotNumberY(); y++) {

                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);

                if (itemNumber < inventory.GetItemList().Count) {
                    // The item slot is filled with an item
                    Item item = inventory.GetItemList()[itemNumber];

                    ItemSlot inventoryItemSlot = itemSlotRectTransform.GetComponentInChildren<ItemSlot>();
                    inventoryItemSlot.SetItem(item);
                    inventoryItemSlot.SetParentInventory(inventory);
                } else {
                    // The item slot is empty : put item icon in transparency

                    ItemSlot_ProductionBuilding inventoryItemSlot = itemSlotRectTransform.GetComponentInChildren<ItemSlot_ProductionBuilding>();
                    Item restrictedItem = inventory.GetRestrictedItemList()[0];
                    inventoryItemSlot.SetItemSlotIcon(restrictedItem);

                }
                itemNumber++;
            }
        }
    }

    public override void OpenTransferItemsPanelGameObject() {
    }

    public override void CloseTransferItemsPanelGameObject() {
    }
}
