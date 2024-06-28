using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBuildingUI : BuildingUI
{
    public static ProductionBuildingUI Instance { get; private set; }

    [SerializeField] private OpenHumanoidUIButton openHumanoidUIButton;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI workerNameText;
    [SerializeField] private TextMeshProUGUI workerDescriptionText;
    [SerializeField] private TextMeshProUGUI selectRecipeText;
    [SerializeField] private TextMeshProUGUI recipeOriginalTimeText;
    [SerializeField] private TextMeshProUGUI recipeModifiedTimeText;
    [SerializeField] private TextMeshProUGUI workerProductionSpeedText;

    [SerializeField] private Image workerImage;
    [SerializeField] private Image workerReplacementImage;
    [SerializeField] private TextMeshProUGUI workerReplacementNameText;
    [SerializeField] private GameObject workerReplacementGameObject;

    [SerializeField] private Transform recipeContainer;
    [SerializeField] private Transform recipeTemplate;

    [SerializeField] private GameObject recipeDescriptionPanel;
    [SerializeField] private GameObject processingGameObject;
    [SerializeField] private Transform inputIngredientsContainer;
    [SerializeField] private Transform outputIngredientsContainer;
    [SerializeField] private Transform inputTemplateSlot;
    [SerializeField] private Transform outputTemplateSlot;

    [SerializeField] private GameObject researchPanelGameObject;
    [SerializeField] private Transform remainingItemsContainer;
    [SerializeField] private Transform remainingItemsTemplate;
    [SerializeField] private Image researchImage;
    [SerializeField] private TextMeshProUGUI researchNameText;

    [SerializeField] private Transform inputInventoryContainer;
    [SerializeField] private Transform outputInventoryContainer;
    [SerializeField] private Transform inputInventoryTemplate;
    [SerializeField] private Transform outputInventoryTemplate;

    [SerializeField] private Image progressionBarFill;

    [SerializeField] private Image buildingIcon;
    [SerializeField] private Transform preferredWorkersContainer;
    [SerializeField] private Transform preferredWorkersTemplate;

    [SerializeField] private Image statAffectingProductivity;

    private bool panelOpen;
    private ProductionBuilding productionBuilding;

    private void Awake() {
        Instance = this;
        workerReplacementGameObject.SetActive(false);
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

    public virtual void RefreshProductionBuildingUI() {
        if (productionBuilding == null) return;

        nameText.text = productionBuilding.GetBuildingSO().name;
        RefreshWorkerPanel();
        RefreshNamePanel();

        if (productionBuilding is ArchitectTable) {
            RefreshArchitectTableUI();
            return;
        } else {
            processingGameObject.SetActive(true);
            researchPanelGameObject.SetActive(false);
            remainingItemsContainer.gameObject.SetActive(false);
        }

        RefreshRecipeList();
        RefreshRecipePanel();
        RefreshInventoryPanels();

        productionBuilding.CheckInputItems();
        if(productionBuilding.GetSelectedRecipeSO() != null) {
            RefreshRecipeTimer();
        }
    }

    private void RefreshNamePanel() {
        buildingIcon.sprite = productionBuilding.GetBuildingSO().buildingIconSprite;
        statAffectingProductivity.sprite = HumanoidsManager.Instance.GetStatSprite(productionBuilding.GetBuildingSO().statAffectingProductivity);

        foreach (Transform child in preferredWorkersContainer) {
            if (child == preferredWorkersTemplate) continue;
            Destroy(child.gameObject);
        }

        List<HumanoidSO> humanoidSOList = HumanoidsManager.Instance.GetBuildingHumanoidTypeProficiency(productionBuilding.GetBuildingSO());
        foreach (HumanoidSO humanoidSO in humanoidSOList) {
            Transform preferredWorker = Instantiate(preferredWorkersTemplate, preferredWorkersContainer);
            preferredWorker.Find("Icon").GetComponent<Image>().sprite = humanoidSO.humanoidSprite;
            preferredWorker.gameObject.SetActive(true);
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

            if(productionBuilding.GetBuildingSO().buildingRecipes.Count <= 4) {
                recipeContainer.GetComponent<GridLayoutGroup>().padding.left = 25;
                recipeContainer.GetComponent<GridLayoutGroup>().padding.top = 25;
                recipeContainer.GetComponent<GridLayoutGroup>().spacing = new Vector2(10, 0);
                recipeContainer.GetComponent<GridLayoutGroup>().cellSize = new Vector2(65,65);
            } else {

                recipeContainer.GetComponent<GridLayoutGroup>().padding.left = 15;
                recipeContainer.GetComponent<GridLayoutGroup>().padding.top = 15;
                recipeContainer.GetComponent<GridLayoutGroup>().spacing = new Vector2(6, 7);
                recipeContainer.GetComponent<GridLayoutGroup>().cellSize = new Vector2(40, 40);
            }

            RectTransform recipeTemplateRectTransform = Instantiate(recipeTemplate, recipeContainer).GetComponent<RectTransform>();
            recipeTemplateRectTransform.GetComponent<RecipeSlotTemplate>().SetRecipe(recipeSO);
            recipeTemplateRectTransform.gameObject.SetActive(true);
            recipeTemplateRectTransform.Find("RecipeIcon").GetComponent<Image>().sprite = ItemAssets.Instance.GetItemSO(recipeSO.outputItems[0].itemType).itemSprite;

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
            recipeOriginalTimeText.text = selectedRecipeSO.standardProductionTime.ToString() + "s";

            if(productionBuilding.GetHumanoidWorkingSpeed() != 0) {

                float productionSpeed = productionBuilding.GetHumanoidWorkingSpeed() * 100f;
                workerProductionSpeedText.text = "(" + productionSpeed.ToString() + "%)";
                float modifiedProductionTime = (selectedRecipeSO.standardProductionTime / (productionSpeed / 100));
                modifiedProductionTime = Mathf.Round(modifiedProductionTime * 10.0f) * 0.1f;
                recipeModifiedTimeText.text = modifiedProductionTime.ToString() + "s";
            } else {
                recipeModifiedTimeText.text = "";
                workerProductionSpeedText.text = "";

            }
        }
        else {
            recipeDescriptionPanel.gameObject.SetActive(false);
            selectRecipeText.text = "Select a Recipe";
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
        if (productionBuilding.GetAssignedHumanoid() != null) {
            Humanoid assignedHumanoid = productionBuilding.GetAssignedHumanoid();

            openHumanoidUIButton.EnableButton(true);
            openHumanoidUIButton.SetHumanoid(assignedHumanoid);

            workerImage.enabled = true;

            workerImage.sprite = assignedHumanoid.GetHumanoidSO().humanoidSprite;
            workerNameText.text = assignedHumanoid.GetHumanoidName();

            workerDescriptionText.text = assignedHumanoid.GetHumanoidActionDescription();
        } else {

            openHumanoidUIButton.EnableButton(false);
            workerImage.enabled = false;
            workerNameText.text = "";
            workerDescriptionText.text = "No humanoid assigned to this building";

        }
    }

    public void RefreshArchitectTableUI() {

        processingGameObject.SetActive(false);

        foreach (Transform child in inputInventoryContainer) {
            if (child.GetComponent<InventoryUI_ProductionBuilding>() != null) {
                if (child != inputInventoryTemplate) {
                    Destroy(child.gameObject);
                }
            }
        }

        if (ResearchMenuUI.Instance.GetCurrentResearch() == null) {
            selectRecipeText.text = "Select a research";
            researchPanelGameObject.SetActive(false);
            return;
        }

        researchImage.sprite = ResearchMenuUI.Instance.GetCurrentResearchSprite();
        researchNameText.text = ResearchMenuUI.Instance.GetCurrentResearchName();
        remainingItemsContainer.gameObject.SetActive(true);
        researchPanelGameObject.SetActive(true);

        selectRecipeText.text = "";
        int itemCount = 0;

        foreach (Transform child in remainingItemsContainer) {
            if (child == remainingItemsTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Item item in ResearchMenuUI.Instance.GetCurrentResearch().remainingItemList) {

            Transform itemTemplate = Instantiate(remainingItemsTemplate, remainingItemsContainer);
            itemTemplate.GetComponent<ItemSlot>().SetItem(item);
            itemTemplate.gameObject.SetActive(true);

            RectTransform inputInventorySlotRectTransform = Instantiate(inputInventoryTemplate, inputInventoryContainer).GetComponent<RectTransform>();

            inputInventorySlotRectTransform.GetComponent<InventoryUI_ProductionBuilding>().SetInventory(productionBuilding.GetInputInventoryList()[itemCount]);
            inputInventorySlotRectTransform.gameObject.SetActive(true);
            itemCount++;
        }
    }

    public void SetWorkerReplacement(Humanoid humanoid) {
        workerReplacementGameObject.SetActive(true);
        workerReplacementImage.sprite = humanoid.GetHumanoidSO().humanoidSprite;
        workerReplacementNameText.text = humanoid.GetHumanoidName();
    }

    public void StopSettingWorkerReplacement() {
        workerReplacementGameObject.SetActive(false);
    }

    private void OnDisable() {
        RecipeTooltipUI.Instance.ResetTooltip();
    }

}
