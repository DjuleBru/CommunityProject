using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HumanoidHaul : MonoBehaviour
{
    private Humanoid humanoid;
    [SerializeField] private HumanoidCarry humanoidCarry;
    [SerializeField] private Building destinationBuilding;
    [SerializeField] private Building sourceBuilding;
    [SerializeField] private Item itemToCarry = null;

    private void Awake() {
        LoadHumanoid();
    }

    public Building TryAssignBestDestinationBuilding() {
        //DeAssign from previous destination building
        if(destinationBuilding != null) {
            destinationBuilding.DeAssignInputHaulier(humanoid);
        }

        List<Building> destinationBuildingsList = new List<Building>();

        if (humanoidCarry.GetItemCarrying() == null || humanoidCarry.GetItemCarrying().amount == 0) {
            destinationBuildingsList = BuildingsManager.Instance.GetDestinationBuildingsList(humanoid.GetCarryCapacity());
        } else {
            destinationBuildingsList = BuildingsManager.Instance.GetDestinationBuildingsList(humanoid.GetCarryCapacity(), humanoidCarry.GetItemCarrying());
        }

        float bestBuildingScore = 0f;
        Building bestBuilding = null;

        foreach (Building building in destinationBuildingsList) {
            float inventoryScore = CalculateDestinationInventoryScore(building);
            float assignedHauliersScore = 1;
            if (building.GetAssignedInputHauliersList() != null) {
                assignedHauliersScore = building.GetAssignedInputHauliersList().Count + 1;
            }
            assignedHauliersScore += 1f;

            float score = inventoryScore / assignedHauliersScore;
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
            destinationBuilding.AssignInputHaulier(humanoid);
        }

        return destinationBuilding;
    }

    public Building IdentifyBestSourceBuilding(Item itemToCarry) {

        List<Building> sourceBuildingsList = BuildingsManager.Instance.GetSourceBuildingsList(humanoid.GetCarryCapacity(), itemToCarry);

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
        if(sourceBuilding != null) {
            sourceBuilding.AssignOutputHaulier(humanoid);
        }
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

        return inventoryPriority * 5 / distanceToBuilding;
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

    public bool FetchItemsInBuilding(Building building) {
        Item itemToFetch = new Item { itemType = itemToCarry.itemType, amount = humanoid.GetCarryCapacity()};

        if(building is Chest) {
            Chest chest = building as Chest;

            if (chest.GetChestInventory().HasItem(itemToFetch)) {
                chest.GetChestInventory().RemoveItemAmount(itemToFetch);
                humanoidCarry.SetItemCarrying(itemToFetch);
                return true;
            } else {
                return false;
            }
        }

        if(building is ProductionBuilding) {
            ProductionBuilding productionBuilding = building as ProductionBuilding;

            foreach (Inventory outputInventory in productionBuilding.GetOutputInventoryList()) {

                if (outputInventory.InventoryCanAcceptItem(itemToFetch) && outputInventory.HasItem(itemToFetch)) {

                    outputInventory.RemoveItemAmount(itemToFetch);
                    humanoidCarry.SetItemCarrying(itemToFetch);
                    return true;

                } else {
                    return false;
                }
            };
        }
        return false;
    }

    public bool DropItemsInBuilding(Building building) {
        if (building is Chest) {
            Chest chest = building as Chest;
            if (chest.GetChestInventory().InventoryCanAcceptItem(humanoidCarry.GetItemCarrying()) && chest.GetChestInventory().AmountInventoryCanReceiveOfType(humanoidCarry.GetItemCarrying()) >= humanoidCarry.GetItemCarrying().amount) {
                chest.GetChestInventory().AddItem(humanoidCarry.GetItemCarrying());
                humanoidCarry.SetItemCarrying(null);
                return true;
            } else {
                return false;
            }
        }

        if (building is ProductionBuilding) {
            ProductionBuilding productionBuilding = building as ProductionBuilding;

            foreach (Inventory intputInventory in productionBuilding.GetInputInventoryList()) {

                if (intputInventory.InventoryCanAcceptItem(humanoidCarry.GetItemCarrying()) && intputInventory.AmountInventoryCanReceiveOfType(humanoidCarry.GetItemCarrying()) >= humanoidCarry.GetItemCarrying().amount) {
                    intputInventory.AddItem(humanoidCarry.GetItemCarrying());
                    humanoidCarry.SetItemCarrying(null);
                    return true;
                } else {
                    return false;
                }
            };
        }

        return false;
    }

    public void ReplaceDestinationBuildingAssigned(Building newDestinationBuilding) {
        if(destinationBuilding != null) {
            destinationBuilding.DeAssignInputHaulier(humanoid);
        }

        humanoid.AssignBuilding(newDestinationBuilding);
        destinationBuilding = newDestinationBuilding;
        newDestinationBuilding.AssignInputHaulier(humanoid);
    }

    public void ReplaceSourceBuildingAssigned(Building newSourceBuilding) {
        if (sourceBuilding != null) {
            sourceBuilding.DeAssignOutputHaulier(humanoid);
        }

        sourceBuilding = newSourceBuilding;
        newSourceBuilding.AssignOutputHaulier(humanoid);
    }

    public Building GetDestinationBuilding() {
        return destinationBuilding;
    }

    public Building GetSourceBuilding() {
        return sourceBuilding;
    }

    public bool IsCarryingItem() {
        return humanoidCarry.GetItemCarrying() != null;
    }

    public Item GetItemCarrying() {
        return humanoidCarry.GetItemCarrying();
    }

    public Item GetItemToCarry() { 
        return itemToCarry; 
    }

    public void SetItemToCarry(Item item) {
        itemToCarry = item;
    }

    public void StopCarrying() {
        Debug.Log("stop carrying");
        if(destinationBuilding != null) {
            destinationBuilding.DeAssignInputHaulier(humanoid);
        }

        if(sourceBuilding != null) {
            sourceBuilding.DeAssignOutputHaulier(humanoid);
        }

        destinationBuilding = null;
        sourceBuilding = null;
        humanoidCarry.SetItemCarrying(null);
        itemToCarry = null;
    }

    public void LoadHumanoid() {
        humanoidCarry = GetComponent<HumanoidCarry>();
        humanoid = GetComponent<Humanoid>();
    }

    public void LoadHumanoidHaul() {
        string humanoidID = humanoid.GetInstanceID().ToString();
        destinationBuilding = ES3.Load(humanoidID + "destinationBuilding", destinationBuilding);
        sourceBuilding = ES3.Load(humanoidID + "sourceBuilding", sourceBuilding);
        itemToCarry = ES3.Load(humanoidID + "itemToCarry", itemToCarry);

        Debug.Log("destination building " + destinationBuilding);
    }

    public void SaveHumanoidHaul() {
        string humanoidID = humanoid.GetInstanceID().ToString();
        ES3.Save(humanoidID + "destinationBuilding", destinationBuilding);
        ES3.Save(humanoidID + "sourceBuilding", sourceBuilding);
        ES3.Save(humanoidID + "itemToCarry", itemToCarry);
    }

    #region DEBUG
    [Button]
    public Building IdentifyBestDestinationBuildingDebug() {

        List<Building> destinationBuildingsList = BuildingsManager.Instance.GetDestinationBuildingsList(humanoid.GetCarryCapacity());

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

        List<Building> sourceBuildingsList = BuildingsManager.Instance.GetSourceBuildingsList(humanoid.GetCarryCapacity(), itemToCarry);

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
