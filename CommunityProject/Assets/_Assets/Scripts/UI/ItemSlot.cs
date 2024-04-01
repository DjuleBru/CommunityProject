using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    protected Canvas canvas;
    protected CanvasGroup group;
    protected RectTransform rectTransform;

    [SerializeField] protected Image itemSlotImage;
    [SerializeField] protected TextMeshProUGUI itemSlotAmountText;

    protected Item item;
    protected Inventory parentInventory;

    protected virtual void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        group = GetComponentInChildren<CanvasGroup>();

        //Awake runs after SetItem
        if (item == null) {
            itemSlotImage.enabled = false;
        }
    }

    public void SetItem(Item item) {
        this.item = item;
        SetItemSlotVisuals(item);
    }

    public void SetParentInventory(Inventory parentInventory) {
        this.parentInventory = parentInventory;
    }

    public Inventory GetParentInventory() {
        return parentInventory;
    }

    public void ResetItem() {
        this.item = null;
        itemSlotImage.enabled = false;
        itemSlotAmountText.text = string.Empty;
    }

    protected virtual void SetItemSlotVisuals(Item item) {
        itemSlotImage.enabled = true;
        itemSlotImage.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;

        if (item.amount > 1) {
            itemSlotAmountText.text = item.amount.ToString();
        }
        else {
            itemSlotAmountText.text = "";
        }
    }
}
