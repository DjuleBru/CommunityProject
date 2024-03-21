using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    [BoxGroup("Basic Info")]
    public Item.ItemType itemType;
    [BoxGroup("Basic Info")]
    public Item.ItemType itemCategory;
    [BoxGroup("Basic Info")]
    public Item.ItemTier itemTier;
    [BoxGroup("Basic Info")]
    public Sprite itemSprite;
    [BoxGroup("Basic Info")]
    public bool isStackable;

    [ShowIf("isStackable")]
    [BoxGroup("Basic Info")]
    public int maxStackable;
}
