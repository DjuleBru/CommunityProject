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
    public Item.ItemCategory itemCategory;
    [BoxGroup("Basic Info")]
    public Item.ItemTier itemTier;
    [BoxGroup("Basic Info")]
    public Sprite itemSprite;
    [BoxGroup("Basic Info")]
    public bool isStackable;
    [BoxGroup("Basic Info")]
    public string itemDescription;

    [ShowIf("isStackable")]
    [BoxGroup("Basic Info")]
    public int maxStackableAmount;


    [BoxGroup("Crops")]
    public Sprite seedSprite;
    [BoxGroup("Crops")]
    public Sprite growth1Sprite;
    [BoxGroup("Crops")]
    public Sprite growth2Sprite;
    [BoxGroup("Crops")]
    public Sprite growth3Sprite;

    [BoxGroup("Food")]
    public int foodValue;


    [BoxGroup("Equipment")]
    public Item.ItemEquipmentCategory itemEquipmentCategory;
    [BoxGroup("Equipment")]
    public Item.ItemEquipmentType itemEquipmentType;
    [BoxGroup("Equipment")]
    public int strengthBonusValue;
    [BoxGroup("Equipment")]
    public int intelligenceBonusValue;
    [BoxGroup("Equipment")]
    public int agilityBonusValue;
    [BoxGroup("Equipment")]
    public int moveSpeedBonusValue;
    [BoxGroup("Equipment")]
    public int healthBonusValue;
    [BoxGroup("Equipment")]
    public int damageBonusValue;
    [BoxGroup("Equipment")]
    public float attackSpeedMultiplier;
    [BoxGroup("Equipment")]
    public int armorBonusValue;
    [BoxGroup("Equipment")]
    public int equipmentDurability;
    [BoxGroup("Equipment")]
    public WeaponSO weaponSO;
}
