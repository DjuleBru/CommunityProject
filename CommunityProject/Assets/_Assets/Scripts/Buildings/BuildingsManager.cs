using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{

    public static BuildingsManager Instance { get; private set; }

    [SerializeField] private GameObject overworldGridVisual;

    private List<Building> buildingsSpawned;
    private List<ProductionBuilding> productionBuildingsSpawned;

    [SerializeField] private List<int> buildingsSavedIDList;

    public event EventHandler OnAnyBuildingSpawned;
    public event EventHandler OnAnyBuildingPlacedOrCancelled;

    private void Awake() {
        Instance = this;
        buildingsSpawned = new List<Building>();
        productionBuildingsSpawned = new List<ProductionBuilding>();
        overworldGridVisual.SetActive(false);

        LoadBuildingsInOverworld();
    }

    public void SaveBuildingsInOverworld() {
        buildingsSavedIDList = new List<int>();

        foreach (Building building in buildingsSpawned) {
            buildingsSavedIDList.Add(building.GetInstanceID());
            ES3.Save(building.GetInstanceID().ToString(), building.gameObject);
            Debug.Log("saving building " + building);
        }

        ES3.Save("productionBuildingsSavedIDList", buildingsSavedIDList);
    }

    public void LoadBuildingsInOverworld() {
        buildingsSavedIDList = ES3.Load("productionBuildingsSavedIDList", new List<int>());

        foreach (int id in buildingsSavedIDList) {
            ES3.Load(id.ToString());
            Debug.Log("loading building " +  id);
        }
        overworldGridVisual.SetActive(false);
    }

    public void SetBuildingSpawned() {
        overworldGridVisual.SetActive(true);
        OnAnyBuildingSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void SetBuildingPlacedOrCancelled() {
        overworldGridVisual.SetActive(false);
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

    public List<Building> GetDestinationBuildingsList(int carryCapacity, Item item) {
        List<Building> destinationBuildingList = new List<Building>();

        foreach (Building building in buildingsSpawned) {

            if (building is ProductionBuilding) {
                ProductionBuilding productionBuilding = building as ProductionBuilding;

                foreach (Inventory inputInventory in productionBuilding.GetInputInventoryList()) {

                    if (inputInventory.InventoryCanAcceptItem(item) && inputInventory.AmountInventoryCanReceiveOfType(item) > carryCapacity) {
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

    public List<Building> GetFoodDistributionBuildingsList() {
        List<Building> foodDistributionBuildingsList = new List<Building>();

        foreach (Building building in buildingsSpawned) {

            if (building is Chest) {
                Chest chest = (Chest)building;

                if (chest.GetItemCategoryToStore() == Item.ItemCategory.Food) {

                    List<Item> foodItems = ItemAssets.Instance.GetItemListOfCategory(Item.ItemCategory.Food);

                    foreach (Item item in foodItems) {
                        if (chest.GetChestInventory().AmountInventoryHasOfType(item) > 0) {
                            foodDistributionBuildingsList.Add(chest);
                        }
                    }

                }
            }
        }
        return foodDistributionBuildingsList;
    }

    public List<Building> GetHousingBuildingsList() {
        List<Building> housingBuildingsList = new List<Building>();

        foreach (Building building in buildingsSpawned) {
            if(building.GetBuildingSO().housingCapacity != 0) {
                housingBuildingsList.Add(building);
            }
        }
        return housingBuildingsList;
    }

    public List<Item> GetAllEquipmentItemsOfCategory(Item.ItemEquipmentCategory category) {

        List<Item> allEquipmentItems = new List<Item>();

        foreach (Building building in buildingsSpawned) {
            if (!(building is Chest)) continue;

            Chest chest = building as Chest;
            if (chest.GetItemCategoryToStore() != Item.ItemCategory.Equipment) continue;

            // We have an equipment holding chest
            foreach(Item item in chest.GetChestInventory().GetItemList()) {
                if(ItemAssets.Instance.GetItemSO(item.itemType).itemEquipmentCategory == category) {
                    allEquipmentItems.Add(item);
                }
            }
        }

        return allEquipmentItems;
    }

    public List<Chest> GetBuildingStoringEquipment(Item item) {

        List<Chest> chestsStoringEquipment = new List<Chest>();

        foreach (Building building in buildingsSpawned) {
            if (!(building is Chest)) continue;

            Chest chest = building as Chest;
            if (chest.GetItemCategoryToStore() != Item.ItemCategory.Equipment) continue;

            // We have an equipment holding chest
            foreach (Item storedItem in chest.GetChestInventory().GetItemList()) {
                if(storedItem.itemType == item.itemType) {
                    chestsStoringEquipment.Add(chest);
                }
            }
        }

        return chestsStoringEquipment;
    }

    public void SetAllVisualCollidersActive(bool active) {
        foreach(Building building in buildingsSpawned) {
            building.GetBuildingVisual().SetColliderActive(active);
        }
    }
}
