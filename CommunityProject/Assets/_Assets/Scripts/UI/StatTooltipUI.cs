using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatTooltipUI : MonoBehaviour
{
    public static StatTooltipUI Instance;

    private Canvas parentCanvas;
    [SerializeField] private GameObject tooltipGameObject;

    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statDescription;
    [SerializeField] private Image statIcon;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Vector2 pos;
        parentCanvas = GetComponentInParent<Canvas>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);

        tooltipGameObject.SetActive(false);
    }

    private void Update() {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        tooltipGameObject.transform.position = parentCanvas.transform.TransformPoint(movePos);
    }


    public void EnableToolTip(bool enabled) {
        tooltipGameObject.SetActive(enabled);
    }

    public void SetTooltip(Sprite statSprite, string statName, string statDescription) {
        statIcon.sprite = statSprite;
        this.statName.text = statName;
        this.statDescription.text = statDescription;
    }
}
