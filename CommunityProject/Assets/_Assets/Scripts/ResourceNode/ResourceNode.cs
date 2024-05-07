using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour {

    [SerializeField] private Item.ItemType itemTypeStored;
    [SerializeField] private int initialItemNumber;

    private Item itemStored;
    [SerializeField] private int itemDropCountOnHit;
    [SerializeField] private WeaponSO harvestingWeaponSO;
    [SerializeField] private PlayerAnimatorManager.PlayerInteractBool interactBool;

    private Inventory resourceNodeInventory;

    public event EventHandler OnResourceNodeHit;
    public event EventHandler OnResourceNodeDepleted;

    protected void Awake() {
        resourceNodeInventory = new Inventory(false, 1, 1, 99999);
    }

    protected void Start() {
        itemStored = new Item { itemType = itemTypeStored, amount = initialItemNumber };
        resourceNodeInventory.AddItem(itemStored);
    }

    public virtual void HitResourceNode() {
        Item itemDropped = new Item { itemType = itemStored.itemType, amount = itemDropCountOnHit };

        if (resourceNodeInventory.HasItem(itemDropped)) {
            ItemWorld.DropItem(transform.position, itemDropped, 5f, false).SetAttractedToPlayerAfterDelay(1f);
            resourceNodeInventory.RemoveItemAmount(itemDropped);
        }

        if (!resourceNodeInventory.HasItem(itemDropped)) {
            OnResourceNodeDepleted?.Invoke(this, EventArgs.Empty);
        }

        OnResourceNodeHit?.Invoke(this, EventArgs.Empty);
    }

    public PlayerAnimatorManager.PlayerInteractBool GetPlayerInteractBool() {
        return interactBool;
    }

    public WeaponSO GetHarvestingWeaponSO() {
        return harvestingWeaponSO;
    }

    public float GetHarvestingAmountNormalized() {
        return (float)itemStored.amount / (float)initialItemNumber;
    }

    protected void InvokeResourceNodeDepleted() {
        OnResourceNodeDepleted.Invoke(this, EventArgs.Empty);
    }

}
