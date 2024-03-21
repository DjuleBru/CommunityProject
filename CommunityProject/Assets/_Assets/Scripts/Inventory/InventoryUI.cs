using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour, IDropHandler {
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    [SerializeField] private Transform emptyItemSlotTemplate;

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryUI();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryUI();
    }

    private void RefreshInventoryUI() {
        foreach(Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            if(child == emptyItemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        if (!inventory.HasLimitedSlots()) {
            RefreshUnlimitedInventoryUI();
        } else {
            Debug.Log("refreshLimited");
            RefreshLimitedInventoryUI();
        }
    }

    private void RefreshUnlimitedInventoryUI() {
        
        // Inventory is unlimited
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            SetSlotTemplateVisuals(item, itemSlotRectTransform);
        }
    }

    private void RefreshLimitedInventoryUI() {
        int itemNumber = 0;

        for(int x = 0;  x < inventory.GetSlotNumberX(); x++) {
            for(int y = 0;  y < inventory.GetSlotNumberY(); y++) {

                RectTransform emptyItemSlotRectTransform = Instantiate(emptyItemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                emptyItemSlotRectTransform.gameObject.SetActive(true);

                if (itemNumber < inventory.GetItemList().Count) {
                    // The item slot is filled with an item
                    Item item = inventory.GetItemList()[itemNumber];
                    SetSlotTemplateVisuals(item, emptyItemSlotRectTransform);
                } else {
                    emptyItemSlotRectTransform.Find("Item").Find("ItemImage").GetComponent<Image>().enabled = false;
                }
                itemNumber++;
            }
        }
    }

    private void SetSlotTemplateVisuals(Item item, RectTransform slotTemplate) {
        Image image = slotTemplate.Find("Item").Find("ItemImage").GetComponent<Image>();
        image.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;

        TextMeshProUGUI text = slotTemplate.Find("Item").Find("ItemAmount").GetComponent<TextMeshProUGUI>();

        if (item.amount > 1) {
            text.text = item.amount.ToString();
        }
        else {
            text.text = "";
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            // Handle drag & drop between inventories
        }
    }
}
