using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuilding : Building
{

    private RecipeSO selectedRecipeSO;

    private Inventory inputInventory;
    private Inventory outputInventory;

    public override void OpenBuildingUI() {
        ProductionBuildingUI.Instance.gameObject.SetActive(true);
        ProductionBuildingUI.Instance.SetProductionBuilding(this);

    }

    public override void CloseBuildingUI() {
        ProductionBuildingUI.Instance.gameObject.SetActive(false);
    }

    public void SetSelectedRecipeSO(RecipeSO selectedRecipeSO) {
        this.selectedRecipeSO = selectedRecipeSO;
        ChangeInventories();
    }

    private void ChangeInventories() {
        int inputItemNumber = selectedRecipeSO.inputItems.Count;
        int outputItemNumber = selectedRecipeSO.outputItems.Count;

        inputInventory = new Inventory(true, 1, inputItemNumber);
        outputInventory = new Inventory(true, 1, outputItemNumber);
    }

    public Inventory GetInputInventory() {
        return inputInventory;
    }

    public Inventory GetOutputInventory() {
        return outputInventory;
    }

    public RecipeSO GetSelectedRecipeSO() {
        return selectedRecipeSO;
    }

}
