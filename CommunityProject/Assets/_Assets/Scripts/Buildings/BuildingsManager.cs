using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Sirenix;

public class BuildingsManager : MonoBehaviour
{

    public static BuildingsManager Instance { get; private set; }

    [SerializeField] private GameObject overworldGridVisual;

    private List<Building> buildingsSpawned;
    private List<ProductionBuilding> productionBuildingsSpawned;
    private List<BuildingSO> unlockedBuildingSOList;
    private List<BuildingSO> lockedBuildingSOList;

    [SerializeField] private List<int> buildingsSavedIDList;

    public event EventHandler OnAnyBuildingSpawned;
    public event EventHandler OnAnyBuildingPlacedOrCancelled;

    [SerializeField] private Sprite metalWorkCategorySprite;
    [SerializeField] private Sprite woodWorkCategorySprite;
    [SerializeField] private Sprite alchemyCategorySprite;
    [SerializeField] private Sprite craftsManShipCategorySprite;
    [SerializeField] private Sprite housingCategorySprite;
    [SerializeField] private Sprite foodProductionCategorySprite;
    [SerializeField] private Sprite researchCategorySprite;
    [SerializeField] private Sprite mineralProcessingSprite;
    [SerializeField] private Sprite fabricProcessingSprite;
    [SerializeField] private Sprite foodPreparationSprite;
    [SerializeField] private Sprite storageSprite;
    [SerializeField] private Sprite industrySprite;

    private void Awake() {
        Instance = this;
        buildingsSpawned = new List<Building>();
        productionBuildingsSpawned = new List<ProductionBuilding>();
        overworldGridVisual.SetActive(false);

        LoadBuildingsInOverworld();
        InitializeUnlockedBuildingsList();
    }

    private void InitializeUnlockedBuildingsList() {
        if (unlockedBuildingSOList == null) {
            unlockedBuildingSOList = new List<BuildingSO>();
            lockedBuildingSOList = new List<BuildingSO>();

            foreach (BuildingSO buildingSO in BuildingAssets.Instance.GetBuildingSOList()) {
                if (buildingSO.itemsToUnlockList.Count == 0) {
                    // building is unlocked at the very beginning
                    unlockedBuildingSOList.Add(buildingSO);
                } else {
                    lockedBuildingSOList.Add(buildingSO);
                }
            };
        }
    }

    public void UnlockBuilding(BuildingSO buildingSO) {
        unlockedBuildingSOList.Add(buildingSO);
        lockedBuildingSOList.Remove(buildingSO);
    }


    public void SaveBuildingsInOverworld() {
        buildingsSavedIDList = new List<int>();

        foreach (Building building in buildingsSpawned) {
            buildingsSavedIDList.Add(building.GetInstanceID());
            ES3.Save(building.GetInstanceID().ToString(), building.gameObject);
            Debug.Log("saving building " + building);
        }

        ES3.Save("buildingsSavedIDList", buildingsSavedIDList);
        Debug.Log("savec building saved ids " + buildingsSavedIDList.Count);
    }

    public void LoadBuildingsInOverworld() {
        buildingsSavedIDList = ES3.Load("buildingsSavedIDList", new List<int>());
        Debug.Log("loaded building saved ids " + buildingsSavedIDList.Count);

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

        SaveBuildingsInOverworld();
    }

    public void RemoveBuilding(Building building) {
        buildingsSpawned.Remove(building);

        if (building is ProductionBuilding) {
            productionBuildingsSpawned.Remove(building as ProductionBuilding);
        }
    }

    public List<BuildingSO> GetUnlockedBuildingSOList() {
        return unlockedBuildingSOList;
    }
    public List<BuildingSO> GetLockedBuildingSOList() {
        return lockedBuildingSOList;
    }

    public List<ProductionBuilding> GetProductionBuildings() {
        return productionBuildingsSpawned;
    }

    public List<Building> GetDestinationBuildingsList(int carryCapacity) {
        List<Building> destinationBuildingList = new List<Building>();  

        foreach (Building building in buildingsSpawned) {

            if(building is ProductionBuilding) {
                ProductionBuilding productionBuilding = building as ProductionBuilding;
                if (productionBuilding.GetSelectedRecipeSO() == null) continue;

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

                    if (outputInventory.AmountInventoryHasOfType(sourceItem) > 0) {
                        sourceBuildingList.Add(productionBuilding);
                    }

                };
            }

            if (building is Chest) {
                Chest chest = (Chest)building;

                if (chest.GetChestInventory().AmountInventoryHasOfType(sourceItem) > 0) {
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

    public List<Chest> GetChestStoringEquipment(Item item) {

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

    public Item GetBestItemTierAvailable(Item item) {
        Item bestEquipment = item;

        int itemTier = (int)ItemAssets.Instance.GetItemSO(item.itemType).itemTier;
        Item.ItemEquipmentType equipmentType = ItemAssets.Instance.GetItemSO(item.itemType).itemEquipmentType;

        for (int i = itemTier; i < ItemAssets.Instance.GetMaxItemTier(); i++) {

            foreach (Building building in buildingsSpawned) {
            if (!(building is Chest)) continue;

            Chest chest = building as Chest;
            if (chest.GetItemCategoryToStore() != Item.ItemCategory.Equipment) continue;

            // We have an equipment holding chest
            foreach (Item storedItem in chest.GetChestInventory().GetItemList()) {
                    Debug.Log(storedItem.itemType);
                    // Check if we have the same item equipment type of equal or higher tier
                    if (ItemAssets.Instance.GetItemSO(storedItem.itemType).itemEquipmentType == equipmentType && (int)storedItem.itemTier == i) {
                        bestEquipment = storedItem;
                    }

                }
            }
        }
        return bestEquipment;
    }

    public List<Chest> GetChestStoringBestEquipment(Item item) {

        List<Chest> chestsStoringEquipment = new List<Chest>();

        foreach (Building building in buildingsSpawned) {
            if (!(building is Chest)) continue;

            Chest chest = building as Chest;
            if (chest.GetItemCategoryToStore() != Item.ItemCategory.Equipment) continue;

            // We have an equipment holding chest
            foreach (Item storedItem in chest.GetChestInventory().GetItemList()) {
                if (storedItem.itemType == item.itemType) {
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

    public Sprite GetWorkingCategorySprite(Building.BuildingCategory category) {
        if(category == Building.BuildingCategory.WoodWork) {
            return woodWorkCategorySprite;
        }

        if (category == Building.BuildingCategory.MetalWork) {
            return metalWorkCategorySprite;
        }

        if (category == Building.BuildingCategory.Alchemy) {
            return alchemyCategorySprite;
        }
        if (category == Building.BuildingCategory.Craftsmanship) {
            return craftsManShipCategorySprite;
        }
        if (category == Building.BuildingCategory.Housing) {
            return housingCategorySprite;
        }
        if (category == Building.BuildingCategory.FoodProduction) {
            return foodProductionCategorySprite;
        }
        if (category == Building.BuildingCategory.FoodPreparation) {
            return foodPreparationSprite;
        }
        if (category == Building.BuildingCategory.Research) {
            return researchCategorySprite;
        }
        if (category == Building.BuildingCategory.MineralProcessing) {
            return mineralProcessingSprite;
        }
        if (category == Building.BuildingCategory.Fabric) {
            return fabricProcessingSprite;
        }
        if (category == Building.BuildingCategory.Storage) {
            return storageSprite;
        }
        if (category == Building.BuildingCategory.Industry) {
            return industrySprite;
        }
        return null;
    }

}
