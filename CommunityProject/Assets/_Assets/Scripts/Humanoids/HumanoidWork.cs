using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidWork : MonoBehaviour
{
    private Humanoid humanoid;
    private bool working;
    public event EventHandler OnHumanoidWorkStarted;
    public event EventHandler OnHumanoidWorkStopped;


    private void Awake() {
        humanoid = GetComponent<Humanoid>();
    }

    public void Work() {
        ProductionBuilding assignedBuilding = humanoid.GetAssignedBuilding() as ProductionBuilding;
        if (assignedBuilding.GetSelectedRecipeSO() == null || assignedBuilding.GetInputItemsMissing() || assignedBuilding.GetOutputInventoryFull() || assignedBuilding.GetPlayerInteractingWithBuilding()) {
            if(working) {
                working = false;
                assignedBuilding.SetHumanoidWorking(false, humanoid.GetHumanoidSO().humanoidType);
                OnHumanoidWorkStopped?.Invoke(this, EventArgs.Empty);
            }

        } else {
            if(!working) {
                working = true;
                assignedBuilding.SetHumanoidWorking(true, humanoid.GetHumanoidSO().humanoidType);
                OnHumanoidWorkStarted?.Invoke(this, EventArgs.Empty);
                humanoid.SetHumanoidActionDescription("Working");
            }
        }
    }

    [Button]
    public ProductionBuilding FindBestWorkingBuilding() {
        List<ProductionBuilding> productionBuildingsList = BuildingsManager.Instance.GetProductionBuildings();

        float bestBuildingScore = 0f;
        ProductionBuilding bestBuilding = null;

        foreach (ProductionBuilding building in productionBuildingsList) {
            float score = CalculateBuildingScore(building);
            //building.SetBuildingVisualDebugScore(score.ToString());

            if(score > bestBuildingScore) {
                bestBuildingScore = score;
                bestBuilding = building;
            }
        }

        return bestBuilding;
    }

    private float CalculateBuildingScore(ProductionBuilding building) {
        if (building.GetSelectedRecipeSO() == null) return 0f;
        if (building.GetAssignedHumanoid() != null) return 0f;

        float distanceToBuilding = Vector3.Distance(transform.position, building.transform.position);
        return 1/distanceToBuilding;
    }

    public bool GetWorking() {
        return working;
    }

    public void StopWorking() {
        ProductionBuilding assignedBuilding = humanoid.GetAssignedBuilding() as ProductionBuilding;
        working = false;
        assignedBuilding.SetHumanoidWorking(false, humanoid.GetHumanoidSO().humanoidType);
        OnHumanoidWorkStopped?.Invoke(this, EventArgs.Empty);
    }
}
