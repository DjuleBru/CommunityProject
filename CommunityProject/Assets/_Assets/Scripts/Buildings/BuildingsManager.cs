using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{

    public static BuildingsManager Instance { get; private set; }

    private List<Building> buildingsSpawned;
    private List<ProductionBuilding> productionBuildingsSpawned;

    public event EventHandler OnAnyBuildingSpawned;
    public event EventHandler OnAnyBuildingPlacedOrCancelled;

    private void Awake() {
        Instance = this;
        buildingsSpawned = new List<Building>();
        productionBuildingsSpawned = new List<ProductionBuilding>();
    }

    public void SetBuildingSpawned() {
        OnAnyBuildingSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void SetBuildingPlacedOrCancelled() {
        OnAnyBuildingPlacedOrCancelled?.Invoke(this, EventArgs.Empty);
    }

    public void AddBuilding(Building building) {
        buildingsSpawned.Add(building);

        if(building is ProductionBuilding) {
            productionBuildingsSpawned.Add(building as ProductionBuilding);
        }
    }

    public void RemoveBuilding(Building building) {
        buildingsSpawned.Remove(building);

        if (building is ProductionBuilding) {
            productionBuildingsSpawned.Remove(building as ProductionBuilding);
        }
    }

    public List<ProductionBuilding> GetProductionBuildings() {
        return productionBuildingsSpawned;
    }
}
