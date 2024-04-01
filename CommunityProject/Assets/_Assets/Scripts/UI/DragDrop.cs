using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

    private Canvas canvas;
    private CanvasGroup group;
    private RectTransform rectTransform;

    private Vector2 initialPosition;
    private InventoryUI parentInventoryUI;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        group = GetComponentInChildren<CanvasGroup>();

        parentInventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        group.blocksRaycasts = false;
        group.alpha = .6f;
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        group.blocksRaycasts = true;
        group.alpha = 1f;

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0) {
            foreach (var go in raycastResults) {
                if(go.gameObject.GetComponent<InventoryUI>()) {
                    // Dropped on any kind of inventory
                    if(go.gameObject == parentInventoryUI.gameObject) {
                        rectTransform.anchoredPosition = initialPosition;
                    } else {
                        // Transfer item from inventory to another

                    }

                } else {
                    // Not dropped on inventory : Drop Item
                    //parentInventoryUI.GetInventory().RemoveItem();
                }
            }
        }

        if (eventData.pointerCurrentRaycast.gameObject == null) {
            // Drop item
            Debug.Log("Item drop");
            rectTransform.anchoredPosition = initialPosition;

        } else {
            // Reset position
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        
    }

    public void OnDrop(PointerEventData eventData) {
        // Drop Item
    }
}
