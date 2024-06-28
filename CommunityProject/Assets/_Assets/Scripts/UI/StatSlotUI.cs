using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private Image statImage;
    [SerializeField] private string statName;
    [SerializeField] private string statDescription;

    public void OnPointerEnter(PointerEventData eventData) {
        StatTooltipUI.Instance.EnableToolTip(true);
        StatTooltipUI.Instance.SetTooltip(statImage.sprite, statName, statDescription);
    }

    public void OnPointerExit(PointerEventData eventData) {
        StatTooltipUI.Instance.EnableToolTip(false);
    }
}
