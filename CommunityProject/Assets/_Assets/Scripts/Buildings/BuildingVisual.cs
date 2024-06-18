using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingVisual : MonoBehaviour, IInteractable
{
    [SerializeField] protected SpriteRenderer placingBuildingBackgroundSprite;
    [SerializeField] protected GameObject buildingHoveredVisual;

    [SerializeField] protected Color validPlacementColor;
    [SerializeField] protected Color unValidPlacementColor;

    [SerializeField] protected Collider2D solidBuildingCollider;
    [SerializeField] protected Collider2D interactionBuildingCollider;

    [SerializeField] protected Building building;
    protected bool playerInTriggerArea;
    protected bool interactingWithBuilding;
    protected static bool buildingPanelOpen;

    [SerializeField] protected TextMeshProUGUI buildingScoreText;

    protected virtual void Start() {
        building.OnBuildingIsUnvalidPlacement += Building_OnBuildingIsUnvalidPlacement;
        building.OnBuildingIsValidPlacement += Building_OnBuildingIsValidPlacement;
        building.OnBuildingPlaced += Building_OnBuildingPlaced;

        BuildingsManager.Instance.OnAnyBuildingPlacedOrCancelled += BuildingsManager_OnAnyBuildingPlacedOrCancelled;
        BuildingsManager.Instance.OnAnyBuildingSpawned += BuildingsManager_OnAnyBuildingSpawned;

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    protected virtual void GameInput_OnInteractAction(object sender, System.EventArgs e) {

            if (!building.GetInteractingWithBuilding()) {
                if (playerInTriggerArea) {
                    InteractWithBuilding();
                }
            }
            else {
                StopInteractingWithBuilding();
            }
    }

    protected virtual void Building_OnBuildingPlaced(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.enabled = false;
    }

    protected virtual void Building_OnBuildingIsValidPlacement(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.color = validPlacementColor;
    }

    protected virtual void Building_OnBuildingIsUnvalidPlacement(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.color = unValidPlacementColor;
    }

    protected virtual void BuildingsManager_OnAnyBuildingSpawned(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.enabled = true;

    }

    protected virtual void BuildingsManager_OnAnyBuildingPlacedOrCancelled(object sender, System.EventArgs e) {
        placingBuildingBackgroundSprite.enabled = false;
    }

    protected virtual void OnDestroy() {
        BuildingsManager.Instance.OnAnyBuildingPlacedOrCancelled -= BuildingsManager_OnAnyBuildingPlacedOrCancelled;
        BuildingsManager.Instance.OnAnyBuildingSpawned -= BuildingsManager_OnAnyBuildingSpawned;
    }


    public virtual void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        if (!building.GetBuildingPlaced()) return;
        this.playerInTriggerArea = playerInTriggerArea;

        if (Player.Instance.GetClosestInteractable() == (this as IInteractable)) {
            OpenPanel();
            buildingPanelOpen = true;
        }

    }

    public void DisableBackground() {
        placingBuildingBackgroundSprite.enabled = false;
    }

    public bool GetPlayerInTriggerArea() {
        return playerInTriggerArea;
    }

    public virtual void SetHovered(bool hovered) {
        if (!building.GetBuildingPlaced()) return;
        buildingHoveredVisual.SetActive(hovered);
    }

    public virtual void InteractWithBuilding() {
        //Pass through function
        if (!building.CanInteractWithBuilding()) return;
        building.InteractWithBuilding();
        interactingWithBuilding = true;
    }

    public virtual void StopInteractingWithBuilding() {
        //Pass through function
        building.StopInteractingWithBuilding();
        interactingWithBuilding = false;
    }

    public virtual void ClosePanel() {
        building.ClosePanel();
        buildingPanelOpen = false;
    }

    public virtual void OpenPanel() {
        //Pass through function
        building.OpenBuildingUI();
    }

    public Collider2D GetSolidCollider() {
        return solidBuildingCollider;
    }

    public void SetBuildingScoreText(string score) {
        buildingScoreText.text = score;
    }

    public void SetColliderActive(bool active) {
        interactionBuildingCollider.enabled = active;
    }
}
