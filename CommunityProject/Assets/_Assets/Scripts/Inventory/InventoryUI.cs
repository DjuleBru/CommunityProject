using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class InventoryUI : MonoBehaviour {
    protected Inventory inventory;

    [SerializeField] protected Transform itemSlotContainer;
    [SerializeField] protected Transform itemSlotTemplate;
    protected bool opened;

    protected Image interactionImage;
    protected GridLayout gridLayout;

    protected virtual void Awake() {
        interactionImage = GetComponent<Image>();
    }

    public virtual void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();
    }

    public virtual void ReplaceInventory(Inventory inventory) {
        if(inventory != null) {
            inventory.OnItemListChanged -= Inventory_OnItemListChanged;
        }
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();
    }

    protected void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryUI();
    }

    protected virtual void RefreshInventoryUI() {
        foreach(Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        if (!inventory.HasLimitedSlots()) {
            RefreshUnlimitedInventoryUI();
        } else {
            RefreshLimitedInventoryUI();
        }
    }

    protected void RefreshUnlimitedInventoryUI() {
        // Inventory is unlimited
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            ItemSlot inventoryItemSlot = itemSlotRectTransform.GetComponent<ItemSlot>();
            inventoryItemSlot.SetItem(item);
            inventoryItemSlot.SetParentInventory(inventory);
        }
    }

    protected virtual void RefreshLimitedInventoryUI() {
        int itemNumber = 0;

        for(int x = 0;  x < inventory.GetSlotNumberX(); x++) {
            for(int y = 0;  y < inventory.GetSlotNumberY(); y++) {

                RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
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

    private void OnDestroy() {
        if(inventory != null) {
            inventory.OnItemListChanged -= Inventory_OnItemListChanged;
        }
    }

}
