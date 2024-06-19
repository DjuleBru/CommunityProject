using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingSO : ScriptableObject
{
    public Building.BuildingUICategory buildingCategory;
    public Building.BuildingType buildingType;
    public Building.BuildingWorksCategory buildingWorksCategory;
    public Humanoid.Stat statAffectingProductivity;
    public List<Item> buildingCostItems;

    public bool canInteractWithBuilding;

    [BoxGroup("ProductionBuilding")]
    public List<RecipeSO> buildingRecipes;

    [BoxGroup("Housing")]
    public int housingCapacity;
    [BoxGroup("Housing")]
    public int energyFillRateMultiplier;
    [BoxGroup("Housing")]
    public int healRate;

    public string buildingName;
    public Sprite buildingIconSprite;
    public Sprite buildingDescriptionPanelSprite;
    public string buildingDescription;

    public GameObject buildingPrefab;

}
