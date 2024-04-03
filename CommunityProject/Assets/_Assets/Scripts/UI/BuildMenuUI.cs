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

    private Building.BuildingUICategory buildingCategory;

    private void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void SetBuildMenuUI(Building.BuildingUICategory buildingCategory) {
        this.buildingCategory = buildingCategory;
        RefreshBuildMenuUI();
    }

    protected void RefreshBuildMenuUI() {
        titleText.text = buildingCategory.ToString();

        foreach (Transform child in buildingButtonContainer) {
            if (child == buildingButtonTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (BuildingSO buildingSO in BuildingAssets.Instance.GetBuildingSOsInCategory(buildingCategory)) {
            RectTransform buildingButtonRectTransform = Instantiate(buildingButtonTemplate, buildingButtonContainer).GetComponent<RectTransform>();

            buildingButtonRectTransform.gameObject.SetActive(true);
            SpawnBuildingButton buildingButton = buildingButtonRectTransform.GetComponent<SpawnBuildingButton>();
            buildingButton.SetBuildingSO(buildingSO);
        }
    }

    public Building.BuildingUICategory GetBuildMenuUICategory() {
        return buildingCategory;
    }

    public void OnPointerExit(PointerEventData eventData) {
        BuildingDescriptionPanelUI.Instance.gameObject.SetActive(false);
    }
}
