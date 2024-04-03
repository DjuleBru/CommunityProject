using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCategoryUI : MonoBehaviour
{
    [SerializeField] private Building.BuildingUICategory buildingCategory;
    public static bool buildMenuOpen;

    public void OpenCloseBuildMenu() {

        if(!buildMenuOpen) {
            buildMenuOpen = true;
            BuildMenuUI.Instance.gameObject.SetActive(true);
            SetBuildMenuUICategory();
        } else {
            if(BuildMenuUI.Instance.GetBuildMenuUICategory() == buildingCategory) {
                // Player clicked on the same category
                buildMenuOpen = false;
                BuildMenuUI.Instance.gameObject.SetActive(false);
            } else {
                BuildMenuUI.Instance.SetBuildMenuUI(buildingCategory);
            }
        }
    }

    public void SetBuildMenuUICategory() {
        BuildMenuUI.Instance.SetBuildMenuUI(buildingCategory);
    }
}
