using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransferItemsUI : MonoBehaviour
{
    [SerializeField] ItemSlot_Transfer itemSlotTransfer;
    [SerializeField] InventoryUI parentInventoryUI;
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Slider amountSlider;

    private Item item;
    private Inventory itemOriginInventory;
    private int maxItemAmount;
    private int itemTransferAmount;

    public void SetItemToTransfer(Item item, Inventory itemOriginInventory) {
        this.item = item;

        this.itemOriginInventory = itemOriginInventory;
        itemSlotTransfer.SetItem(item);
        maxItemAmount = item.amount;
    }

    public void ResetItemToTransfer() {
        item = null;
        itemSlotTransfer.ResetItem();
    }

    public void TransferItem() {
        Item itemToTransfer = new Item { itemType = item.itemType, amount = itemTransferAmount };

        parentInventoryUI.GetInventory().AddItem(itemToTransfer);
        itemOriginInventory.RemoveItemAmount(itemToTransfer);

        ResetItemToTransfer();
        parentInventoryUI.CloseTransferItemsPanelGameObject();
    }

    public void SliderUpdateItemAmount() {
        itemTransferAmount = (int)(amountSlider.value * maxItemAmount);

        if(itemTransferAmount == 0) {
            itemTransferAmount = 1;
        }

        UpdateItemAmountText(itemTransferAmount);
    }

    public void SetHalfItemAmount() {
        itemTransferAmount = (int)(maxItemAmount / 2f);

        UpdateItemAmountText(itemTransferAmount);
        float sliderValue = (float)itemTransferAmount/(float)maxItemAmount;
        UpdateItemAmountSlider(sliderValue);
    }

    public void SetMinItemAmount() {
        itemTransferAmount = 1;
        UpdateItemAmountText(itemTransferAmount);

        float sliderValue = 0;
        UpdateItemAmountSlider(sliderValue);
    }

    public void SetMaxItemAmount() {
        itemTransferAmount = maxItemAmount;

        UpdateItemAmountText(itemTransferAmount);
        float sliderValue = 1;
        UpdateItemAmountSlider(sliderValue);
    }

    public void AddItemAmount() {
        itemTransferAmount++;

        UpdateItemAmountText(itemTransferAmount);
        float sliderValue = (float)itemTransferAmount / (float)maxItemAmount;
        UpdateItemAmountSlider(sliderValue);
    }

    public void RemoveItemAmount() {
        itemTransferAmount--;

        UpdateItemAmountText(itemTransferAmount);
        float sliderValue = (float)itemTransferAmount / (float)maxItemAmount;
        UpdateItemAmountSlider(sliderValue);
    }

    private void UpdateItemAmountText(int itemAmount) {
        amountText.text = itemAmount.ToString();
    }

    private void UpdateItemAmountSlider(float sliderValue) {
        amountSlider.value = sliderValue;
    }

}
