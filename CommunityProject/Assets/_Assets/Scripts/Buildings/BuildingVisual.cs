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

    [SerializeField] private Building building;
    private bool playerInTriggerArea;
    private bool interactingWithBuilding;
    private bool buildingPanelOpen;
    
    private void Start() {
        Debug.Log(this + " " + building);
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

                if (!building.GetInteractingWithBuilding()) {
                    InteractWithBuilding();
                }

                ClosePanel();
                buildingPanelOpen = false;
            }
            else {
                if(building.GetInteractingWithBuilding()) {
                    StopInteractingWithBuilding();
                }
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

        if(buildingPanelOpen) {
            OpenPanel();
        }
    }

    public void SetHovered(bool hovered) {
        if (!building.GetBuildingPlaced()) return;
        buildingHoveredVisual.SetActive(hovered);
    }

    public virtual void InteractWithBuilding() {
        //Pass through function
        building.InteractWithBuilding();
        interactingWithBuilding = true;
    }

    public virtual void StopInteractingWithBuilding() {

        //Pass through function
        building.StopInteractingWithBuilding();
        interactingWithBuilding = false;
    }

    public void ClosePanel() {
        building.ClosePanel();
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
