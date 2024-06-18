using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidEquipmentButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private Image equipmentImage;
    [SerializeField] private Sprite defaultEquipmentSprite;
    [SerializeField] private GameObject fetchingImage;
    [SerializeField] private Item.ItemEquipmentCategory category;

    [SerializeField] private Color defaultEquipmentColor;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            HumanoidUI.Instance.OpenInventoryUI(category, this);
        });
        fetchingImage.SetActive(false);
    }

    public void SetItem(Item item) {
        if(item == null) {
            equipmentImage.sprite = defaultEquipmentSprite;
            equipmentImage.color = defaultEquipmentColor;
            fetchingImage.SetActive(false);
        } else {

            if(item.amount == 0) {
                fetchingImage.SetActive(true);
            } else {
                fetchingImage.SetActive(false);
            }

            equipmentImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
            equipmentImage.color = Color.white;
        }
    }

}
