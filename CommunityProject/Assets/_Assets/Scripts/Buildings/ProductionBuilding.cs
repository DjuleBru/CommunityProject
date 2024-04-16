using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class ProductionBuilding : Building
{
    public event EventHandler OnWorkerStartedWorking;
    public event EventHandler OnWorkerFinishedWorking;

    private RecipeSO selectedRecipeSO;
    private float productionTimer;

    private List<Inventory> inputInventoryList;
    private List<Inventory> outputInventoryList;
    private List<ItemWorld> itemWorldProducedList;

    private bool working;
    private bool inputItemsMissing;

    [SerializeField] private ProductionBuildingUI_World productionBuildingUIWorld;
    [SerializeField] private ProductionBuildingVisual productionBuildingvisual;

    protected override void Awake() {
        base.Awake();
        itemWorldProducedList = new List<ItemWorld>();
    }

    protected override void Update() {
        base.Update();

        if (playerInteractingWithBuilding) {
            Work(Player.Instance.GetPlayerWorkingSpeed(), true);
        }
    }

    protected void Work(float productionSpeed, bool isPlayerWorking) {
        productionTimer += Time.deltaTime * productionSpeed;

        if(productionTimer >= selectedRecipeSO.standardProductionTime) {
            ProduceSelectedRecipe(isPlayerWorking);
            productionTimer = 0f;
        }
    }

    protected void ProduceSelectedRecipe(bool isPlayerWorking) {
        // Check input items missing
        CheckInputItems();
        if(inputItemsMissing) { return; }

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
                            return;
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
    }

    public override void OpenBuildingUI() {
        ProductionBuildingUI.Instance.gameObject.SetActive(true);
        ProductionBuildingUI.Instance.SetProductionBuilding(this);

    }

    public override void ClosePanel() {
        ProductionBuildingUI.Instance.gameObject.SetActive(false);
    }

    public override void InteractWithBuilding() {
        if (!inputItemsMissing && selectedRecipeSO != null) {
            base.InteractWithBuilding();

            playerInteractingWithBuilding = true;
            Player.Instance.SetPlayerWorking(true);
            productionBuildingvisual.SetWorking(true);

            working = true;
            OnWorkerStartedWorking?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void StopInteractingWithBuilding() {
        base.StopInteractingWithBuilding();
        // Player is Working
        playerInteractingWithBuilding = false;
        Player.Instance.SetPlayerWorking(false);
        productionBuildingvisual.SetWorking(false);

        working = false;

        if(itemWorldProducedList.Count > 0) {
            // Items were dropped on the floor
            foreach(ItemWorld itemWorld in itemWorldProducedList) {
                itemWorld.SetAttractedToPlayer();
            }
        }
        OnWorkerFinishedWorking?.Invoke(this, EventArgs.Empty);
    }

    protected override void PlaceBuilding() {
        base.PlaceBuilding();
        productionBuildingUIWorld.SetWorkerMissing(true);
        productionBuildingUIWorld.SetRecipeMissing(true);
    }

    public void SetSelectedRecipeSO(RecipeSO selectedRecipeSO) {

        productionBuildingUIWorld.SetRecipeMissing(false);
        productionBuildingUIWorld.SetItemsMissing(true);

        if(this.selectedRecipeSO != selectedRecipeSO) {
            this.selectedRecipeSO = selectedRecipeSO;
            ChangeInventories();
        }
    }

    private void ChangeInventories() {
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

    private void NewInventory_OnItemListChanged(object sender, EventArgs e) {
        CheckInputItems();
    }

    private void DropInventoryItems() {
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

    public float GetProductionTimerNormalized() {
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

    public void CheckInputItems() {

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
            }
        }

        RefreshProductionBuildingUIWorld();
    }

    public void RefreshProductionBuildingUIWorld() {
        productionBuildingUIWorld.SetItemsMissing(inputItemsMissing);
    }

}
