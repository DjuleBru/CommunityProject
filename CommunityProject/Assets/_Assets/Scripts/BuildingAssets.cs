using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAssets : MonoBehaviour
{
    public static BuildingAssets Instance { get; private set; }

    [SerializeField] private List<BuildingSO> buildingSOList;


    private void Awake() {
        Instance = this;
    }

    public BuildingSO GetBuildingSO(Building.BuildingType buildingType) {
        foreach (BuildingSO buildingSO in buildingSOList) {
            if (buildingSO.buildingType == buildingType) {
                return buildingSO;
            }
        }
        return null;
    }

    public List<BuildingSO> GetBuildingSOsInUICategory(Building.BuildingUICategory buildingCategory) {
        List<BuildingSO> buildingSOListInCategory = new List<BuildingSO>();
        foreach(BuildingSO buildingSO in buildingSOList) {
            if(buildingSO.buildingUICategory  == buildingCategory) {
                buildingSOListInCategory.Add(buildingSO);
            }
        }

        return buildingSOListInCategory;
    }

    public List<BuildingSO> GetBuildingSOsInCategory(Building.BuildingUICategory buildingUICategory, Building.BuildingCategory buildingCategory) {
        List<BuildingSO> buildingSOListInCategory = new List<BuildingSO>();

        foreach (BuildingSO buildingSO in buildingSOList) {
            if (buildingSO.buildingUICategory == buildingUICategory) {
                if(buildingSO.buildingCategory == buildingCategory) {
                    buildingSOListInCategory.Add(buildingSO);
                }
            }
        }

        return buildingSOListInCategory;
    }

    public List<BuildingSO> GetBuildingSOList() {
        return buildingSOList;
    }

}
