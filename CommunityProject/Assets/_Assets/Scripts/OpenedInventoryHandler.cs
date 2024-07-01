using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedInventoryHandler : MonoBehaviour
{
    public static OpenedInventoryHandler Instance;
    private List<InventoryUI> openedInventories = new List<InventoryUI>();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        InventoryUI.OnAnyInventoryClosed += InventoryUI_OnAnyInventoryClosed;
        InventoryUI.OnAnyInventoryOpened += InventoryUI_OnAnyInventoryOpened;
    }

    private void InventoryUI_OnAnyInventoryOpened(object sender, System.EventArgs e) {
        openedInventories.Add(sender as InventoryUI);
    }

    private void InventoryUI_OnAnyInventoryClosed(object sender, System.EventArgs e) {
        if (!openedInventories.Contains(sender as InventoryUI)) return;
        openedInventories.Remove(sender as InventoryUI);
    }

    public InventoryUI GetOtherInventoryOpened(InventoryUI inventoryUI) {
        foreach(InventoryUI inventory in openedInventories) {
            if (inventory == inventoryUI) continue;
            return inventory;
        }
        return null;
    }
}
