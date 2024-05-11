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
            Debug.Log("null chest inventory - creating one");
            chestInventory = new Inventory(false, 3, 3, false, null);
        }

        BuildingsManager.Instance.AddBuilding(this);
    }

    public void AddItemsToChest(List<Item> itemList) {

        if (chestInventory == null) {
            Debug.Log("null chest inventory - creating one");
            chestInventory = new Inventory(false, 3, 3, false, null);
        }

        foreach (Item item in itemList) {
            Debug.Log("adding " + item.itemType + " " + item.amount);
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
    }

    public void CloseInventory() {
        chestInventoryUI.CloseInventoryPanel();
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

    public override void LoadBuilding() {
        if (isDungeonChest) {
            buildingPlaced = true;
        }

        base.LoadBuilding();
    }

}
