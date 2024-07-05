using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI_SFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public static event EventHandler OnAnyButtonUIHover;
    public static event EventHandler OnAnyButtonUIClick;

    public void OnPointerClick(PointerEventData eventData) {
        OnAnyButtonUIClick?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        OnAnyButtonUIHover?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerExit(PointerEventData eventData) {

    }
}
