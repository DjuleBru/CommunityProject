using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuildingUI_World : MonoBehaviour
{
    [SerializeField] private GameObject missingItemsUI;
    [SerializeField] private GameObject missingSelectedRecipeUI;
    [SerializeField] private GameObject missingWorkerUI;
    [SerializeField] private GameObject outputInventoryFull;

    [SerializeField] private ProductionBuilding productionBuilding;

    [SerializeField] private GameObject progressionBarGameObject;
    [SerializeField] private Image progressionBarFill;

    private bool working;

    private void Start() {
        missingItemsUI.SetActive(false);
        missingSelectedRecipeUI.SetActive(false);
        missingWorkerUI.SetActive(false);
        outputInventoryFull.SetActive(false);
        progressionBarGameObject.SetActive(false);
    }

    private void Update() {
        if(working) {
            RefreshRecipeTimer();
        }
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

    public void RefreshRecipeTimer() {
        progressionBarFill.fillAmount = productionBuilding.GetProductionTimerNormalized();
    }

    public void SetWorking(bool working) {
        this.working = working;

        if(working) {
            progressionBarGameObject.SetActive(true);
        } else {
            progressionBarGameObject.SetActive(false);
        }
    }

}
