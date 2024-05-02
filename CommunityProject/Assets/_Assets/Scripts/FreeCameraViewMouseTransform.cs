using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraViewMouseTransform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        Building building = collision.GetComponent<Building>();

        if (building != null) {
            if (building is ProductionBuilding) {

                ProductionBuilding productionBuilding = (ProductionBuilding)building;

                productionBuilding.GetBuildingVisual().SetHovered(true);
                ProductionBuildingUI.Instance.gameObject.SetActive(true);
                ProductionBuildingUI.Instance.SetProductionBuilding(productionBuilding);
                productionBuilding.GetProductionBuildingUIWorld().ShowAssignedHaulers(true);

            }

            if(building is Chest) {
                Chest chest = (Chest)building;

                chest.GetChestVisual().OpenChestVisual();
                chest.GetChestVisual().SetHovered(true);
                chest.GetChestUIWorld().ShowAssignedHaulers(true);
                chest.OpenInventory();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Building building = collision.GetComponent<Building>();

        if (building != null) {
            building.GetBuildingVisual().SetHovered(false);

            if (building is ProductionBuilding) {
                ProductionBuilding productionBuilding = (ProductionBuilding)building;
                ProductionBuildingUI.Instance.gameObject.SetActive(false);
                productionBuilding.GetProductionBuildingUIWorld().ShowAssignedHaulers(false);
            }

            if(building is Chest) {
                Chest chest = (Chest)building;

                chest.GetChestVisual().CloseChestVisual();
                chest.GetChestVisual().SetHovered(false);
                chest.GetChestUIWorld().ShowAssignedHaulers(false);
                chest.CloseInventory();
            }
        }
    }
}
