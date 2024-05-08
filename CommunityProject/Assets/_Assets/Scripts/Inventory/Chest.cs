using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Building
{
    [ES3Serializable]
    private Inventory chestInventory;
    [SerializeField] private List<Item> itemsInChest = new List<Item>();
    [SerializeField] private InventoryUI_Interactable chestInventoryUI;
    [SerializeField] bool isDungeonChest;
    [SerializeField] private BuildingHaulersUI_World buildingHaulersUI_World;

    private bool chestHasBeenFilled;

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
        if(isDungeonChest) {
            buildingPlaced = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            chestHasBeenFilled = false;
        }

        BuildingsManager.Instance.AddBuilding(this);
    }

    public void AddItemsToChest(List<Item> itemList) {
        foreach (Item item in itemList) {
            itemsInChest.Add(item);
        }
    }

    protected override void Update() {
        if(!isDungeonChest) {
            base.Update();
        }
        
        if(!chestHasBeenFilled) {
            if (chestInventory != null) {
                chestInventory.AddItemList(itemsInChest);
            }
            chestHasBeenFilled = true;
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
        base.LoadBuilding();
        if(chestInventory == null) {
            chestInventory = new Inventory(false, 3, 3, false, null);
        }
    }

}
