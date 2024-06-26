using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProductionBuilding : Building
{
    public event EventHandler OnWorkerStartedWorking;
    public event EventHandler OnWorkerFinishedWorking;

    protected RecipeSO selectedRecipeSO;
    protected float productionTimer;

    protected List<Inventory> inputInventoryList;
    protected List<Inventory> outputInventoryList;
    protected List<ItemWorld> itemWorldProducedList;

    protected bool working;
    protected float humanoidWorkingSpeed;
    protected bool inputItemsMissing;
    protected bool outputInventoryFull;

    [SerializeField] protected ProductionBuildingUI_World productionBuildingUIWorld;
    [SerializeField] protected BuildingHaulersUI_World buildingHaulersUI_World;
    [SerializeField] protected ProductionBuildingVisual productionBuildingvisual;

    [SerializeField] protected GameObject groundTileMap;
    protected override void Awake() {
        base.Awake();
        itemWorldProducedList = new List<ItemWorld>();
    }

    protected override void Update() {
        base.Update();

        if (playerInteractingWithBuilding) {
            Work(Player.Instance.GetPlayerWorkingSpeed(), true);
            return;
        }

        if(assignedHumanoid != null) {
            if(working) {
                Work(humanoidWorkingSpeed, false);
            }
        }
    }

    protected virtual void Work(float productionSpeed, bool isPlayerWorking) {
        productionTimer += Time.deltaTime * productionSpeed;

        if (productionTimer >= selectedRecipeSO.standardProductionTime) {
            if(ProduceSelectedRecipe(isPlayerWorking)) {
                productionTimer = 0f;
            };
        }
    }

    protected virtual bool ProduceSelectedRecipe(bool isPlayerWorking) {
        // Check input items missing
        CheckInputItems();
        if(inputItemsMissing) { return false; }

        //Produce Output Items in output inventories
        foreach (Inventory outputInventory in outputInventoryList) {
            foreach (Item outputItem in selectedRecipeSO.outputItems) {
                Item itemToProduce = new Item { itemType = outputItem.itemType, amount = outputItem.amount };

                if (isPlayerWorking) {
                    itemWorldProducedList.Add(ItemWorld.DropItem(transform.position, itemToProduce, 13f, false));
                }
                else {
                    //Identify output inventory
                    if (outputInventory.InventoryCanAcceptItem(itemToProduce)) {

                        // Check if inventory has space to accept item stack
                        if (outputInventory.AmountInventoryCanReceiveOfType(itemToProduce) >= outputItem.amount) {

                            // Add output item to this inventory
                            outputInventory.AddItem(itemToProduce);

                        }
                        else {
                            // Inventory has not enough space for output item
                            Debug.Log("no space in output inventory");
                            productionBuildingUIWorld.SetOutputInventoryFull(true);
                            outputInventoryFull = true;
                            return false;
                        }
                    }
                }
            }
        }

        // Spend Input Items in input inventories
        foreach (Inventory inputInventory in inputInventoryList) {
            foreach (Item inputItem in selectedRecipeSO.inputItems) {
                Item itemToRemove = new Item { itemType = inputItem.itemType, amount = inputItem.amount };

                if (inputInventory.HasItem(itemToRemove)) {
                    inputInventory.RemoveItemAmount(itemToRemove);
                }
            }
        }

        return true;
    }

    public override void OpenBuildingUI() {
        ProductionBuildingUI.Instance.gameObject.SetActive(true);
        ProductionBuildingUI.Instance.SetProductionBuilding(this);

    }

    public override void ClosePanel() {
        ProductionBuildingUI.Instance.gameObject.SetActive(false);
    }

    public override void InteractWithBuilding() {
        if (!inputItemsMissing && selectedRecipeSO != null && !outputInventoryFull) {
            base.InteractWithBuilding();

            playerInteractingWithBuilding = true;
            Player.Instance.SetPlayerWorking(true);
            productionBuildingvisual.SetWorking(true, HumanoidSO.HumanoidType.Human);

            working = true;
            OnWorkerStartedWorking?.Invoke(this, EventArgs.Empty);

            ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
        }
    }

    public override void StopInteractingWithBuilding() {
        base.StopInteractingWithBuilding();
        // Player is Working
        playerInteractingWithBuilding = false;
        Player.Instance.SetPlayerWorking(false);
        productionBuildingvisual.SetWorking(false, HumanoidSO.HumanoidType.Human);

        working = false;

        if(itemWorldProducedList.Count > 0) {
            // Items were dropped on the floor
            foreach(ItemWorld itemWorld in itemWorldProducedList) {
                itemWorld.SetAttractedToPlayer();
            }
        }
        OnWorkerFinishedWorking?.Invoke(this, EventArgs.Empty);
    }

    public void SetHumanoidWorking(bool working, HumanoidSO.HumanoidType humanoidType) {

        if(!playerInteractingWithBuilding) {
            this.working = working;
            productionBuildingUIWorld.SetWorking(working);
            productionBuildingvisual.SetWorking(working, humanoidType);
        }
        ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
    }

    protected override void PlaceBuilding() {
        base.PlaceBuilding();
        productionBuildingUIWorld.SetWorkerMissing(true);
        productionBuildingUIWorld.SetRecipeMissing(true);
    }

    public virtual void SetSelectedRecipeSO(RecipeSO selectedRecipeSO) {

        productionBuildingUIWorld.SetRecipeMissing(false);
        productionBuildingUIWorld.SetItemsMissing(true);

        if(this.selectedRecipeSO != selectedRecipeSO) {
            this.selectedRecipeSO = selectedRecipeSO;
            ChangeInventories();
        }
    }

    protected virtual void ChangeInventories() {
        // Unsub from previous inventory events
        if(inputInventoryList != null) {
            foreach(var inventory in inputInventoryList) {
                inventory.OnItemListChanged -= NewInventory_OnItemListChanged;
            }
        }
        if(outputInventoryList != null) {
            foreach(var inventory in outputInventoryList) {
                inventory.OnItemListChanged -= NewInventory_OnItemListChanged;
            }
        }

        //Drop all items in previous inventories
        DropInventoryItems();

        inputInventoryList = new List<Inventory>();
        outputInventoryList = new List<Inventory>();

        foreach (Item item in selectedRecipeSO.inputItems) {
            List<Item> inputInventoryItem = new List<Item>();
            inputInventoryItem.Add(item);
            Inventory newInventory = new Inventory(true, 1, 1, true, inputInventoryItem);
            inputInventoryList.Add(newInventory);
            newInventory.OnItemListChanged += NewInventory_OnItemListChanged;
        }

        foreach (Item item in selectedRecipeSO.outputItems) {
            List<Item> outputInventoryItem = new List<Item>();
            outputInventoryItem.Add(item);
            Inventory newInventory = new Inventory(true, 1, 1, true, outputInventoryItem);
            outputInventoryList.Add(newInventory);
            newInventory.OnItemListChanged += NewInventory_OnItemListChanged;
        }

        productionBuildingUIWorld.SetItemsMissing(true);
    }

    protected void NewInventory_OnItemListChanged(object sender, EventArgs e) {
        CheckInputItems();
        CheckOutputItems();
    }

    protected void DropInventoryItems() {
        if(inputInventoryList != null) {
            foreach (Inventory inventory in inputInventoryList) {
                foreach (Item item in inventory.GetItemList()) {
                    ItemWorld.DropItem(Player.Instance.transform.position, item, 5f, true).SetAttractedToPlayer();
                }
            }
        }

        if (outputInventoryList != null) {
            foreach (Inventory inventory in outputInventoryList) {
                foreach (Item item in inventory.GetItemList()) {
                    ItemWorld.DropItem(Player.Instance.transform.position, item, 5f, true).SetAttractedToPlayer();
                }
            }
        }
    }

    public virtual float GetProductionTimerNormalized() {
        return (productionTimer / selectedRecipeSO.standardProductionTime);
    }

    public bool GetWorking() {
        return working;
    }

    public List<Inventory> GetInputInventoryList() {
        return inputInventoryList;
    }

    public List<Inventory> GetOutputInventoryList() {
        return outputInventoryList;
    }

    public RecipeSO GetSelectedRecipeSO() {
        return selectedRecipeSO;
    }

    public virtual void CheckInputItems() {

        if (selectedRecipeSO != null) {

            List<Item> missingItemsList = new List<Item>();

            foreach (Item item in selectedRecipeSO.inputItems) {
                missingItemsList.Add(new Item { itemType = item.itemType, amount = item.amount });
            }

            Item itemToRemoveFromMissingItemsList = null;

            if (inputInventoryList != null) {
                foreach (Inventory inventory in inputInventoryList) {
                    foreach (Item item in missingItemsList) {
                        if (inventory.HasItem(item)) {
                            itemToRemoveFromMissingItemsList = item;
                        }
                    }
                    missingItemsList.Remove(itemToRemoveFromMissingItemsList);
                }
            }

            if (missingItemsList.Count == 0) {
                inputItemsMissing = false;
            }
            else {
                inputItemsMissing = true;
                working = false;
            }
        }

        RefreshProductionBuildingUIWorld();
    }

    public void CheckOutputItems() {
        bool atLeastOneOutputInventoryFull = false;

        if (selectedRecipeSO != null) {
            foreach (Inventory inventory in outputInventoryList) {
                foreach(Item item in selectedRecipeSO.outputItems) {
                    if (!inventory.InventoryCanAcceptItem(item)) {
                        atLeastOneOutputInventoryFull = true;
                    }
                }
            }
        }

        outputInventoryFull = atLeastOneOutputInventoryFull;
        RefreshProductionBuildingUIWorld();
    }

    public override void AssignHumanoid(Humanoid humanoid) {
        this.assignedHumanoid = humanoid;
        productionBuildingUIWorld.SetWorkerMissing(false);

        ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
        SetHumanoidWorkingSpeed();
    }

    public override void ReplaceAssignedHumanoid(Humanoid humanoid) {
        if(assignedHumanoid != null) {
            assignedHumanoid.RemoveAssignedBuilding();
        }
        assignedHumanoid = humanoid;
        assignedHumanoid.AssignBuilding(this);
        ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
        SetHumanoidWorkingSpeed();
    }

    public override void RemoveAssignedHumanoid() {
        this.assignedHumanoid = null;
        ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
        productionBuildingUIWorld.SetWorkerMissing(true);
    }

    private void SetHumanoidWorkingSpeed() {

        if(buildingSO.statAffectingProductivity == Humanoid.Stat.strength) {
            humanoidWorkingSpeed = assignedHumanoid.GetStrength() /5f;
        }

        if (buildingSO.statAffectingProductivity == Humanoid.Stat.intelligence) {
            humanoidWorkingSpeed = assignedHumanoid.GetIntelligence() / 5f;
        }

        if (buildingSO.statAffectingProductivity == Humanoid.Stat.agility) {
            humanoidWorkingSpeed = assignedHumanoid.GetAgility() / 5f;
        }

        if (buildingSO.statAffectingProductivity == Humanoid.Stat.speed) {
            humanoidWorkingSpeed = assignedHumanoid.GetMoveSpeed() / 5f;
        }

        if(assignedHumanoid.GetHumanoidSO().humanoidProficiencies.Contains(buildingSO.buildingCategory)) {
            humanoidWorkingSpeed *= 1.5f;
        }

    }

    public float GetHumanoidWorkingSpeed() {
        return humanoidWorkingSpeed;
    }

    public Humanoid GetAssignedHumanoid() {
        return assignedHumanoid;
    }

    public void RefreshProductionBuildingUIWorld() {
        productionBuildingUIWorld.SetItemsMissing(inputItemsMissing);
        productionBuildingUIWorld.SetOutputInventoryFull(outputInventoryFull);
    }

    public ProductionBuildingUI_World GetProductionBuildingUIWorld() {
        return productionBuildingUIWorld;
    }

    public BuildingHaulersUI_World GetBuildingHaulersUI_World() {
        return buildingHaulersUI_World;
    }

    public virtual bool GetInputItemsMissing() {
        return inputItemsMissing;
    }

    public bool GetOutputInventoryFull() {
        return outputInventoryFull;
    }

    public bool GetPlayerInteractingWithBuilding() {
        return playerInteractingWithBuilding;
    }

    public override void LoadBuilding() {
        base.LoadBuilding();

        if(inputInventoryList == null) {
            inputInventoryList = new List<Inventory>();
        }

        if(outputInventoryList == null) {
            outputInventoryList = new List<Inventory>();
        }
 
        if(assignedHumanoid == null) {
            productionBuildingUIWorld.SetWorkerMissing(true);
        } else {
            productionBuildingUIWorld.SetWorkerMissing(false);
        }

        if (selectedRecipeSO == null) {
            productionBuildingUIWorld.SetRecipeMissing(true);
        }
        else {
            productionBuildingUIWorld.SetRecipeMissing(false);
        }

        if(inputItemsMissing) {
            productionBuildingUIWorld.SetItemsMissing(true);
        } else {
            productionBuildingUIWorld.SetItemsMissing(false);
        }

        if (outputInventoryFull) {
            productionBuildingUIWorld.SetOutputInventoryFull(true);
        }
        else {
            productionBuildingUIWorld.SetOutputInventoryFull(false);
        }

    }


}
