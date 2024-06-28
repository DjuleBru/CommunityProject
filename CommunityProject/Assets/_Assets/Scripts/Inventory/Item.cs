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
        CopperSword,
        WoodenHandle,
        WoodenBlade,
        CopperHelmet,
        CopperNecklace,
        CopperRing,
        CopperBoots,
        CopperBlade,
        CopperArmor,
        CopperOre,
        CopperBar,
        Charcoal,
        CopperShield,
        Chisel,
        Wheat,
        Flour,
        Bread,
        HayStack,
        monsterMeat,
        monterExtractT1,
        monsterExtractT2,
        monsterExtractT3,
        monsterExtractT4,
        monsterExtractT5,
        Sand,
        Glass,
        GlassVial,
        knowledgePotionT1,
        knowledgePotionT2,
        knowledgePotionT3,
        Poppy,
        PoppyPowder,
        WoodenSpear,
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
        ring,
    }

    public enum ItemEquipmentType {
        stick,
        sword,
        flail,
        spear,
        longsword,
        bow,
        hammer,
        axe,
        pickaxe,
        shield,
        brestplate,
        intelligenceRing,
        intelligenceNecklace,
        helmet,
        speedBoots,
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
