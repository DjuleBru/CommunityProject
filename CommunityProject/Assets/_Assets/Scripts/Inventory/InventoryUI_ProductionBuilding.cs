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
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();
    }


    public override void OpenTransferItemsPanelGameObject() {
    }

    public override void CloseTransferItemsPanelGameObject() {
    }
}
