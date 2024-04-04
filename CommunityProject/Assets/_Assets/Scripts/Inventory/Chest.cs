using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [ES3Serializable]
    private Inventory chestInventory;
    [SerializeField] private List<Item> itemsInChest = new List<Item>();
    [SerializeField] private InventoryUI_Interacted chestInventoryUI;

    private bool chestHasBeenFilled;

    private void Start() {
        chestInventory = new Inventory(false, 3, 3);
        chestHasBeenFilled = false;
    }

    public void AddItemsToChest(List<Item> itemList) {
        foreach (Item item in itemList) {
            itemsInChest.Add(item);
        }
    }

    private void Update() {
        if(!chestHasBeenFilled) {
            if (chestInventory != null) {
                chestInventory.AddItemList(itemsInChest);
            }
            chestHasBeenFilled = true;
        }
    }

    public void OpenInventory() {
        chestInventoryUI.SetInventory(chestInventory); 
        chestInventoryUI.OpenCloseInventoryPanel();
    }

    public void CloseInventory() {
        chestInventoryUI.CloseInventoryPanel();
    }

}
