using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Sirenix.Utilities.Editor.MultilineWrapLayoutUtility;
using static UnityEditor.Progress;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private bool hasLimitedSlots;
    private int slotNumberX;
    private int slotNumberY;
    private int totalSlotNumber;

    public Inventory(bool hasLimitedStorage, int slotNumberX, int slotNumberY) {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Stone, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });

        this.hasLimitedSlots = hasLimitedStorage;
        this.slotNumberX = slotNumberX;
        this.slotNumberY = slotNumberY;
        totalSlotNumber = slotNumberX * slotNumberY;
    }

    public void AddItem(Item item) {
        if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
            // Item is stackable 
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList) {

                if (inventoryItem.itemType == item.itemType) {
                    // Item is stackable and already in inventory
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory) {
                // Item is stackable not already in inventory
                if (!hasLimitedSlots | itemList.Count < totalSlotNumber) {
                    itemList.Add(item);
                }
            }

        }
        else {
            // Item is NOT stackable 
            if (!hasLimitedSlots | itemList.Count < totalSlotNumber) {
                itemList.Add(item);
            }
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public List<Item> GetItemList() {
        return itemList;
    }

    public bool HasLimitedSlots() {
        return hasLimitedSlots;
    }

    public int GetSlotNumberX() {
        return slotNumberX;
    }

    public int GetSlotNumberY() {
        return slotNumberY;
    }

}
