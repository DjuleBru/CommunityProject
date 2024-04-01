using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour {
    private Inventory inventory;

    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    [SerializeField] private Transform emptyItemSlotTemplate;
    [SerializeField] private TransferItemsUI transferItemsUI;

    [SerializeField] private GameObject inventoryPanel;
    private bool opened;

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();

        if (inventory.HasLimitedSlots()) {
            transferItemsUI.gameObject.SetActive(false);
        }
        inventoryPanel.SetActive(false);
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryUI();
    }

    private void RefreshInventoryUI() {
        foreach(Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            if(child == emptyItemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        if (!inventory.HasLimitedSlots()) {
            RefreshUnlimitedInventoryUI();
        } else {
            RefreshLimitedInventoryUI();
        }
    }

    private void RefreshUnlimitedInventoryUI() {
        
        // Inventory is unlimited
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            ItemSlot inventoryItemSlot = itemSlotRectTransform.GetComponent<ItemSlot>();
            inventoryItemSlot.SetItem(item);
            inventoryItemSlot.SetParentInventory(inventory);
        }
    }

    private void RefreshLimitedInventoryUI() {
        int itemNumber = 0;

        for(int x = 0;  x < inventory.GetSlotNumberX(); x++) {
            for(int y = 0;  y < inventory.GetSlotNumberY(); y++) {

                RectTransform itemSlotRectTransform = Instantiate(emptyItemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                itemSlotRectTransform.gameObject.SetActive(true);

                if (itemNumber < inventory.GetItemList().Count) {
                    // The item slot is filled with an item
                    Item item = inventory.GetItemList()[itemNumber];

                    ItemSlot inventoryItemSlot = itemSlotRectTransform.GetComponent<ItemSlot>();
                    inventoryItemSlot.SetItem(item);
                    inventoryItemSlot.SetParentInventory(inventory);
                }
                itemNumber++;
            }
        }
    }

    public Inventory GetInventory() {
        return inventory;
    }

    public void OpenTransferItemsPanelGameObject() {
        if (inventory.HasLimitedSlots()) {
            transferItemsUI.gameObject.SetActive(true);
        }
    }

    public void CloseTransferItemsPanelGameObject() {
        transferItemsUI.ResetItemToTransfer();
        transferItemsUI.gameObject.SetActive(false);
    }

    public void OpenCloseInventoryPanel() {
        inventoryPanel.SetActive(!opened);
        opened = !opened;
    }
}
