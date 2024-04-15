using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : Building
{

    private RecipeSO selectedRecipeSO;

    private List<Inventory> inputInventoryList;
    private List<Inventory> outputInventoryList;

    private bool inputItemsMissing;

    [SerializeField] private ProductionBuildingUI_World productionBuildingUIWorld;
    [SerializeField] private ProductionBuildingVisual productionBuildingvisual;

    public override void OpenBuildingUI() {
        ProductionBuildingUI.Instance.gameObject.SetActive(true);
        ProductionBuildingUI.Instance.SetProductionBuilding(this);

    }

    public override void ClosePanel() {
        ProductionBuildingUI.Instance.gameObject.SetActive(false);
    }

    public override void InteractWithBuilding() {
        if (!inputItemsMissing) {
            interactingWithBuilding = true;
            Player.Instance.SetPlayerWorking(true);
            productionBuildingvisual.SetWorking(true);
        }
    }

    public override void StopInteractingWithBuilding() {
        // Player is Working
        interactingWithBuilding = false;
        Player.Instance.SetPlayerWorking(false);
        productionBuildingvisual.SetWorking(false);
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
        //Drop all items in previous inventories
        DropInventoryItems();

        inputInventoryList = new List<Inventory>();
        outputInventoryList = new List<Inventory>();

        foreach (Item item in selectedRecipeSO.inputItems) {
            List<Item> inputInventoryItem = new List<Item>();
            inputInventoryItem.Add(item);
            inputInventoryList.Add(new Inventory(true, 1, 1, true, inputInventoryItem));
        }

        foreach (Item item in selectedRecipeSO.outputItems) {
            List<Item> outputInventoryItem = new List<Item>();
            outputInventoryItem.Add(item);
            outputInventoryList.Add(new Inventory(true, 1, 1, true, outputInventoryItem));
        }

        productionBuildingUIWorld.SetItemsMissing(true);
    }

    private void DropInventoryItems() {
        if(inputInventoryList != null) {
            foreach (Inventory inventory in inputInventoryList) {
                foreach (Item item in inventory.GetItemList()) {
                    ItemWorld.DropItem(Player.Instance.transform.position, item, true);
                }
            }
        }
        if (outputInventoryList != null) {
            foreach (Inventory inventory in outputInventoryList) {
                foreach (Item item in inventory.GetItemList()) {
                    ItemWorld.DropItem(Player.Instance.transform.position, item, true);
                }
            }
        }
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
