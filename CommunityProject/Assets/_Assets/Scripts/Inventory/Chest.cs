using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [ES3Serializable]
    private Inventory chestInventory;
    [SerializeField] private List<Item> itemsInChest = new List<Item>();

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
        InventoryUI_Interacted.Instance.SetInventory(chestInventory);
        InventoryUI_Interacted.Instance.gameObject.SetActive(true);
        InventoryUI_Interacted.Instance.RefreshInventorySize();
    }

    public void CloseInventory() {
        InventoryUI_Interacted.Instance.gameObject.SetActive(false);
    }

}
