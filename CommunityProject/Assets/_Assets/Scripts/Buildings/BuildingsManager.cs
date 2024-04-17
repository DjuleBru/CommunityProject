using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{

    public static BuildingsManager Instance { get; private set; }

    private List<Building> buildingsSpawned;
    private List<ProductionBuilding> productionBuildingsSpawned;

    public event EventHandler OnAnyBuildingSpawned;
    public event EventHandler OnAnyBuildingPlacedOrCancelled;

    private void Awake() {
        Instance = this;
        buildingsSpawned = new List<Building>();
        productionBuildingsSpawned = new List<ProductionBuilding>();
    }

    public void SetBuildingSpawned() {
        OnAnyBuildingSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void SetBuildingPlacedOrCancelled() {
        OnAnyBuildingPlacedOrCancelled?.Invoke(this, EventArgs.Empty);
    }

    public void AddBuilding(Building building) {
        buildingsSpawned.Add(building);

        if(building is ProductionBuilding) {
            productionBuildingsSpawned.Add(building as ProductionBuilding);
        }
    }

    public void RemoveBuilding(Building building) {
        buildingsSpawned.Remove(building);

        if (building is ProductionBuilding) {
            productionBuildingsSpawned.Remove(building as ProductionBuilding);
        }
    }

    public List<ProductionBuilding> GetProductionBuildings() {
        return productionBuildingsSpawned;
    }

    public List<Building> GetDestinationBuildingsList(int carryCapacity) {
        List<Building> destinationBuildingList = new List<Building>();  

        foreach (Building building in buildingsSpawned) {
            if(building is ProductionBuilding) {
                ProductionBuilding productionBuilding = building as ProductionBuilding;

                foreach(Inventory inputInventory in productionBuilding.GetInputInventoryList()) {
                    Item inputInventoryItem = inputInventory.GetRestrictedItemList()[0];
                    
                    if (inputInventory.AmountInventoryCanReceiveOfType(inputInventoryItem) > carryCapacity) {
                        destinationBuildingList.Add(productionBuilding);
                    }
                };
            }
        }
        return destinationBuildingList;
    }

    public List<Building> GetSourceBuildingsList(int carryCapacity, Item sourceItem) {
        List<Building> sourceBuildingList = new List<Building>();

        foreach (Building building in buildingsSpawned) {

            if (building is ProductionBuilding) {
                ProductionBuilding productionBuilding = building as ProductionBuilding;

                foreach (Inventory outputInventory in productionBuilding.GetOutputInventoryList()) {

                    if (outputInventory.AmountInventoryHasOfType(sourceItem) > carryCapacity) {
                        sourceBuildingList.Add(productionBuilding);
                    }

                };
            }

            if (building is Chest) {
                Chest chest = (Chest)building;

                if (chest.GetChestInventory().AmountInventoryHasOfType(sourceItem) > carryCapacity) {
                    sourceBuildingList.Add(chest);
                }
            }
        }
        return sourceBuildingList;
    }
}
