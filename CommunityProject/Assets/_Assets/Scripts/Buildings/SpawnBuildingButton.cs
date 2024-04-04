using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BuildingSO buildingSO;

    [SerializeField] private Image buildingIcon;

    public void OnPointerEnter(PointerEventData eventData) {
        BuildingDescriptionPanelUI.Instance.gameObject.SetActive(true);
        BuildingDescriptionPanelUI.Instance.SetBuildingSO(buildingSO);
    }

    public void OnPointerExit(PointerEventData eventData) {
    }

    public void SetBuildingSO(BuildingSO buildingSO) {
        this.buildingSO = buildingSO;
        buildingIcon.sprite = buildingSO.buildingIconSprite;
    }

    public void SpawnBuilding() {
        if(HasBuildingMaterials()) {
            Instantiate(buildingSO.buildingPrefab);
        } else {
            BuildingDescriptionPanelUI.Instance.ShowUnsufficientMaterialsVisual();
        }
    }

    private bool HasBuildingMaterials() {
        // Check if there are enough resources in player
        foreach (Item item in buildingSO.buildingCostItems) {
            if (!Player.Instance.GetInventory().HasItem(item)) {
                return false;
            }
        }
        return true;
    }

}
