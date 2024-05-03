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

    private bool assigningDestionationBuilding;

    private void Awake() {
        Instance = this;
        mousePositionCollider = GetComponent<Collider2D>();
        mousePositionCollider.enabled = false;
    }

    private void Update() {
        if (assigningBuildingToHumanoid) {
            if(Input.GetMouseButtonDown(1)) {
                //Right click : cancel assigning
                SetAssigningBuildingToHumanoid(false, null, false);
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
        }
        StopAssignmentMode();
    }

    private void HandleHaulierBuildingAssignment() {
        if(assigningDestionationBuilding) {

            if (productionBuildingHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceDestinationBuildingAssigned(productionBuildingHovered);
                productionBuildingHovered.GetBuildingHaulersUI_World().RefreshAssignedHaulers();
            }

            if(chestHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceDestinationBuildingAssigned(chestHovered);
                chestHovered.GetBuildingHaulersUI_World().RefreshAssignedHaulers();
            }

            StopAssignmentMode();

        } else {

            if (productionBuildingHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceSourceBuildingAssigned(productionBuildingHovered);
                productionBuildingHovered.GetBuildingHaulersUI_World().RefreshAssignedHaulers();
            }

            if (chestHovered != null) {
                humanoid.GetComponent<HumanoidCarry>().ReplaceSourceBuildingAssigned(chestHovered);
                chestHovered.GetBuildingHaulersUI_World().RefreshAssignedHaulers();
            }

            StopAssignmentMode();
        }
    }

    public void SetAssigningBuildingToHumanoid(bool assigning, Humanoid humanoid, bool assigningDestinationBuilding) {
        assigningBuildingToHumanoid = assigning;
        this.humanoid = humanoid;
        this.assigningDestionationBuilding = assigningDestinationBuilding;

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
        assigningBuildingToHumanoid = false;
        mousePositionCollider.enabled = false;
        HumanoidsMenuUI.Instance.OpenCloseHumanoidsMenu();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Building building = collision.GetComponent<Building>();


        if(building != null) {
            if (building is ProductionBuilding) {

                ProductionBuilding productionBuilding = (ProductionBuilding)building;
                productionBuildingHovered = productionBuilding;

                ShowProductionBuildingInfo(productionBuilding);

                if (humanoid.IsWorker()) {
                    ProductionBuildingUI.Instance.SetWorkerReplacement(humanoid);
                }

                if(humanoid.IsHaulier()) {
                    if(assigningDestionationBuilding) {
                        productionBuildingHovered.GetBuildingHaulersUI_World().ShowPotentialInputHaulerAssign(humanoid);
                    } else {
                        productionBuildingHovered.GetBuildingHaulersUI_World().ShowPotentialOutputHaulerAssign(humanoid);
                    }
                }

            }

            if(building is Chest) {
                Chest chest = (Chest)building;
                chestHovered = chest;

                if (humanoid.IsHaulier()) {

                    ShowChestInfo(chest);
                    if (assigningDestionationBuilding) {
                        chestHovered.GetBuildingHaulersUI_World().ShowPotentialInputHaulerAssign(humanoid);
                    }
                    else {
                        chestHovered.GetBuildingHaulersUI_World().ShowPotentialOutputHaulerAssign(humanoid);
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
                productionBuildingHovered.GetBuildingHaulersUI_World().ShowAssignedHaulers(false);
                productionBuildingHovered = null;
            }

            if(building is Chest) {

                chestHovered.CloseInventory();
                chestHovered = null;
            }
        }
    }

    private void ShowProductionBuildingInfo(ProductionBuilding productionBuilding) {
        productionBuilding.GetBuildingVisual().SetHovered(true);
        ProductionBuildingUI.Instance.gameObject.SetActive(true);
        ProductionBuildingUI.Instance.SetProductionBuilding(productionBuilding);
        productionBuilding.GetBuildingHaulersUI_World().ShowAssignedHaulers(true);
    }

    private void ShowChestInfo(Chest chest) {
        chest.GetChestVisual().OpenChestVisual();
        chest.GetChestVisual().SetHovered(true);
        chest.GetBuildingHaulersUI_World().ShowAssignedHaulers(true);
        chest.OpenInventory();
    } 

    public bool IsAssigningBuildingToHumanoid() {
        return assigningBuildingToHumanoid;
    }
}
