using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI_Interacted : InventoryUI
{
    [SerializeField] private Animator transferAllItemsAnimator;
    [SerializeField] protected TransferItemsUI transferItemsUI;
    [SerializeField] protected GameObject inventoryPanel;

    [SerializeField] protected bool canReceiveItems;

    private bool transferAllItemsPanelOpen;

    protected override void Awake() {
        base.Awake();
        gameObject.SetActive(false);

        transferItemsUI.gameObject.SetActive(false);
    }

    private void Update() {
        HandleTransferAllItemsPanel();
    }
    
    private void HandleTransferAllItemsPanel() {
        if (inventory.GetItemList().Count == 0) return;
        // There are no items to transfer

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                InventoryUI_Interacted interactableInventory = go.gameObject.GetComponentInParent<InventoryUI_Interacted>();
                if (interactableInventory != null) {
                    // Hovering an interactable inventory

                    if (interactableInventory == this) {
                        // Hovering this inventory

                        if (transferAllItemsPanelOpen) return;
                        transferAllItemsAnimator.gameObject.SetActive(true);
                        transferAllItemsPanelOpen = true;
                    }
                    else {
                        CloseTransferAllItemsPanel();
                    }
                }
                else {
                    CloseTransferAllItemsPanel();
                }
            }
        }
        else {
            CloseTransferAllItemsPanel();
        }
    }

    private void CloseTransferAllItemsPanel() {
        if (!transferAllItemsPanelOpen) return;
        transferAllItemsPanelOpen = false;
        transferAllItemsAnimator.gameObject.SetActive(false);
        transferAllItemsPanelOpen = false;
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

    public void OpenTransferItemsPanelGameObject() {
        transferItemsUI.gameObject.SetActive(true);
    }

    public void CloseTransferItemsPanelGameObject() {
        transferItemsUI.ResetItemToTransfer();
        transferItemsUI.gameObject.SetActive(false);
    }

    public void OpenCloseInventoryPanel() {
        gameObject.SetActive(!opened);
        inventoryPanel.SetActive(!opened);
        interactionImage.enabled = !opened;
        opened = !opened;
    }

    public void CloseInventoryPanel() {
        gameObject.SetActive(false);
        inventoryPanel.SetActive(false);
        interactionImage.enabled = false;
        opened = false;
    }

    public void TransferAllItems() {
        List<Item> itemsToRemove = new List<Item>();

        foreach(Item item in inventory.GetItemList()) {
            ItemSO itemToTransferSO = ItemAssets.Instance.GetItemSO(item.itemType);
            Item itemToTransfer = new Item { itemType = itemToTransferSO.itemType, amount = item.amount };

            if(Player.Instance.GetInventory().HasSpaceForItem(itemToTransfer)) {
                Player.Instance.GetInventory().AddItem(itemToTransfer);
                itemsToRemove.Add(item);
            } else {
                break;
            }
        }

        foreach(Item item in itemsToRemove) {
            inventory.RemoveItemStack(item);
        }
    }

    public bool GetCanReceiveItems() {
        return canReceiveItems;
    }
}
