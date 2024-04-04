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

    [SerializeField] Image buildingBackgroundImage;
    [SerializeField] Image buildingWorksCategory;
    [SerializeField] Image bestWorkerIcon;

    [SerializeField] Transform buildingCostContainer;
    [SerializeField] Transform itemCostTemplate;
    [SerializeField] Transform recipeContainer;
    [SerializeField] Transform recipeTemplate;
    [SerializeField] private Animator unsufficientMaterialsAnimator;

    private BuildingSO buildingSO;

    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetBuildingSO(BuildingSO buildingSO) {
        this.buildingSO = buildingSO;
        RefreshBuildingDescriptionPanel();
    }

    private void RefreshBuildingDescriptionPanel() {
        buildingNameText.text = buildingSO.name;
        buildingDescriptionText.text = buildingSO.buildingDescription;
        buildingWorksText.text = buildingSO.buildingWorksCategory.ToString();

        RefreshBuildingCost();
        RefreshBuildingRecipes();
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
            recipeTemplateRectTransform.Find("RecipeIcon").GetComponent<Image>().sprite = recipeSO.recipeSprite;
        }
    }
    public void ShowUnsufficientMaterialsVisual() {
        unsufficientMaterialsAnimator.SetTrigger("UnsufficientMaterials");
    }

}
