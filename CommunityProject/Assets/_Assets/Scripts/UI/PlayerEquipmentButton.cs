using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEquipmentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image equipmentImage;
    [SerializeField] private Sprite defaultEquipmentSprite;
    [SerializeField] private Item.ItemEquipmentCategory category;

    [SerializeField] private Image equipmentDurabilityBarFill;
    [SerializeField] private TextMeshProUGUI equipmentAmountText;

    [SerializeField] private Color defaultEquipmentColor;

    public static PlayerEquipmentButton lastButtonPressed;
    private bool inventoryOpen;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {

            if (lastButtonPressed != null && lastButtonPressed == this) {
                inventoryOpen = !inventoryOpen;
                if (inventoryOpen) {
                    EquipmentMenuUI.Instance.OpenInventoryUI(category, this);
                }
                else {
                    EquipmentMenuUI.Instance.CloseInventoryUI();
                }
            }
            else {
                lastButtonPressed = this;
                EquipmentMenuUI.Instance.OpenInventoryUI(category, this);
                inventoryOpen = true;
            }
        });
    }

    private void Update() {
        if (PlayerEquipment.Instance.GetEquipmentItem(category) != null) {
            equipmentDurabilityBarFill.fillAmount = PlayerEquipment.Instance.GetEquipmentItemDurabilityNormalized(category);
        }
        else {
            equipmentDurabilityBarFill.fillAmount = 0;
        }
    }

    public void SetItem(Item item) {
        if (item == null) {
            equipmentImage.sprite = defaultEquipmentSprite;
            equipmentImage.color = defaultEquipmentColor;
            equipmentAmountText.text = "";
        }
        else {

            equipmentImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
            equipmentImage.color = Color.white;

            equipmentAmountText.text = item.amount.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.EnableToolTip(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (PlayerEquipment.Instance.GetEquipmentItem(category) == null) return;

        EquipmentTooltipUI.Instance.SetToolTip(PlayerEquipment.Instance.GetEquipmentItem(category));
        EquipmentTooltipUI.Instance.EnableToolTip(true);
    }
}
