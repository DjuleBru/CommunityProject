using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType {
        Wood,
        WoodPlanks,
        Stone,
        CutStone,
        Clay,
        Brick,
        WoodenShield,
        HealthPotion,
        Potato,
        WoodenArmor,
        WoodenBoots,
        WoodenHelmet,
        WoodenNecklace,
        WoodenRing,
        WoodenSword,
    }

    public enum ItemCategory {
        All,
        Material,
        Equipment,
        Consumable,
        Food,
    }

    public enum ItemEquipmentCategory {
        main,
        secondary,
        head,
        boots,
        necklace,
        ring
    }

    public enum ItemTier {
        Tier1,
        Tier2,
        Tier3,
    }

    public ItemType itemType;
    public ItemTier itemTier;
    public int amount;
}
