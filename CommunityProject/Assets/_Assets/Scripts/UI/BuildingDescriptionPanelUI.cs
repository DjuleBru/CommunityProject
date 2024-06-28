using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDescriptionPanelUI : MonoBehaviour
{
    public static BuildingDescriptionPanelUI Instance { get; private set; }

    [SerializeField] TextMeshProUGUI buildingNameText;
    [SerializeField] TextMeshProUGUI buildingDescriptionText;
    [SerializeField] TextMeshProUGUI buildingWorksText;
    [SerializeField] TextMeshProUGUI preferredWorkersText;
    [SerializeField] TextMeshProUGUI recipeText;

    [SerializeField] Image buildingBackgroundImage;
    [SerializeField] Image buildingWorksCategory;
    [SerializeField] Image bestWorkerIcon;

    [SerializeField] private Transform preferredWorkersContainer;
    [SerializeField] private Transform preferredWorkersTemplate;

    [SerializeField] private Image statAffectingProductivity;
    [SerializeField] private GameObject statAffectingProductivityGameObject;

    [SerializeField] Transform buildingCostContainer;
    [SerializeField] Transform itemCostTemplate;
    [SerializeField] Transform recipeContainer;
    [SerializeField] Transform recipeTemplate;
    [SerializeField] private Animator unsufficientMaterialsAnimator;

    private BuildingSO buildingSO;

    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
        preferredWorkersTemplate.gameObject.SetActive(false);
    }

    public void SetBuildingSO(BuildingSO buildingSO) {
        this.buildingSO = buildingSO;
        RefreshBuildingDescriptionPanel();
    }

    private void RefreshBuildingDescriptionPanel() {

        buildingNameText.text = buildingSO.name;
        buildingDescriptionText.text = buildingSO.buildingDescription;
        buildingWorksText.text = buildingSO.buildingCategory.ToString();

        buildingWorksCategory.sprite = BuildingsManager.Instance.GetWorkingCategorySprite(buildingSO.buildingCategory);
        RefreshStatAffectingProductivity();

        if (buildingSO.buildingCategory != Building.BuildingCategory.Housing) {
            recipeContainer.GetComponent<GridLayoutGroup>().spacing = new Vector2(5,0);
            preferredWorkersText.gameObject.SetActive(true);
            recipeText.text = "Recipes";
            RefreshPreferredWorkers();
            RefreshBuildingCost();
            RefreshBuildingRecipes();

        } else {
            recipeContainer.GetComponent<GridLayoutGroup>().spacing = new Vector2(2, 0);
            preferredWorkersText.gameObject.SetActive(false);
            recipeText.text = "Housing capacity";

            foreach (Transform child in recipeContainer) {
                if (child == recipeTemplate) continue;
                Destroy(child.gameObject);
            }

            if (buildingSO.buildingCategory == Building.BuildingCategory.Housing) {
                for (int i = 0; i < buildingSO.housingCapacity; i++) {
                    Transform humanoidTemplate = Instantiate(preferredWorkersTemplate, recipeContainer);
                    humanoidTemplate.gameObject.SetActive(true);
                }
                return;
            }
        }

    }

    private void RefreshPreferredWorkers() {

        foreach(Transform child in  preferredWorkersContainer) {
            if (child == preferredWorkersTemplate) continue;
            Destroy(child.gameObject);
        }

        List<HumanoidSO> humanoidSOList = HumanoidsManager.Instance.GetBuildingHumanoidTypeProficiency(buildingSO);
        foreach(HumanoidSO humanoidSO in humanoidSOList) {
            Transform preferredWorker = Instantiate(preferredWorkersTemplate, preferredWorkersContainer);
            preferredWorker.Find("Icon").GetComponent<Image>().sprite = humanoidSO.humanoidSprite;
            preferredWorker.gameObject.SetActive(true);
        }
    }

    private void RefreshStatAffectingProductivity() {
        if(buildingSO.buildingUICategory != Building.BuildingUICategory.Storage && buildingSO.buildingUICategory != Building.BuildingUICategory.Utility) {
            statAffectingProductivityGameObject.SetActive(true);
            statAffectingProductivity.sprite = HumanoidsManager.Instance.GetStatSprite(buildingSO.statAffectingProductivity);
        } else {
            statAffectingProductivityGameObject.SetActive(false);
        }
    }

    private void RefreshBuildingCost() {
        foreach (Transform child in buildingCostContainer) {
            if (child == itemCostTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Item item in buildingSO.buildingCostItems) {
            RectTransform itemCostTemplateRectTransform = Instantiate(itemCostTemplate, buildingCostContainer).GetComponent<RectTransform>();

            itemCostTemplateRectTransform.gameObject.SetActive(true);
            itemCostTemplateRectTransform.Find("ItemIcon").GetComponent<Image>().sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
            itemCostTemplateRectTransform.GetComponentInChildren<TextMeshProUGUI>().text = item.amount.ToString();

        }
    }
    private void RefreshBuildingRecipes() {
        foreach (Transform child in recipeContainer) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }


        foreach (RecipeSO recipeSO in buildingSO.buildingRecipes) {
            RectTransform recipeTemplateRectTransform = Instantiate(recipeTemplate, recipeContainer).GetComponent<RectTransform>();

            recipeTemplateRectTransform.gameObject.SetActive(true);
            recipeTemplateRectTransform.Find("RecipeIcon").GetComponent<Image>().sprite = ItemAssets.Instance.GetItemSO(recipeSO.outputItems[0].itemType).itemSprite;
            recipeTemplateRectTransform.GetComponent<RecipeSlotTemplate>().SetRecipe(recipeSO);
        }

        if(buildingSO.buildingRecipes.Count <= 4) {
            recipeContainer.GetComponent<GridLayoutGroup>().cellSize = new Vector2(50, 50);
            recipeContainer.GetComponent<GridLayoutGroup>().padding.left = 15;
            recipeContainer.GetComponent<GridLayoutGroup>().padding.top = 20;
        } else {
            recipeContainer.GetComponent<GridLayoutGroup>().cellSize = new Vector2(35, 35);
            recipeContainer.GetComponent<GridLayoutGroup>().padding.left = 12;
            recipeContainer.GetComponent<GridLayoutGroup>().padding.top = 11;
        }

    }
    public void ShowUnsufficientMaterialsVisual() {
        unsufficientMaterialsAnimator.SetTrigger("UnsufficientMaterials");
    }

}
