using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HumanoidEquipmentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private Image equipmentImage;
    [SerializeField] private Sprite defaultEquipmentSprite;
    [SerializeField] private GameObject fetchingImage;
    [SerializeField] private Item.ItemEquipmentCategory category;

    [SerializeField] private Image equipmentDurabilityBarFill;
    [SerializeField] private TextMeshProUGUI equipmentAmountText;

    [SerializeField] private Color defaultEquipmentColor;

    public static HumanoidEquipmentButton lastButtonPressed;
    private bool inventoryOpen;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {

            if (lastButtonPressed != null && lastButtonPressed == this) {
                inventoryOpen = !inventoryOpen;
                if (inventoryOpen) {
                    HumanoidUI.Instance.OpenInventoryUI(category, this);
                }
                else {
                    HumanoidUI.Instance.CloseInventoryUI();
                }
            }
            else {
                lastButtonPressed = this;
                HumanoidUI.Instance.OpenInventoryUI(category, this);
                inventoryOpen = true;
            }
        });
        fetchingImage.SetActive(false);
    }

    private void Update() {
        if (HumanoidUI.Instance.GetHumanoid().GetEquipmentItem(category) != null) {
            equipmentDurabilityBarFill.fillAmount = HumanoidUI.Instance.GetHumanoid().GetEquipmentItemDurabilityNormalized(category);
        } else {
            equipmentDurabilityBarFill.fillAmount = 0;
        }
    }

    public void SetItem(Item item) {
        if(item == null) {
            equipmentImage.sprite = defaultEquipmentSprite;
            equipmentImage.color = defaultEquipmentColor;
            fetchingImage.SetActive(false);
            equipmentAmountText.text = "";
        } else {

            if(item.amount == 0) {
                fetchingImage.SetActive(true);
            } else {
                fetchingImage.SetActive(false);
            }

            equipmentImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
            equipmentImage.color = Color.white;

            equipmentAmountText.text = item.amount.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        EquipmentTooltipUI.Instance.EnableToolTip(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (HumanoidUI.Instance.GetHumanoid().GetEquipmentItem(category) == null) return;

        EquipmentTooltipUI.Instance.SetToolTip(HumanoidUI.Instance.GetHumanoid().GetEquipmentItem(category));
        EquipmentTooltipUI.Instance.EnableToolTip(true);
    }
}
