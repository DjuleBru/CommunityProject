using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemList;
    private List<Item> restrictedItemList;
    private bool hasLimitedSlots;
    private bool restrictedInventory;
    private int slotNumberX;
    private int slotNumberY;
    private int totalSlotNumber;
    private int inventoryMaxStackAmount;

    public Inventory(bool hasLimitedSlots, int slotNumberX, int slotNumberY, bool restrictedInventory, List<Item> restrictedItemList) {
        itemList = new List<Item>();
        this.restrictedItemList = restrictedItemList;
        this.hasLimitedSlots = hasLimitedSlots;
        this.restrictedInventory = restrictedInventory;
        this.slotNumberX = slotNumberX;
        this.slotNumberY = slotNumberY;
        totalSlotNumber = slotNumberX * slotNumberY;
    }

    public Inventory(bool hasLimitedSlots, int slotNumberX, int slotNumberY, int maxStackAmount) {
        itemList = new List<Item>();
        this.inventoryMaxStackAmount = maxStackAmount;
        this.hasLimitedSlots = hasLimitedSlots;
        this.slotNumberX = slotNumberX;
        this.slotNumberY = slotNumberY;
        totalSlotNumber = slotNumberX * slotNumberY;
    }

    public bool InventoryCanAcceptItem(Item item) {
        if (!restrictedInventory) return true;

        foreach(Item acceptedItem in restrictedItemList) {
            if(item.itemType == acceptedItem.itemType) {
                return true;
            }
        }
        return false;
    }

    public void AddItem(Item item) {
        if (item == null) return;
        if (!InventoryCanAcceptItem(item)) return;
        
        if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
            // Item is stackable 
            AddStackableItemToInventory(item);
        }
        else {
            // Item is NOT stackable 
            AddUnStackableItemToInventory(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    private void AddStackableItemToInventory(Item item) {
        bool itemAlreadyInInventory = false;
        bool stackIsFull = false;
        int maxStackableAmount = 0;

        if(inventoryMaxStackAmount == 0) {
            maxStackableAmount = ItemAssets.Instance.GetItemSO(item.itemType).maxStackableAmount;
        }

        foreach (Item inventoryItem in itemList) {

            if (inventoryItem.itemType == item.itemType) {
                // Item already in inventory
                itemAlreadyInInventory = true;

                int inventoryItemAmount = inventoryItem.amount;
                int newItemAmount = item.amount;

                if ((inventoryItemAmount + newItemAmount) <= maxStackableAmount) {
                    // Stack is not full and can carry more items
                    inventoryItem.amount += item.amount;
                    stackIsFull = false;
                    break;
                }

                else {
                    // Stack cannot carry all amounts : fill it & create new stack
                    stackIsFull = true;

                    if (inventoryItemAmount <= maxStackableAmount) {
                        // Stack is not full : fill stack
                        int spareStacks = maxStackableAmount - inventoryItemAmount;
                        inventoryItem.amount += spareStacks;
                        item.amount -= spareStacks;
                    }
                }
            }
        }

        if (stackIsFull) {
            if (!hasLimitedSlots | itemList.Count < totalSlotNumber) {
                itemList.Add(item);
            }
            else {
                Debug.Log("There is no space in inventory");
            }
        }

        if (!itemAlreadyInInventory) {
            // Item is stackable not already in inventory
            if (!hasLimitedSlots | itemList.Count < totalSlotNumber) {
                itemList.Add(item);
            }
            else {
                Debug.Log("There is no space in inventory");
            }
        }
    }

    private void AddUnStackableItemToInventory(Item item) {
        if (!hasLimitedSlots | itemList.Count < totalSlotNumber) {
            itemList.Add(item);
        }
        else {
            Debug.Log("There is no space in inventory");
        }
    }

    public void RemoveItemStack(Item item) {
        if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
            // Item is stackable 

            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType && inventoryItem.amount == item.amount) {
                    // Item stack identified

                    int inventoryItemAmount = inventoryItem.amount;
                    int newItemAmount = item.amount;

                    if(inventoryItemAmount - newItemAmount >= 0) {
                        // There is enough items in the stack to remove them
                        inventoryItem.amount -= item.amount;
                        itemInInventory = inventoryItem;
                        break;
                    }
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0) {
                // Item is stackable and there are no more counts
                itemList.Remove(itemInInventory);
            }
        }
        else {
            // Item is NOT stackable 
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItemAmount(Item item) {
        if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
            // Item is stackable 

            List<Item> itemsToRemove = new List<Item>();
            int itemAmountLeftToRemove = item.amount;

            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    // Item is stackable and already in inventory

                    int inventoryItemAmount = inventoryItem.amount;

                    if (inventoryItemAmount  > itemAmountLeftToRemove) {
                        // There is enough items in the stack to remove them
                        inventoryItem.amount -= itemAmountLeftToRemove;
                        break;
                    } else {
                        // There is not enough items in the stack to remove them
                        itemAmountLeftToRemove -= inventoryItemAmount;
                        itemsToRemove.Add(inventoryItem);
                    }
                }
            }

            // Remove empty item stacks
            foreach(Item itemToRemove in itemsToRemove) {
                itemList.Remove(itemToRemove);
            }
        }

        else {
            // Item is NOT stackable 
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public int AmountInventoryCanReceiveOfType(Item item) {
        if (!InventoryCanAcceptItem(item)) return 0;

        int maxStackableAmount = 0;

        if (inventoryMaxStackAmount == 0) {
            maxStackableAmount = ItemAssets.Instance.GetItemSO(item.itemType).maxStackableAmount;
        }

        if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
            // Item is stackable 
            int itemAmountInventoryCanCarry = 0;

            if (itemList.Count < totalSlotNumber) {
                // There are item slots available
                return maxStackableAmount;
            }

            //There are no empty item slots available

            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    // Item is stackable and already in inventory
                    int inventoryItemAmount = inventoryItem.amount;

                    if ((inventoryItemAmount) < maxStackableAmount) {
                        // Meeting an unfilled stack
                        itemAmountInventoryCanCarry += (maxStackableAmount - inventoryItemAmount);
                    }
                }
            }

            return itemAmountInventoryCanCarry;
        } else {
            if (itemList.Count < totalSlotNumber) {
                // There are item slots available
                return totalSlotNumber - itemList.Count;
            }
        }

        return 0;
    }

    public int AmountInventoryHasOfType(Item item) {
        int amountInInventory = 0;

        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == item.itemType) {
                // Item is in inventory
                amountInInventory += inventoryItem.amount;
            }
        }

        return amountInInventory;
    }

    public bool HasItem(Item askedItem) {
        int itemStacks =0;

        // Loop through each item and sum the stacks of the correct item type
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == askedItem.itemType) {
                // Item type is in inventory
                itemStacks += inventoryItem.amount;
            }
        }

        if(itemStacks >= askedItem.amount) {
            return true;
        } else {
            return false;
        }
    }

    public List<Item> GetItemList() {
        return itemList;
    }

    public List<Item> GetRestrictedItemList() {
        return restrictedItemList;
    }

    public void AddItemList(List<Item> itemList) {
        foreach (Item item in itemList) {
            AddItem(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
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

    public void ShowInventoryDebug() {
        foreach (Item inventoryItem in itemList) {
            Debug.Log(inventoryItem.itemType + " " + inventoryItem.amount);
        }
    }
}
