using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingSO : ScriptableObject
{
    public Building.BuildingUICategory buildingUICategory;
    public Building.BuildingType buildingType;
    public Building.BuildingCategory buildingCategory;
    public List<Item> buildingCostItems;
    public List<Item> itemsToUnlockList;
    public bool canInteractWithBuilding;

    [BoxGroup("ProductionBuilding")]
    public List<RecipeSO> buildingRecipes;
    [BoxGroup("ProductionBuilding")]
    public Humanoid.Stat statAffectingProductivity;

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
