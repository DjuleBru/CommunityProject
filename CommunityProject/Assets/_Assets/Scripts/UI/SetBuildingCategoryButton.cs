using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBuildingCategoryButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Building.BuildingCategory buildingCategory;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            BuildMenuUI.Instance.SetBuildingCategory(buildingCategory);
        });
    }

    public void SetBuildingCategory(Building.BuildingCategory buildingCategory, bool categoryLocked) {
        this.buildingCategory = buildingCategory;
        icon.sprite = BuildingsManager.Instance.GetWorkingCategorySprite(buildingCategory);

        if(categoryLocked) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
    }
}
