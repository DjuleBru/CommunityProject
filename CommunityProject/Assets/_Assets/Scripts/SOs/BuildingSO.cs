using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingSO : ScriptableObject
{
    public Building.BuildingUICategory buildingCategory;
    public Building.BuildingType buildingType;
    public Building.BuildingWorksCategory buildingWorksCategory;
    public List<Item> buildingCostItems;

    public List<RecipeSO> buildingRecipes;

    public string buildingName;
    public Sprite buildingIconSprite;
    public Sprite buildingDescriptionPanelSprite;
    public string buildingDescription;

    public GameObject buildingPrefab;


}
