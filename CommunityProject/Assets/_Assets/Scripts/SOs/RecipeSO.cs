using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite recipeSprite;

    public List<Item> inputItems;
    public List<Item> outputItems;
    public List<Item> itemsToUnlockList;

    public float standardProductionTime;
}
