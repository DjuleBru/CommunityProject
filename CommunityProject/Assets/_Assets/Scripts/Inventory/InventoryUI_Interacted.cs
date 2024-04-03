using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI_Interacted : InventoryUI
{

    public static InventoryUI_Interacted Instance { get; private set; }
    [SerializeField] private Animator transferAllItemsAnimator;

    private bool transferAllItemsPanelOpen;

    protected override void Awake() {
        Instance = this;
        gameObject.SetActive(false);

        transferItemsUI.gameObject.SetActive(false);
    }

    private void Update() {
        if (inventory.GetItemList().Count == 0) return;
        // There are no items to transfer

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                if (go.gameObject.GetComponentInParent<InventoryUI_Interacted>()) {
                    // Hovering this inventory
                    if (transferAllItemsPanelOpen) return;
                    transferAllItemsAnimator.gameObject.SetActive(true);
                    transferAllItemsPanelOpen = true;
                }
            }
        } else {
            if (!transferAllItemsPanelOpen) return;
            transferAllItemsPanelOpen = false;
            transferAllItemsAnimator.gameObject.SetActive(false);
            transferAllItemsPanelOpen = false;
        }
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
}
