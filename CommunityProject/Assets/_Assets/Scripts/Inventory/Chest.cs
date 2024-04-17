using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Building
{
    [ES3Serializable]
    private Inventory chestInventory;
    [SerializeField] private List<Item> itemsInChest = new List<Item>();
    [SerializeField] private InventoryUI_Interactable chestInventoryUI;

    private bool chestHasBeenFilled;

    protected override void Start() {
        base.Start();
        chestInventory = new Inventory(false, 3, 3, false, null);
        chestHasBeenFilled = false;

        BuildingsManager.Instance.AddBuilding(this);
    }

    public void AddItemsToChest(List<Item> itemList) {
        foreach (Item item in itemList) {
            itemsInChest.Add(item);
        }
    }

    protected override void Update() {
        base.Update();
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

    public Inventory GetChestInventory() {
        return chestInventory;
    }

}
