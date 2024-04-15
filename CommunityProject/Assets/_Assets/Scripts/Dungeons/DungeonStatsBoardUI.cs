using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonStatsBoardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dungeonTimeText;
    [SerializeField] private InventoryUI dungeonLootedItemsInventoryUI;
    [SerializeField] private List<Item> dungeonLootItemList = new List<Item>();

    private Inventory dungeonLootedItemsInventory;
    private bool inventoryHasBeenFilled;

    public void SetDungeonLootUI(List<Item> itemList) {
        dungeonLootedItemsInventory = new Inventory(false, 3, 3, false, null);
        dungeonLootItemList = itemList;
        inventoryHasBeenFilled = false;
    }

    private void Update() {
        if (!inventoryHasBeenFilled) {
            if (dungeonLootedItemsInventory != null) {
                dungeonLootedItemsInventoryUI.SetInventory(dungeonLootedItemsInventory);
                dungeonLootedItemsInventory.AddItemList(dungeonLootItemList);
                inventoryHasBeenFilled = true;
            }
        }
    }

    public void SetDungeonTimeUI(float time) {
        dungeonTimeText.text = ((int)time).ToString() + "s";
    }
}
