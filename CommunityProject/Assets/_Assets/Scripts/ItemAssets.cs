using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour {
    public static ItemAssets Instance { get; private set; }

    public Transform itemWorldPrefab;
    [SerializeField] private List<ItemSO> itemSOList;
    [SerializeField] private int maxItemTier;

    private void Awake() {
        Instance = this;
    }

    public ItemSO GetItemSO(Item.ItemType itemType) {
        foreach (ItemSO itemSO in itemSOList) {
            if (itemSO.itemType == itemType) {
                return itemSO;
            }
        }
        return null;
    }

    public Color GetItemColor(Item.ItemType itemType) {
        Item.ItemTier itemTier = GetItemSO(itemType).itemTier;

        switch(itemTier) {
            default:
            case Item.ItemTier.Tier1: return new Color(1, 0, 0);
            case Item.ItemTier.Tier2: return new Color(0, 1, 0);
            case Item.ItemTier.Tier3: return new Color(0, 0, 1);
        }
    }

    public List<ItemSO> GetItemSOList() {
        return itemSOList;
    }

    public List<Item> GetItemListOfCategory(Item.ItemCategory itemCategory) {
        List<Item> itemsRestricted = new List<Item>();

        foreach (ItemSO itemSO in itemSOList) {
            if(itemSO.itemCategory == itemCategory) {
                itemsRestricted.Add(new Item { itemType = itemSO.itemType, amount = 0 });
            }
        }

        return itemsRestricted;
    }

    public int GetMaxItemTier() {
        return maxItemTier;
    }
}
