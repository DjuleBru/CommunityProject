using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraViewMouseTransform : MonoBehaviour
{

    public static FreeCameraViewMouseTransform Instance;
    public Building buildingHovered;

    private bool collidingActive;

    private void Awake() {
        Instance = this;
    }

    public void EnableMouseTransform(bool enable) {
        collidingActive = enable;
    }

    public Building GetBuildingHovered() {
        return buildingHovered;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collidingActive) return;
        if (HumanoidManualAssignManager.Instance.IsAssigningBuildingToHumanoid()) return;

        DungeonStatsBoard dungeonStatsBoard = collision.GetComponentInParent<DungeonStatsBoard>();

        Building building = collision.GetComponent<Building>();

        if (building != null) {
            buildingHovered = building;
            if (building is ProductionBuilding) {

                ProductionBuilding productionBuilding = (ProductionBuilding)building;

                productionBuilding.GetBuildingVisual().SetHovered(true);
                ProductionBuildingUI.Instance.gameObject.SetActive(true);
                ProductionBuildingUI.Instance.SetProductionBuilding(productionBuilding);
                productionBuilding.GetBuildingHaulersUI_World().ShowAssignedHaulers(true);

            }

            if(building is Chest) {
                Chest chest = (Chest)building;

                chest.GetChestVisual().OpenChestVisual();
                chest.GetChestVisual().SetHovered(true);
                chest.GetBuildingHaulersUI_World().ShowAssignedHaulers(true);
                chest.OpenInventory();
            }

            if (building is House) {
                House house = (House)building;
                house.GetBuildingVisual().SetHovered(true);
                house.GetHousingBuildingUIWorld().ShowAssignedHoused(true);
            }
        }

        if (dungeonStatsBoard != null) {
            dungeonStatsBoard.SetHovered(true);
            dungeonStatsBoard.OpenPanel();
            dungeonStatsBoard.GetDungeonStatsBoardWorldUI().ShowAssignedDungeoneers(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!collidingActive) return;
        Building building = collision.GetComponent<Building>();
        DungeonStatsBoard dungeonStatsBoard = collision.GetComponentInParent<DungeonStatsBoard>();

        if (building != null) {
            buildingHovered = null;
            building.GetBuildingVisual().SetHovered(false);

            if (building is ProductionBuilding) {
                ProductionBuilding productionBuilding = (ProductionBuilding)building;
                ProductionBuildingUI.Instance.gameObject.SetActive(false);
                productionBuilding.GetBuildingHaulersUI_World().ShowAssignedHaulers(false);
            }

            if(building is Chest) {
                Chest chest = (Chest)building;

                chest.GetChestVisual().CloseChestVisual();
                chest.GetChestVisual().SetHovered(false);
                chest.GetBuildingHaulersUI_World().ShowAssignedHaulers(false);
                chest.CloseInventory();
            }

            if (building is House) {
                House house = (House)building;
                house.GetBuildingVisual().SetHovered(false);
                house.GetHousingBuildingUIWorld().ShowAssignedHoused(false);
            }
        }

        if (dungeonStatsBoard != null) {
            dungeonStatsBoard.ClosePanel();
            dungeonStatsBoard.GetDungeonStatsBoardWorldUI().ShowAssignedDungeoneers(false);
        }
    }
}
