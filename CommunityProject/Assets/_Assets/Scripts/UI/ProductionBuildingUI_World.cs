using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuildingUI_World : MonoBehaviour
{
    [SerializeField] private GameObject missingItemsUI;
    [SerializeField] private GameObject missingSelectedRecipeUI;
    [SerializeField] private GameObject missingWorkerUI;
    [SerializeField] private GameObject outputInventoryFull;

    private void Awake() {
        missingItemsUI.SetActive(false);
        missingSelectedRecipeUI.SetActive(false);
        missingWorkerUI.SetActive(false);
        outputInventoryFull.SetActive(false);
    }

    public void SetItemsMissing(bool missing) {
        missingItemsUI.SetActive(missing);
    }

    public void SetRecipeMissing(bool missing) {
        missingSelectedRecipeUI.SetActive(missing);
    }

    public void SetWorkerMissing(bool assigned) {
        missingWorkerUI.SetActive(assigned);
    }

    public void SetOutputInventoryFull(bool full) {
        outputInventoryFull.SetActive(full);
    }

}
