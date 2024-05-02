using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanoidManualAssignManager : MonoBehaviour
{
    public static HumanoidManualAssignManager Instance { get; private set; }

    private bool assigningBuildingToHumanoid;

    private Humanoid humanoid;
    private Collider2D mousePositionCollider;

    private ProductionBuilding productionBuildingHovered;
    private Chest chestHovered;

    private bool haulerOutputBuildingAssigned;

    private void Awake() {
        Instance = this;
        mousePositionCollider = GetComponent<Collider2D>();
        mousePositionCollider.enabled = false;
    }

    private void Update() {
        if (assigningBuildingToHumanoid) {
            if(Input.GetMouseButtonDown(1)) {
                //Right click : cancel assigning
                SetAssigningBuildingToHumanoid(false, null);
            }

            if(Input.GetMouseButtonDown(0)) {
                // Left click : assign building 
                if(humanoid.IsWorker()) {
                    HandleWorkerBuildingAssignment();
                }

                if(humanoid.IsHaulier()) {
                    HandleHaulierBuildingAssignment();
                }
            }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    private void HandleWorkerBuildingAssignment() {
        if (productionBuildingHovered != null) {
            productionBuildingHovered.ReplaceAssignedHumanoid(humanoid);
            ProductionBuildingUI.Instance.StopSettingWorkerReplacement();
            StopAssignmentMode();
        }
    }

    private void HandleHaulierBuildingAssignment() {
        if(!haulerOutputBuildingAssigned) {

            if (productionBuildingHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceDestinationBuildingAssigned(productionBuildingHovered);
                haulerOutputBuildingAssigned = true;
                productionBuildingHovered.GetProductionBuildingUIWorld().RefreshAssignedHaulers();
            }

            if(chestHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceDestinationBuildingAssigned(chestHovered);
                haulerOutputBuildingAssigned = true;
                chestHovered.GetChestUIWorld().RefreshAssignedHaulers();
            }

        } else {

            if (productionBuildingHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceSourceBuildingAssigned(productionBuildingHovered);
                productionBuildingHovered.GetProductionBuildingUIWorld().RefreshAssignedHaulers();
                haulerOutputBuildingAssigned = false;
                StopAssignmentMode();
            }

            if (chestHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceSourceBuildingAssigned(chestHovered);
                haulerOutputBuildingAssigned = true;
                chestHovered.GetChestUIWorld().RefreshAssignedHaulers();
                StopAssignmentMode();
            }
        }
    }

    public void SetAssigningBuildingToHumanoid(bool assigning, Humanoid humanoid) {
        assigningBuildingToHumanoid = assigning;
        this.humanoid = humanoid;

        if (assigningBuildingToHumanoid) {

            if(!FreeCameraViewManager.Instance.CameraIsInFreeView()) {
                FreeCameraViewManager.Instance.SetFreeCamera(true);
            }

            StartAssignmentMode();
        } else {
            StopAssignmentMode();
        }
    }

    private void StartAssignmentMode() {
        mousePositionCollider.enabled = true;
        HumanoidsMenuUI.Instance.OpenCloseHumanoidsMenu();
    }

    private void StopAssignmentMode() {
        mousePositionCollider.enabled = false;
        HumanoidsMenuUI.Instance.OpenCloseHumanoidsMenu();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Building building = collision.GetComponent<Building>();

        if(building != null) {
            if (building is ProductionBuilding) {

                ProductionBuilding productionBuilding = (ProductionBuilding)building;
                productionBuildingHovered = productionBuilding;

                if (humanoid.IsWorker()) {
                    ProductionBuildingUI.Instance.SetWorkerReplacement(humanoid);
                }

                if(humanoid.IsHaulier()) {
                    if(!haulerOutputBuildingAssigned) {
                        productionBuildingHovered.GetProductionBuildingUIWorld().ShowPotentialInputHaulerAssign(humanoid);
                    } else {
                        productionBuildingHovered.GetProductionBuildingUIWorld().ShowPotentialOutputHaulerAssign(humanoid);
                    }
                }

            }

            if(building is Chest) {
                Chest chest = (Chest)building;
                chestHovered = chest;

                if (humanoid.IsHaulier()) {
                    if (!haulerOutputBuildingAssigned) {
                        chestHovered.GetChestUIWorld().ShowPotentialInputHaulerAssign(humanoid);
                    }
                    else {
                        chestHovered.GetChestUIWorld().ShowPotentialOutputHaulerAssign(humanoid);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Building building = collision.GetComponent<Building>();

        if (building != null) {
            building.GetBuildingVisual().SetHovered(false);

            if(building is ProductionBuilding) {
                ProductionBuildingUI.Instance.StopSettingWorkerReplacement();
                productionBuildingHovered.GetProductionBuildingUIWorld().ShowAssignedHaulers(false);
                productionBuildingHovered = null;
            }

            if(building is Chest) {

                chestHovered.CloseInventory();
                chestHovered = null;
            }
        }
    }
}
