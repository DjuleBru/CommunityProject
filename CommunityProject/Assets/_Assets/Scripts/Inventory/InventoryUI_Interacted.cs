using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Interacted : InventoryUI
{

    public static InventoryUI_Interacted Instance { get; private set; }

    protected void Awake() {
        Instance = this;

        gameObject.SetActive(false);
    }

    public override void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();

        if (inventory.HasLimitedSlots()) {
            transferItemsUI.gameObject.SetActive(false);
        }
        inventoryPanel.SetActive(true);
    }
}
