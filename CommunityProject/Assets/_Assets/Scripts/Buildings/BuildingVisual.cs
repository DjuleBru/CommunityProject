using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingVisual : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer placingBuildingBackgroundSprite;
    [SerializeField] private GameObject buildingHoveredVisual;

    [SerializeField] private Color validPlacementColor;
    [SerializeField] private Color unValidPlacementColor;

    [SerializeField] private Collider2D solidBuildingCollider;

    private Building building;
    private bool playerInTriggerArea;
    private bool buildingPanelOpen;

    private void Awake() {
        building = GetComponentInParent<Building>();
    }
    
    private void Start() {
        building.OnBuildingIsUnvalidPlacement += Building_OnBuildingIsUnvalidPlacement;
        building.OnBuildingIsValidPlacement += Building_OnBuildingIsValidPlacement;
        building.OnBuildingPlaced += Building_OnBuildingPlaced;

        BuildingsManager.Instance.OnAnyBuildingPlacedOrCancelled += BuildingsManager_OnAnyBuildingPlacedOrCancelled;
        BuildingsManager.Instance.OnAnyBuildingSpawned += BuildingsManager_OnAnyBuildingSpawned;

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (playerInTriggerArea) {
            if (buildingPanelOpen) {
                ClosePanel();
                buildingPanelOpen = false;
            }
            else {
                OpenPanel();
                buildingPanelOpen = true;
            }
        }
    }

    private void Building_OnBuildingPlaced(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.enabled = false;
    }

    private void Building_OnBuildingIsValidPlacement(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.color = validPlacementColor;
    }

    private void Building_OnBuildingIsUnvalidPlacement(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.color = unValidPlacementColor;
    }

    private void BuildingsManager_OnAnyBuildingSpawned(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.enabled = true;

    }

    private void BuildingsManager_OnAnyBuildingPlacedOrCancelled(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.enabled = false;
    }

    private void OnDestroy() {
        BuildingsManager.Instance.OnAnyBuildingPlacedOrCancelled -= BuildingsManager_OnAnyBuildingPlacedOrCancelled;
        BuildingsManager.Instance.OnAnyBuildingSpawned -= BuildingsManager_OnAnyBuildingSpawned;
    }


    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        if (!building.GetBuildingPlaced()) return;
        this.playerInTriggerArea = playerInTriggerArea;
    }

    public void SetHovered(bool hovered) {
        if (!building.GetBuildingPlaced()) return;
        buildingHoveredVisual.SetActive(hovered);
    }

    public virtual void ClosePanel() {
        //Pass through function
        building.CloseBuildingUI();
        buildingPanelOpen = false;
    }

    public void OpenPanel() {
        //Pass through function
        building.OpenBuildingUI();
    }

    public Collider2D GetSolidCollider() {
        return solidBuildingCollider;
    }
}
