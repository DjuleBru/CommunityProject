using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemSlot_Inventory : ItemSlot, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {


    private Vector2 initialPosition;
    private InventoryUI parentInventoryUI;

    InventoryUI previousInventoryUIDraggedOn;
    private bool draggedOnInventory;

    [SerializeField] private bool disableInteraction;

    protected override void Awake() {
        base.Awake();
        parentInventoryUI = GetComponentInParent<InventoryUI>();

        if(disableInteraction) {
            group.blocksRaycasts = false;
        }

    }

    public void OnBeginDrag(PointerEventData eventData) {
        group.blocksRaycasts = false;
        group.alpha = .6f;
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        InventoryUI inventoryUIDraggedOn = GetInventoryUIDraggedOn();

        if (inventoryUIDraggedOn != null) {
            if (inventoryUIDraggedOn == parentInventoryUI) return;

            if(!draggedOnInventory) {
                // ItemSlot enters inventory UI
                if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
                    // Item is stackable : oepn transfer items panel
                    inventoryUIDraggedOn.OpenTransferItemsPanelGameObject();
                }

                previousInventoryUIDraggedOn = inventoryUIDraggedOn;
                draggedOnInventory = true;
            }

        } else {
            if (GetTransferItemsUIDraggedOn() != null) return;
            // Player is dragging on transferItemsPanel

            if (draggedOnInventory) {
                // ItemSlot exits inventory UI
                previousInventoryUIDraggedOn.CloseTransferItemsPanelGameObject();
                previousInventoryUIDraggedOn = null;
            }
            draggedOnInventory = false;
        };
    }

    public void OnEndDrag(PointerEventData eventData) {
        group.blocksRaycasts = true;
        group.alpha = 1f;

        Inventory inventoryDraggedOn = GetInventoryDraggedOn();

        if(inventoryDraggedOn != null) {
            // Dragged on an inventory
            if(inventoryDraggedOn != parentInventoryUI.GetInventory()) {
                // Dragged on another inventory : Transfer item from inventory to another
                TransferItemBetweenInventories(inventoryDraggedOn);
                GetInventoryUIDraggedOn().CloseTransferItemsPanelGameObject();
            } else {
                // Dragged on this inventory : Reset position
                rectTransform.anchoredPosition = initialPosition;
            }
        } else {
            // Dropped on something else than an inventory
            TransferItemsUI transferItemsUIPanel = GetTransferItemsUIDraggedOn();
            if (transferItemsUIPanel != null) {
                // Dropped on transfer UI Panel : Set Item to transfer then Reset position
                transferItemsUIPanel.SetItemToTransfer(item, parentInventory);
                rectTransform.anchoredPosition = initialPosition;

            } else {
                // Dropped on floor
                Item droppedItem = new Item { itemType = item.itemType, amount = item.amount };
                parentInventoryUI.GetInventory().RemoveItemStack(item);
                ItemWorld.DropItem(Player.Instance.transform.position, droppedItem, true);
            }
        }
    }

    private void TransferItemBetweenInventories(Inventory newInventory) {
        Item transferedItem = new Item { itemType = item.itemType, amount = item.amount };

        if (newInventory.HasSpaceForItem(transferedItem)) {
            parentInventoryUI.GetInventory().RemoveItemStack(item);
            newInventory.AddItem(transferedItem);

        }
        else {
            //Reset position
            rectTransform.anchoredPosition = initialPosition;
        }
    }

    private Inventory GetInventoryDraggedOn() {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        Inventory newInventory = null;

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                if (go.gameObject.GetComponent<InventoryUI>()) {
                    // Dropped on any kind of inventory
                    newInventory = go.gameObject.GetComponent<InventoryUI>().GetInventory();
                }
            }
        }

        return newInventory;
    }
    private InventoryUI GetInventoryUIDraggedOn() {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        InventoryUI newInventoryUI = null;

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                if (go.gameObject.GetComponent<InventoryUI>()) {
                    // Dropped on any kind of inventory
                    newInventoryUI = go.gameObject.GetComponent<InventoryUI>();
                }
            }
        }

        return newInventoryUI;
    }

    private TransferItemsUI GetTransferItemsUIDraggedOn() {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        TransferItemsUI transferItemsUI = null;

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                if (go.gameObject.GetComponent<TransferItemsUI>()) {
                    // Dragged on transferItemsUI
                    transferItemsUI = go.gameObject.GetComponent<TransferItemsUI>();
                }
            }
        }

        return transferItemsUI;
    }

    public void OnPointerDown(PointerEventData eventData) {

    }

    public void OnDrop(PointerEventData eventData) {
        // Drop Item
    }

}
