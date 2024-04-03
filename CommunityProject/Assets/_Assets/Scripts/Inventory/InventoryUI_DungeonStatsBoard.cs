using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_DungeonStatsBoard : InventoryUI
{
    protected override void Awake() {
    }

    public override void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();
    }
}
