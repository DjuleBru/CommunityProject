using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnBuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BuildingSO buildingSO;
    private Button button;

    [SerializeField] private Image buildingIcon;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=> {
            SpawnBuilding();
        });
    }

    public void OnPointerEnter(PointerEventData eventData) {
        BuildingDescriptionPanelUI.Instance.gameObject.SetActive(true);
        BuildingDescriptionPanelUI.Instance.SetBuildingSO(buildingSO);
    }

    public void OnPointerExit(PointerEventData eventData) {
    }

    public void SetBuildingSO(BuildingSO buildingSO, bool buildingLocked) {
        this.buildingSO = buildingSO;
        buildingIcon.sprite = buildingSO.buildingIconSprite;

        if(buildingLocked) {
            button.interactable = false;
        } else {
            button.interactable = true;
        }
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
