using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        this.hasLimitedSlots = hasLimitedStorage;
        this.slotNumberX = slotNumberX;
        this.slotNumberY = slotNumberY;
        totalSlotNumber = slotNumberX * slotNumberY;
    }

    public void AddItem(Item item) {
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

        foreach (Item inventoryItem in itemList) {

            if (inventoryItem.itemType == item.itemType) {
                // Item already in inventory
                itemAlreadyInInventory = true;

                int inventoryItemAmount = inventoryItem.amount;
                int newItemAmount = item.amount;

                if ((inventoryItemAmount + newItemAmount) <= ItemAssets.Instance.GetItemSO(item.itemType).maxStackableAmount) {
                    // Stack is not full and can carry more items
                    inventoryItem.amount += item.amount;
                    stackIsFull = false;
                    break;
                }
                else {
                    // Stack cannot carry all amounts : fill it & create new stack
                    stackIsFull = true;

                    if (inventoryItemAmount <= ItemAssets.Instance.GetItemSO(item.itemType).maxStackableAmount) {
                        // Stack is not full : fill stack
                        int spareStacks = ItemAssets.Instance.GetItemSO(item.itemType).maxStackableAmount - inventoryItemAmount;
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
                itemList.Remove(item);
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

            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList) {

                if (inventoryItem.itemType == item.itemType && inventoryItem.amount >= item.amount) {
                    // Item is stackable and already in inventory

                    int inventoryItemAmount = inventoryItem.amount;
                    int newItemAmount = item.amount;

                    if (inventoryItemAmount - newItemAmount >= 0) {
                        // There is enough items in the stack to remove them
                        inventoryItem.amount -= item.amount;
                        itemInInventory = inventoryItem;

                        break;
                    }
                }
            }

            if (itemInInventory != null && itemInInventory.amount <= 0) {
                // Item is stackable and there are no more counts
                itemList.Remove(item);
            }
        }
        else {
            // Item is NOT stackable 
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool HasSpaceForItem(Item item) {
        if (ItemAssets.Instance.GetItemSO(item.itemType).isStackable) {
            // Item is stackable 

            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList) {
                if (inventoryItem.itemType == item.itemType) {
                    // Item is stackable and already in inventory

                    int inventoryItemAmount = inventoryItem.amount;
                    int newItemAmount = item.amount;

                    if ((inventoryItemAmount + newItemAmount) <= ItemAssets.Instance.GetItemSO(item.itemType).maxStackableAmount) {
                        return true;
                    } else {
                        if(itemList.Count < totalSlotNumber) {
                            return true;
                        } else {
                            return false;
                        }
                    }
                }
            }

            if (!itemAlreadyInInventory) {
                // Item is stackable not already in inventory
                if (itemList.Count < totalSlotNumber) {
                    return true;
                }
                else {
                    // There is no space in inventory
                    return false;
                }
            }
        }
        else {
            // Item is NOT stackable 
            if (itemList.Count < totalSlotNumber) {
                return true;
            }
            else {
                // There is no space in inventory
                return false;
            }
        }

        return false;
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
