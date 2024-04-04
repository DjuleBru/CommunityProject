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
    [SerializeField] private Transform inputInventorySlot;
    [SerializeField] private Transform outputInventorySlot;

    [SerializeField] private Image progressionBarFill;

    private ProductionBuilding productionBuilding;

    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetProductionBuilding(ProductionBuilding productionBuilding) {
        this.productionBuilding = productionBuilding;
        RefreshProductionBuildingUI();
    }

    public void RefreshProductionBuildingUI() {
        nameText.text = productionBuilding.GetBuildingSO().name;
        RefreshRecipeList();
        RefreshRecipePanel();

        if(productionBuilding.GetSelectedRecipeSO() != null) {
            RefreshInventoryPanels();
        }
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

    private void RefreshInventoryPanels() {
        inputInventoryContainer.GetComponent<InventoryUI_ProductionBuilding>().SetInventory(productionBuilding.GetInputInventory());
        outputInventoryContainer.GetComponent<InventoryUI_ProductionBuilding>().SetInventory(productionBuilding.GetOutputInventory());
    }

}
