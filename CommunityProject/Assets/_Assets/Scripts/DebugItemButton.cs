using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemButton : ItemSlot
{
    public void GiveItemToPlayer() {
        Item itemToAdd = new Item { itemType = item.itemType, amount = item.amount };
        Player.Instance.GetInventory().AddItem(itemToAdd);
    }
}
