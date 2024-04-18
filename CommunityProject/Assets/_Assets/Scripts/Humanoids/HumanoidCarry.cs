using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HumanoidCarry : MonoBehaviour
{
    private Humanoid humanoid;

    private Inventory humanoidCarryInventory;
    private int maxCarryAmount = 5;

    private Building destinationBuilding;
    private Building sourceBuilding;
    private Item itemToCarry;
    private Item itemCarrying = null;

    public event EventHandler OnCarryStarted;
    public event EventHandler OnCarryCompleted;

    private void Awake() {
        humanoidCarryInventory = new Inventory(true, 1, 1, maxCarryAmount);
        humanoid = GetComponent<Humanoid>();
    }

    public Building IdentifyBestDestinationBuilding() {
        List<Building> destinationBuildingsList = new List<Building>();
        if (itemCarrying == null) {
            destinationBuildingsList = BuildingsManager.Instance.GetDestinationBuildingsList(maxCarryAmount);
        } else {
            destinationBuildingsList = BuildingsManager.Instance.GetDestinationBuildingsList(maxCarryAmount, itemCarrying);
        }

        float bestBuildingScore = 0f;
        Building bestBuilding = null;

        foreach (Building building in destinationBuildingsList) {
            float score = CalculateDestinationInventoryScore(building);
            //building.SetBuildingVisualDebugScore(score.ToString());

            if (score > bestBuildingScore) {
                if(destinationBuilding is ProductionBuilding) {

                    //Check if there are source buildings for that destination building
                    Inventory destinationBuildingInventory = FindHighestPriorityInventoryInBuilding(destinationBuilding);
                    Item itemAskedForDestinationBuilding = destinationBuildingInventory.GetRestrictedItemList()[0];

                    if (IdentifyBestSourceBuilding(itemAskedForDestinationBuilding) != null) {
                        bestBuildingScore = score;
                        bestBuilding = building;
                    }
                } else {
                    bestBuildingScore = score;
                    bestBuilding = building;
                }
            }
        }

        destinationBuilding = bestBuilding;

        if(destinationBuilding != null) {
            Inventory destinationBuildingInventory = FindHighestPriorityInventoryInBuilding(destinationBuilding);
            itemToCarry = destinationBuildingInventory.GetRestrictedItemList()[0];
            humanoid.AssignBuilding(destinationBuilding);

        }

        return destinationBuilding;
    }

    public Building IdentifyBestSourceBuilding(Item itemToCarry) {

        List<Building> sourceBuildingsList = BuildingsManager.Instance.GetSourceBuildingsList(maxCarryAmount, itemToCarry);

        float bestBuildingScore = 0f;
        Building bestBuilding = null;

        foreach (Building building in sourceBuildingsList) {
            float score = CalculateSourceInventoryScore(building);
            //building.SetBuildingVisualDebugScore(score.ToString());

            if (score > bestBuildingScore) {
                bestBuildingScore = score;
                bestBuilding = building;
            }
        }

        sourceBuilding = bestBuilding;
        return sourceBuilding;
    }
    private float CalculateDestinationInventoryScore(Building building) {

        float distanceToBuilding = Vector3.Distance(transform.position, building.transform.position);
        float inventoryPriority = 0;

        if (building is ProductionBuilding) {
            ProductionBuilding productionBuilding = (ProductionBuilding)building;

            Inventory highestPriorityBuildingInventory = FindHighestPriorityInventoryInBuilding(productionBuilding);
            inventoryPriority = CalculateDestinationInventoryPriorityScore(highestPriorityBuildingInventory);
        }

        return inventoryPriority * 3 / distanceToBuilding;
    }
    private float CalculateSourceInventoryScore(Building building) {
        float distanceToBuilding = Vector3.Distance(transform.position, building.transform.position);

        return 1 / distanceToBuilding;
    }
    public Inventory FindHighestPriorityInventoryInBuilding(Building building) {
        float highestInventoryPriority = 0f;
        Inventory highestPriorityInventory = null;

       if(building is ProductionBuilding) {
            ProductionBuilding producerBuilding = (ProductionBuilding)building;
            foreach (Inventory inventory in producerBuilding.GetInputInventoryList()) {
                Item inputInventoryItem = inventory.GetRestrictedItemList()[0];

                // inventory priority [0,1] - 0 when full, 1 when empty
                float inventoryPriority = (float)inventory.AmountInventoryCanReceiveOfType(inputInventoryItem) / (float)ItemAssets.Instance.GetItemSO(inputInventoryItem.itemType).maxStackableAmount;
                if (inventoryPriority >= highestInventoryPriority) {
                    highestInventoryPriority = inventoryPriority;
                    highestPriorityInventory = inventory;
                }
            }
        }

        return highestPriorityInventory;
    }

    public float CalculateDestinationInventoryPriorityScore(Inventory inventory) {
        Item inputInventoryItem = inventory.GetRestrictedItemList()[0];
        float inventoryPriority = (float)inventory.AmountInventoryCanReceiveOfType(inputInventoryItem) / (float)ItemAssets.Instance.GetItemSO(inputInventoryItem.itemType).maxStackableAmount;

        return inventoryPriority;
    }

    public bool FetchItemsInSourceBuilding() {
        Item itemToFetch = new Item { itemType = itemToCarry.itemType, amount = maxCarryAmount };

        if(sourceBuilding is Chest) {
            Chest chest = sourceBuilding as Chest;
            if(chest.GetChestInventory().HasItem(itemToFetch)) {
                chest.GetChestInventory().RemoveItemAmount(itemToFetch);
                itemCarrying = itemToFetch;
                OnCarryStarted?.Invoke(this, EventArgs.Empty);
                return true;
            } else {
                return false;
            }
        }

        if(sourceBuilding is ProductionBuilding) {
            ProductionBuilding productionBuilding = sourceBuilding as ProductionBuilding;

            foreach (Inventory outputInventory in productionBuilding.GetOutputInventoryList()) {

                if (outputInventory.InventoryCanAcceptItem(itemToFetch) && outputInventory.HasItem(itemToFetch)) {

                    outputInventory.RemoveItemAmount(itemToFetch);
                    itemCarrying = itemToFetch;
                    OnCarryStarted?.Invoke(this, EventArgs.Empty);
                    return true;

                } else {
                    return false;
                }
            };
        }
        return false;
    }

    public bool DropItemsInDestinationBuilding() {
        if (destinationBuilding is Chest) {
            Chest chest = destinationBuilding as Chest;
            if (chest.GetChestInventory().InventoryCanAcceptItem(itemCarrying) && chest.GetChestInventory().AmountInventoryCanReceiveOfType(itemCarrying) >= itemCarrying.amount) {
                chest.GetChestInventory().AddItem(itemCarrying);
                itemCarrying = null;
                OnCarryCompleted?.Invoke(this, EventArgs.Empty);
                return true;
            } else {
                return false;
            }
        }

        if (destinationBuilding is ProductionBuilding) {
            ProductionBuilding productionBuilding = destinationBuilding as ProductionBuilding;

            foreach (Inventory intputInventory in productionBuilding.GetInputInventoryList()) {

                if (intputInventory.InventoryCanAcceptItem(itemCarrying) && intputInventory.AmountInventoryCanReceiveOfType(itemCarrying) >= itemCarrying.amount) {
                    intputInventory.AddItem(itemCarrying);
                    itemCarrying = null;
                    OnCarryCompleted?.Invoke(this, EventArgs.Empty);
                    return true;
                } else {
                    return false;
                }
            };
        }

        return false;
    }
    public Inventory GetHumanoidCarryInventory() {
        return humanoidCarryInventory;
    }

    public int GetMaxCarryAmount() {
        return maxCarryAmount;
    }

    public Building GetDestinationBuilding() {
        return destinationBuilding;
    }

    public Building GetSourceBuilding() {
        return sourceBuilding;
    }

    public bool IsCarryingItem() {
        return itemCarrying != null;
    }

    public Item GetItemCarrying() {
        return itemCarrying;
    }

    public Item GetItemToCarry() { 
        return itemToCarry; 
    }

    #region DEBUG
    [Button]
    public Building IdentifyBestDestinationBuildingDebug() {

        List<Building> destinationBuildingsList = BuildingsManager.Instance.GetDestinationBuildingsList(maxCarryAmount);

        float bestBuildingScore = 0f;
        Building bestBuilding = null;

        foreach (Building building in destinationBuildingsList) {
            float score = CalculateDestinationInventoryScore(building);
            building.SetBuildingVisualDebugScore(score.ToString());

            if (score > bestBuildingScore) {
                bestBuildingScore = score;
                bestBuilding = building;
            }
        }

        destinationBuilding = bestBuilding;

        if (destinationBuilding != null) {

            Inventory destinationBuildingInventory = FindHighestPriorityInventoryInBuilding(destinationBuilding as ProductionBuilding);
            itemToCarry = destinationBuildingInventory.GetRestrictedItemList()[0];
            humanoid.AssignBuilding(destinationBuilding);
        }

        return destinationBuilding;
    }

    [Button]
    public Building IdentifyBestSourceBuildingDebug() {

        List<Building> sourceBuildingsList = BuildingsManager.Instance.GetSourceBuildingsList(maxCarryAmount, itemToCarry);

        float bestBuildingScore = 0f;
        Building bestBuilding = null;

        foreach (Building building in sourceBuildingsList) {
            float score = CalculateSourceInventoryScore(building);
            building.SetBuildingVisualDebugScore(score.ToString());

            if (score > bestBuildingScore) {
                bestBuildingScore = score;
                bestBuilding = building;
            }
        }

        sourceBuilding = bestBuilding;
        return sourceBuilding;
    }
    #endregion
}
