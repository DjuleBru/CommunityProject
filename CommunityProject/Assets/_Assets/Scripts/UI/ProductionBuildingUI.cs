using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuildingUI : BuildingUI
{
    public static ProductionBuildingUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI workerNameText;
    [SerializeField] private TextMeshProUGUI workerDescriptionText;
    [SerializeField] private TextMeshProUGUI selectRecipeText;
    [SerializeField] private TextMeshProUGUI recipeTimeText;

    [SerializeField] private Image workerImage;

    [SerializeField] private Transform recipeContainer;
    [SerializeField] private Transform recipeTemplate;

    [SerializeField] private GameObject recipeDescriptionPanel;
    [SerializeField] private Transform inputIngredientsContainer;
    [SerializeField] private Transform outputIngredientsContainer;
    [SerializeField] private Transform inputTemplateSlot;
    [SerializeField] private Transform outputTemplateSlot;

    [SerializeField] private Transform inputInventoryContainer;
    [SerializeField] private Transform outputInventoryContainer;
    [SerializeField] private Transform inputInventoryTemplate;
    [SerializeField] private Transform outputInventoryTemplate;

    [SerializeField] private Image progressionBarFill;

    private bool panelOpen;
    private ProductionBuilding productionBuilding;

    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update() {
        if(productionBuilding != null) {
            if(productionBuilding.GetWorking()) {
                RefreshRecipeTimer();
            }
        }
    }

    public void SetProductionBuilding(ProductionBuilding productionBuilding) {
        this.productionBuilding = productionBuilding;
        RefreshProductionBuildingUI();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshProductionBuildingUI();
    }

    public void RefreshProductionBuildingUI() {
        nameText.text = productionBuilding.GetBuildingSO().name;
        RefreshRecipeList();
        RefreshRecipePanel();
        RefreshInventoryPanels();
        RefreshWorkerPanel();

        productionBuilding.CheckInputItems();
        if(productionBuilding.GetSelectedRecipeSO() != null) {
            RefreshRecipeTimer();
        }
    }

    public void RefreshRecipeTimer() {
        progressionBarFill.fillAmount = productionBuilding.GetProductionTimerNormalized();
    }

    public void RefreshRecipeList() {
        foreach (Transform child in recipeContainer) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in productionBuilding.GetBuildingSO().buildingRecipes) {
            RectTransform recipeTemplateRectTransform = Instantiate(recipeTemplate, recipeContainer).GetComponent<RectTransform>();

            recipeTemplateRectTransform.gameObject.SetActive(true);
            recipeTemplateRectTransform.Find("RecipeIcon").GetComponent<Image>().sprite = recipeSO.recipeSprite;

            if(productionBuilding.GetSelectedRecipeSO() != null) {
                // Production building has a recipe selected
                if (productionBuilding.GetSelectedRecipeSO() == recipeSO) {
                    // This recipe SO is selected
                    recipeTemplateRectTransform.GetComponent<RecipeSlot_Selectable>().SetRecipeSlot(recipeSO, productionBuilding, true);
                } else {
                    recipeTemplateRectTransform.GetComponent<RecipeSlot_Selectable>().SetRecipeSlot(recipeSO, productionBuilding, false);
                }
            } else {
                // There is no recipeSO selected
                recipeTemplateRectTransform.GetComponent<RecipeSlot_Selectable>().SetRecipeSlot(recipeSO, productionBuilding, false);
            }
        }
    }

    public void RefreshRecipePanel() {
        RecipeSO selectedRecipeSO = productionBuilding.GetSelectedRecipeSO();
        if (selectedRecipeSO != null) {

            recipeDescriptionPanel.gameObject.SetActive(true);
            selectRecipeText.gameObject.SetActive(false);

            RefreshRecipeItemsList(inputIngredientsContainer, inputTemplateSlot, selectedRecipeSO.inputItems);
            RefreshRecipeItemsList(outputIngredientsContainer, outputTemplateSlot, selectedRecipeSO.outputItems);
            recipeTimeText.text = selectedRecipeSO.standardProductionTime.ToString() + "s";
        }
        else {
            recipeDescriptionPanel.gameObject.SetActive(false);
            selectRecipeText.gameObject.SetActive(true);
        }
    }

    private void RefreshRecipeItemsList(Transform itemContainer, Transform itemTemplate, List<Item> itemList) {
        foreach (Transform child in itemContainer) {
            if (child == itemTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Item item in itemList) {
            RectTransform itemTemplateRectTransform = Instantiate(itemTemplate, itemContainer).GetComponent<RectTransform>();

            itemTemplateRectTransform.gameObject.SetActive(true);

            itemTemplateRectTransform.GetComponent<ItemSlot>().SetItem(item);
        }

    }

    public void RefreshInventoryPanels() {

        foreach (Transform child in inputInventoryContainer) {
            if (child.GetComponent<InventoryUI_ProductionBuilding>() != null) {
                if (child != inputInventoryTemplate) {
                    Destroy(child.gameObject);
                }
            }
        }

        foreach (Transform child in outputInventoryContainer) {
            if (child.GetComponent<InventoryUI_ProductionBuilding>() != null) {

                if (child != outputInventoryTemplate) {
                    Destroy(child.gameObject);
                }
            }
        }

        if (productionBuilding.GetSelectedRecipeSO() == null) return;

        int itemCount = 0;
        foreach (Item item in productionBuilding.GetSelectedRecipeSO().inputItems) {

            RectTransform inputInventorySlotRectTransform = Instantiate(inputInventoryTemplate, inputInventoryContainer).GetComponent<RectTransform>();

            inputInventorySlotRectTransform.GetComponent<InventoryUI_ProductionBuilding>().SetInventory(productionBuilding.GetInputInventoryList()[itemCount]);
            inputInventorySlotRectTransform.gameObject.SetActive(true);
            itemCount++;
        }

        itemCount = 0;
        foreach (Item item in productionBuilding.GetSelectedRecipeSO().outputItems) {

            RectTransform outputInventorySlotRectTransform = Instantiate(outputInventoryTemplate, outputInventoryContainer).GetComponent<RectTransform>();

            outputInventorySlotRectTransform.GetComponent<InventoryUI_ProductionBuilding>().SetInventory(productionBuilding.GetOutputInventoryList()[itemCount]);
            outputInventorySlotRectTransform.gameObject.SetActive(true);
            itemCount++;
        }
    }

    public void RefreshWorkerPanel() {
        if (productionBuilding == null) return;
        if(productionBuilding.GetAssignedHumanoid() != null) {
            Humanoid assignedHumanoid = productionBuilding.GetAssignedHumanoid();
            workerImage.enabled = true;

            workerImage.sprite = assignedHumanoid.GetHumanoidSO().humanoidSprite;
            workerNameText.text = assignedHumanoid.GetHumanoidName();

            workerDescriptionText.text = assignedHumanoid.GetHumanoidActionDescription();
        } else {

            workerImage.enabled = false;
            workerNameText.text = "";
            workerDescriptionText.text = "No humanoid assigned to this building";

        }
    }

}
