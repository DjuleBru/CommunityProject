using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType {
        Wood,
        Stone,
        HealthPotion
    }

    public enum ItemCategory {
        Material,
        Equipment,
        Consumable,
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
