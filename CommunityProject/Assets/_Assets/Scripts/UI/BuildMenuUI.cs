using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildMenuUI : MonoBehaviour, IPointerExitHandler {

    public static BuildMenuUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform buildingButtonTemplate;
    [SerializeField] private Transform buildingButtonContainer;


    [SerializeField] private Transform buildingCategoryTemplate;
    [SerializeField] private Transform buildingCategoryContainer;

    [SerializeField] private List<Building.BuildingCategory> rawStationCategories;
    [SerializeField] private List<Building.BuildingCategory> assemblyStationCategories;
    [SerializeField] private List<Building.BuildingCategory> foodProductionCategories;
    [SerializeField] private List<Building.BuildingCategory> containerCategories;
    [SerializeField] private List<Building.BuildingCategory> utilityCategories;

    private Building.BuildingUICategory buildingUICategory;
    private Building.BuildingCategory buildingCategory;

    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetBuildMenuUI(Building.BuildingUICategory buildingCategory) {
        this.buildingUICategory = buildingCategory;
        RefreshBuildCategoriesUI();
    }

    protected void RefreshBuildCategoriesUI() {
        titleText.text = buildingUICategory.ToString();

        List<Building.BuildingCategory> buildingCategoryList = GetBuildingCategoryListFromUICategory();

        RefreshBuildingCategoryButtonsUI(buildingCategoryList);
    }

    protected void RefreshBuildingCategoryButtonsUI(List<Building.BuildingCategory> buildingCategoryList) {

        foreach (Transform child in buildingCategoryContainer) {
            if (child == buildingCategoryTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(Building.BuildingCategory category in buildingCategoryList) {
            RectTransform buildingButtonRectTransform = Instantiate(buildingCategoryTemplate, buildingCategoryContainer).GetComponent<RectTransform>();
            buildingButtonRectTransform.gameObject.SetActive(true);
            SetBuildingCategoryButton categoryButton = buildingButtonRectTransform.GetComponent<SetBuildingCategoryButton>();
            categoryButton.SetBuildingCategory(category);
            buildingCategory = buildingCategoryList[0];
        }

        RefreshBuildingButtons();
    }

    protected void RefreshBuildingButtons() {

        foreach (Transform child in buildingButtonContainer) {
            if (child == buildingButtonTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (BuildingSO buildingSO in BuildingAssets.Instance.GetBuildingSOsInCategory(buildingUICategory, buildingCategory)) {
            RectTransform buildingButtonRectTransform = Instantiate(buildingButtonTemplate, buildingButtonContainer).GetComponent<RectTransform>();

            buildingButtonRectTransform.gameObject.SetActive(true);
            SpawnBuildingButton buildingButton = buildingButtonRectTransform.GetComponent<SpawnBuildingButton>();
            buildingButton.SetBuildingSO(buildingSO);
        }
    }

    private List<Building.BuildingCategory> GetBuildingCategoryListFromUICategory() {

        if(buildingUICategory == Building.BuildingUICategory.rawStations) {
            return rawStationCategories;
        }

        if (buildingUICategory == Building.BuildingUICategory.AssemblyStation) {
            return assemblyStationCategories;
        }

        if (buildingUICategory == Building.BuildingUICategory.FoodProduction) {
            return foodProductionCategories;
        }

        if (buildingUICategory == Building.BuildingUICategory.Storage) {
            return containerCategories;
        }

        if (buildingUICategory == Building.BuildingUICategory.Utility) {
            return utilityCategories;
        }
        return null;
    }

    public Building.BuildingUICategory GetBuildMenuUICategory() {
        return buildingUICategory;
    }

    public void OnPointerExit(PointerEventData eventData) {
        BuildingDescriptionPanelUI.Instance.gameObject.SetActive(false);
    }

    public void SetBuildingCategory(Building.BuildingCategory category) {
        this.buildingCategory = category;
        Debug.Log(buildingUICategory);
        Debug.Log(buildingCategory);
        RefreshBuildingButtons();
    }
}
