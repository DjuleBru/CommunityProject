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

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        group = GetComponentInChildren<CanvasGroup>();
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

        if (eventData.pointerCurrentRaycast.gameObject == null) {
            // Drop item
            rectTransform.anchoredPosition = initialPosition;

        } else {
            // Reset position
            rectTransform.anchoredPosition = initialPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        
    }

    public void OnDrop(PointerEventData eventData) {
        // Drop Item
    }
}
