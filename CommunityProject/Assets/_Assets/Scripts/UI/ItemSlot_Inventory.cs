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

                if (inventoryUIDraggedOn is InventoryUI_Interacted) {
                    InventoryUI_Interacted interactableInventoryDraggedOn = inventoryUIDraggedOn as InventoryUI_Interacted;

                    if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
                        // Item is stackable : oepn transfer items panel
                        interactableInventoryDraggedOn.OpenTransferItemsPanelGameObject();
                    }
                }

                previousInventoryUIDraggedOn = inventoryUIDraggedOn;
                draggedOnInventory = true;
            }

        } else {
            if (GetTransferItemsUIDraggedOn() != null) return;
            // Player is dragging on transferItemsPanel

            if (draggedOnInventory) {
                // ItemSlot exits inventory UI
                if (inventoryUIDraggedOn is InventoryUI_Interacted) {
                    InventoryUI_Interacted interactableInventoryDraggedOn = inventoryUIDraggedOn as InventoryUI_Interacted;
                    interactableInventoryDraggedOn.CloseTransferItemsPanelGameObject();
                    previousInventoryUIDraggedOn = null;
                }

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
            if(inventoryDraggedOn != parentInventoryUI.GetInventory() && GetInventoryUI_InteractedDraggedOn() != null && GetInventoryUI_InteractedDraggedOn().GetCanReceiveItems()) {
                // Dragged on another inventory, interactable inventory that can receive items
                    TransferItemBetweenInventories(inventoryDraggedOn);
                    SetParentInventoryUI(GetInventoryUI_InteractedDraggedOn());
                    GetInventoryUI_InteractedDraggedOn().CloseTransferItemsPanelGameObject();
            } else {
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

    private InventoryUI_Interacted GetInventoryUI_InteractedDraggedOn() {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        InventoryUI_Interacted newInventoryUI = null;

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                if (go.gameObject.GetComponent<InventoryUI_Interacted>()) {
                    // Dropped on any kind of inventory
                    newInventoryUI = go.gameObject.GetComponent<InventoryUI_Interacted>();
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

    private void SetParentInventoryUI(InventoryUI inventoryUI) {
        parentInventoryUI = inventoryUI;
    }

    public void OnPointerDown(PointerEventData eventData) {

    }

    public void OnDrop(PointerEventData eventData) {
        // Drop Item
    }

}
