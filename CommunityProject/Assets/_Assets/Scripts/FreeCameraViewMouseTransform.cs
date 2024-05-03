using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraViewMouseTransform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if (HumanoidManualAssignManager.Instance.IsAssigningBuildingToHumanoid()) return;
        DungeonStatsBoard dungeonStatsBoard = collision.GetComponentInParent<DungeonStatsBoard>();

        Building building = collision.GetComponent<Building>();

        if (building != null) {
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
        }

        if (dungeonStatsBoard != null) {
            dungeonStatsBoard.SetHovered(true);
            dungeonStatsBoard.OpenPanel();
            dungeonStatsBoard.GetDungeonStatsBoardWorldUI().ShowAssignedDungeoneers(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Building building = collision.GetComponent<Building>();
        DungeonStatsBoard dungeonStatsBoard = collision.GetComponentInParent<DungeonStatsBoard>();

        if (building != null) {
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
        }

        if (dungeonStatsBoard != null) {
            dungeonStatsBoard.ClosePanel();
            dungeonStatsBoard.GetDungeonStatsBoardWorldUI().ShowAssignedDungeoneers(false);
        }
    }
}
