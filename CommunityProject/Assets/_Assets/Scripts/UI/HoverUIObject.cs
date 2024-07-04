using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    protected Animator slotAnimator;

    private void Awake() {
        slotAnimator = GetComponent<Animator>();
    }
    public void OnPointerExit(PointerEventData eventData) {
        slotAnimator.SetTrigger("Hover_Out");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        slotAnimator.SetTrigger("Hover_In");
    }
}
