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
    [SerializeField] protected TransferItemsUI transferItemsUI;

    [SerializeField] protected GameObject inventoryPanel;
    protected bool opened;

    protected Image interactionImage;
    protected GridLayout gridLayout;

    protected virtual void Awake() {
         interactionImage = GetComponent<Image>();
         interactionImage.enabled = false;
    }

    public virtual void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();

        transferItemsUI.gameObject.SetActive(false);

        inventoryPanel.SetActive(false);
    }

    protected void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryUI();
    }

    protected void RefreshInventoryUI() {
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

    protected void RefreshLimitedInventoryUI() {
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

    public void OpenTransferItemsPanelGameObject() {
        if (inventory.HasLimitedSlots()) {
            transferItemsUI.gameObject.SetActive(true);
        }
    }

    public void CloseTransferItemsPanelGameObject() {
        transferItemsUI.ResetItemToTransfer();
        transferItemsUI.gameObject.SetActive(false);
    }

    //[Button]
    //public void RefreshInventorySize() {
    //    float yPosition = 0;
    //    int rowNumber = 0;

    //    foreach(RectTransform slot in itemSlotContainer) {
    //        if(slot.position.y != yPosition) {
    //            rowNumber++;
    //            yPosition = slot.position.y;
    //        }
    //    }

    //    float height = (rowNumber +1) * itemSlotContainer.GetComponent<GridLayoutGroup>().cellSize.y;

    //    GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, height);
    //}

    public void OpenCloseInventoryPanel() {
        inventoryPanel.SetActive(!opened);
        interactionImage.enabled = !opened;
        opened = !opened;
    }
}
