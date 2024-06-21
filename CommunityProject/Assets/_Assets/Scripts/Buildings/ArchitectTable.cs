using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectTable : ProductionBuilding
{
    private float timeToProcessItem = 5f;

    protected override void Start() {
        base.Start();
        ChangeInventories();
        if (ResearchMenuUI.Instance.GetCurrentResearch() != null) {
            productionBuildingUIWorld.SetRecipeMissing(false);
        }
        ResearchMenuUI.Instance.OnSelectedResearchChanged += ResearchMenuUI_OnSelectedResearchChanged;
        ResearchMenuUI.Instance.OnSelectedResearchFinished += ResearchMenuUI_OnSelectedResearchFinished;
    }

    public override void OpenBuildingUI() {
        base.OpenBuildingUI();
    }


    protected override void Work(float productionSpeed, bool isPlayerWorking) {
        productionTimer += Time.deltaTime * productionSpeed;

        if (productionTimer >= timeToProcessItem) {
            if (ProduceSelectedRecipe(isPlayerWorking)) {
                productionTimer = 0f;
            };
        }
    }

    protected override void ChangeInventories() {
        // Unsub from previous inventory events
        if (inputInventoryList != null) {
            foreach (var inventory in inputInventoryList) {
                inventory.OnItemListChanged -= NewInventory_OnItemListChanged;
            }
        }

        //Drop all items in previous inventories
        DropInventoryItems();

        inputInventoryList = new List<Inventory>();

        if (ResearchMenuUI.Instance.GetCurrentResearch() == null) return;

        foreach (Item item in ResearchMenuUI.Instance.GetCurrentResearch().remainingItemList) {
            Debug.Log("creating an inventory for " +  item.itemType);
            List<Item> inputInventoryItem = new List<Item>();
            inputInventoryItem.Add(item);
            Inventory newInventory = new Inventory(true, 1, 1, true, inputInventoryItem);
            inputInventoryList.Add(newInventory);
            newInventory.OnItemListChanged += NewInventory_OnItemListChanged;
        }

        productionBuildingUIWorld.SetItemsMissing(true);
    }

    protected override bool ProduceSelectedRecipe(bool isPlayerWorking) {
        CheckInputItems();
        if (inputItemsMissing) { return false; }

        // Progress in research and spend items

        List<Item> remainingItemsForResearch = new List<Item>();
        foreach(Item item in ResearchMenuUI.Instance.GetCurrentResearch().remainingItemList) {
            remainingItemsForResearch.Add(item);
        }

        foreach(Item item in remainingItemsForResearch) {

            Item itemToRemove = new Item { itemType = item.itemType, amount = 1 };
            foreach (Inventory inventory in inputInventoryList) {

                if (inventory.GetItemList().Count == 0) continue;
                if (!inventory.HasItem(itemToRemove)) continue;
                if (inventory.GetItemList()[0].amount != 0) {

                    if (inventory.HasItem(itemToRemove)) {
                        inventory.RemoveItemAmount(itemToRemove);
                    }

                    ResearchMenuUI.Instance.ProgressInSelectedResearch(itemToRemove);
                }
            }
        }
       
        ProductionBuildingUI.Instance.RefreshArchitectTableUI();

        return true;
    }


    private void ResearchMenuUI_OnSelectedResearchFinished(object sender, System.EventArgs e) {
        ChangeInventories();
        ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
    }

    private void ResearchMenuUI_OnSelectedResearchChanged(object sender, System.EventArgs e) {
        ChangeInventories();
        ProductionBuildingUI.Instance.RefreshProductionBuildingUI();

        if(ResearchMenuUI.Instance.GetCurrentResearch() != null) {
            productionBuildingUIWorld.SetRecipeMissing(false);
        }
    }

    public override float GetProductionTimerNormalized() {
        return (productionTimer / timeToProcessItem);
    }

    public override void CheckInputItems() {

        if (ResearchMenuUI.Instance.GetCurrentResearch() != null) {

            List<Item> missingItemsList = new List<Item>();

            foreach (Item item in ResearchMenuUI.Instance.GetCurrentResearch().remainingItemList) {
                if (item.amount == 0) continue;
                missingItemsList.Add(new Item { itemType = item.itemType, amount = 1 });
            }

            bool inputItemMissing = true;
            foreach(Item item in missingItemsList) {

                foreach(Inventory inventory in inputInventoryList) {
                    if(inventory.HasItem(item)) {
                        // There are items in inventory
                        inputItemMissing = false;
                    }
                }
            }

            if (!inputItemMissing) {
                // There are items in inventory
                inputItemsMissing = false;
            }
            else {
                // There are no items in inventory
                inputItemsMissing = true;
                working = false;
            }
        }

        RefreshProductionBuildingUIWorld();
    }


    public override bool GetInputItemsMissing() {
        CheckInputItems();
        return inputItemsMissing;
    }

}
