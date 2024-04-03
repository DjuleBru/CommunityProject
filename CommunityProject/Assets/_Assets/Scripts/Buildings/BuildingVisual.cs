using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer placingBuildingBackgroundSprite;

    [SerializeField] private Color validPlacementColor;
    [SerializeField] private Color unValidPlacementColor;

    private Building building;

    private void Awake() {
        building = GetComponentInParent<Building>();
    }
    private void Start() {
        building.OnBuildingIsUnvalidPlacement += Building_OnBuildingIsUnvalidPlacement;
        building.OnBuildingIsValidPlacement += Building_OnBuildingIsValidPlacement;
        building.OnBuildingPlaced += Building_OnBuildingPlaced;

        BuildingsManager.Instance.OnAnyBuildingPlacedOrCancelled += BuildingsManager_OnAnyBuildingPlacedOrCancelled;
        BuildingsManager.Instance.OnAnyBuildingSpawned += BuildingsManager_OnAnyBuildingSpawned;
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
}
