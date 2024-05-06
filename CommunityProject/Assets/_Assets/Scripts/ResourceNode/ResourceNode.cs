using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] protected Item itemStored;
    [SerializeField] protected int itemDropCountOnHit;
    [SerializeField] protected WeaponSO harvestingWeaponSO;
    [SerializeField] protected PlayerAnimatorManager.PlayerInteractBool interactBool;

    protected Inventory resourceNodeInventory;

    protected int initialItemNumber;
    public event EventHandler OnResourceNodeHit;
    public event EventHandler OnResourceNodeDepleted;

    protected void Awake() {
        resourceNodeInventory = new Inventory(false, 1, 1, 99999);
    }

    protected void Start() {
        resourceNodeInventory.AddItem(itemStored);
        initialItemNumber = (int)itemStored.amount;
    }

    public virtual void HitResourceNode() {
        OnResourceNodeHit?.Invoke(this, EventArgs.Empty);

        Item itemDropped = new Item { itemType = itemStored.itemType, amount = itemDropCountOnHit };

        if (resourceNodeInventory.HasItem(itemDropped)) {
            ItemWorld.DropItem(transform.position, itemDropped, 5f, false).SetAttractedToPlayerAfterDelay(1f);
            resourceNodeInventory.RemoveItemAmount(itemDropped);
        }

        if (!resourceNodeInventory.HasItem(itemDropped)) {
            OnResourceNodeDepleted?.Invoke(this, EventArgs.Empty);
        }
    }

    public PlayerAnimatorManager.PlayerInteractBool GetPlayerInteractBool() {
        return interactBool;
    }

    public WeaponSO GetHarvestingWeaponSO() {
        return harvestingWeaponSO;
    }

    public float GetHarvestingAmountNormalized() {
        return initialItemNumber / itemStored.amount;
    }

}
