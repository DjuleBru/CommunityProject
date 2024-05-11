using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCarry : MonoBehaviour
{
    private Humanoid humanoid;

    [SerializeField] private Inventory humanoidCarryInventory;
    private int carryCapacity;

    [SerializeField] private Item itemCarrying = null;
    [SerializeField] private Item itemToCarry = null;

    [SerializeField] private List<Item> itemCarryingList = new List<Item>();

    public event EventHandler OnCarryStarted;
    public event EventHandler OnCarryCompleted;

    private void Awake() {
        humanoidCarryInventory = new Inventory(true, 1, 1, carryCapacity);
        humanoid = GetComponent<Humanoid>();
    }

    private void Start() {
        carryCapacity = humanoid.GetCarryCapacity();
    }

    public bool FetchItemsInBuilding(Building building) {
        Item itemToFetch = new Item { itemType = itemToCarry.itemType, amount = carryCapacity };

        if (building is Chest) {
            Chest chest = building as Chest;

            if (chest.GetChestInventory().HasItem(itemToFetch)) {
                chest.GetChestInventory().RemoveItemAmount(itemToFetch);
                itemCarrying = itemToFetch;
                OnCarryStarted?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else {
                return false;
            }
        }

        if (building is ProductionBuilding) {
            ProductionBuilding productionBuilding = building as ProductionBuilding;

            foreach (Inventory outputInventory in productionBuilding.GetOutputInventoryList()) {

                if (outputInventory.InventoryCanAcceptItem(itemToFetch) && outputInventory.HasItem(itemToFetch)) {

                    outputInventory.RemoveItemAmount(itemToFetch);
                    itemCarrying = itemToFetch;
                    OnCarryStarted?.Invoke(this, EventArgs.Empty);
                    return true;

                }
                else {
                    return false;
                }
            };
        }
        return false;
    }

    public bool DropItemCarryingInBuilding(Building building) {
        if (building is Chest) {
            Chest chest = building as Chest;
            if (chest.GetChestInventory().InventoryCanAcceptItem(itemCarrying) && chest.GetChestInventory().AmountInventoryCanReceiveOfType(itemCarrying) >= itemCarrying.amount) {
                chest.GetChestInventory().AddItem(itemCarrying);
                itemCarrying = null;
                OnCarryCompleted?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else {
                return false;
            }
        }

        if (building is ProductionBuilding) {
            ProductionBuilding productionBuilding = building as ProductionBuilding;

            foreach (Inventory intputInventory in productionBuilding.GetInputInventoryList()) {

                if (intputInventory.InventoryCanAcceptItem(itemCarrying) && intputInventory.AmountInventoryCanReceiveOfType(itemCarrying) >= itemCarrying.amount) {
                    intputInventory.AddItem(itemCarrying);
                    itemCarrying = null;
                    OnCarryCompleted?.Invoke(this, EventArgs.Empty);
                    return true;
                }
                else {
                    return false;
                }
            };
        }

        return false;
    }

    public bool DropItemListCarryingInChest(Chest chest) {

        bool droppedAllItems = true;

        List<Item> itemToRemoveList = new List<Item>();

        foreach(Item item in itemCarryingList) {
            if (chest.GetChestInventory().InventoryCanAcceptItem(item) && chest.GetChestInventory().AmountInventoryCanReceiveOfType(item) >= item.amount) {
                chest.GetChestInventory().AddItem(item);
                itemToRemoveList.Add(item);
            }
            else {
                droppedAllItems = false;
            }
        }

        foreach(Item item in itemToRemoveList) {
            RemoveItemCarrying(item);
        }

        return droppedAllItems;
    }

    public Inventory GetHumanoidCarryInventory() {
        return humanoidCarryInventory;
    }

    public void SetItemCarrying(Item itemCarrying) {
        this.itemCarrying = itemCarrying;

        if(itemCarrying != null ) {
            OnCarryStarted?.Invoke(this, EventArgs.Empty);
        } else {
            OnCarryCompleted?.Invoke(this, EventArgs.Empty);
        }

    }

    public void SetItemToCarry(Item itemToCarry) {
        this.itemToCarry = itemToCarry;
    }

    public void AddItemCarrying(Item itemCarrying) {
        itemCarryingList.Add(itemCarrying);
        OnCarryStarted?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItemCarrying(Item itemCarrying) {
        itemCarryingList.Remove(itemCarrying);

        if(itemCarryingList.Count == 0) {
            OnCarryCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearItemsCarryingList() {
        itemCarryingList.Clear();
    }

    public Item GetItemCarrying() {
        return itemCarrying;
    }

    public List<Item> GetItemCarryingList() {
        return itemCarryingList;
    }

    public Item GetItemCarryingForVisual() {
        if (itemCarryingList.Count > 0) {
            return itemCarryingList[UnityEngine.Random.Range(0, itemCarryingList.Count)];
        }

        return itemCarrying;
    }

    public bool IsCarryingItem() {

        if(itemCarrying == null) {
            if (itemCarryingList.Count != 0) {
                return true;
            }
            else {
                return false;
            }
        }

        if(itemCarrying != null) {
            if (itemCarrying.amount != 0 || itemCarryingList.Count != 0) {
                return true;
            }
            else {
                return false;
            }
        }

        return false;
    }

    public void LoadHumanoidCarry() {
        if (itemCarrying == null) return;
        if (itemCarrying.amount != 0) {
            OnCarryStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}
