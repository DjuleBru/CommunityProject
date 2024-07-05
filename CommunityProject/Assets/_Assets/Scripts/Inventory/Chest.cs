using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Building
{
    [ES3Serializable]
    private Inventory chestInventory;
    [SerializeField] private InventoryUI_Interactable chestInventoryUI;
    [SerializeField] bool isDungeonChest;
    [SerializeField] private BuildingHaulersUI_World buildingHaulersUI_World;

    [SerializeField] private Item.ItemCategory itemCategoryToStore;

    public static event EventHandler OnAnyChestOpened;
    public static event EventHandler OnAnyChestClosed;

    protected override void Awake() {
        rb = GetComponent<Rigidbody2D>();
        buildingCollider = GetComponent<Collider2D>();

        if (!isDungeonChest) {
            interactionCollider.enabled = false;
            buildingCollider.isTrigger = true;
            isValidBuildingPlacement = true;
        }

        buildingCamera.enabled = false;
    }

    protected override void Start() {
        base.Start();

        if (chestInventory == null) {

            if(itemCategoryToStore == Item.ItemCategory.All) {
                chestInventory = new Inventory(false, 3, 3, false, null);
            } else {
                List<Item> itemsRestricted = ItemAssets.Instance.GetItemListOfCategory(itemCategoryToStore);

                chestInventory = new Inventory(false, 3, 3, true, itemsRestricted);
            }
        }
    }

    public void AddItemsToChest(List<Item> itemList) {

        if (chestInventory == null) {
            chestInventory = new Inventory(false, 3, 3, false, null);
        }

        foreach (Item item in itemList) {
            chestInventory.AddItem(item);
        }
    }

    protected override void Update() {
        if(!isDungeonChest) {
            base.Update();
        }
    }

    public void OpenInventory() {
        chestInventoryUI.SetInventory(chestInventory); 
        chestInventoryUI.OpenCloseInventoryPanel();
        OnAnyChestOpened?.Invoke(this, EventArgs.Empty);
    }

    public void CloseInventory() {
        chestInventoryUI.CloseInventoryPanel();
        OnAnyChestClosed?.Invoke(this, EventArgs.Empty);
    }

    public Inventory GetChestInventory() {
        return chestInventory;
    }

    public BuildingHaulersUI_World GetBuildingHaulersUI_World() {
        return buildingHaulersUI_World;
    }

    public ChestVisual GetChestVisual() {
        return buildingVisual as ChestVisual;
    }

    public Item.ItemCategory GetItemCategoryToStore() {  return itemCategoryToStore; }
    public override void LoadBuilding() {
        if (isDungeonChest) {
            buildingPlaced = true;
        }

        base.LoadBuilding();
    }

}
