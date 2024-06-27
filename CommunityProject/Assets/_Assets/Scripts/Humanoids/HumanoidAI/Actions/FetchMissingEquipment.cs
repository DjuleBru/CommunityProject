using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using System.Runtime.CompilerServices;

public class FetchMissingEquipment : Action {

    private Humanoid humanoid;
    public HumanoidMovement humanoidMovement;
    public Collider2D humanoidCollider;

    bool allEquipmentsFetched;
    Item itemToFetch;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {
        allEquipmentsFetched = true;

        if (humanoid.GetMainHandItem() != null) {
            Item.ItemEquipmentCategory equipmentCategory = Item.ItemEquipmentCategory.main;
            if (humanoid.GetEquipmentItem(equipmentCategory).amount == 0) {
                FetchEquipment(equipmentCategory);
            }
        }

        if (humanoid.GetSecondaryHandItem() != null) {
            Item.ItemEquipmentCategory equipmentCategory = Item.ItemEquipmentCategory.secondary;
            if (humanoid.GetEquipmentItem(equipmentCategory).amount == 0) {
                FetchEquipment(equipmentCategory);
            }
        }

        if (humanoid.GetHeadItem() != null) {
            Item.ItemEquipmentCategory equipmentCategory = Item.ItemEquipmentCategory.head;
            if (humanoid.GetEquipmentItem(equipmentCategory).amount == 0) {
                FetchEquipment(equipmentCategory);
            }
        }

        if (humanoid.GetBootsItem() != null) {
            Item.ItemEquipmentCategory equipmentCategory = Item.ItemEquipmentCategory.boots;
            if (humanoid.GetEquipmentItem(equipmentCategory).amount == 0) {
                FetchEquipment(equipmentCategory);
            }
        }

        if (humanoid.GetNecklaceItem() != null) {
            Item.ItemEquipmentCategory equipmentCategory = Item.ItemEquipmentCategory.necklace;
            if (humanoid.GetEquipmentItem(equipmentCategory).amount == 0) {
                FetchEquipment(equipmentCategory);
            }
        }

        if (humanoid.GetRingItem() != null) {
            Item.ItemEquipmentCategory equipmentCategory = Item.ItemEquipmentCategory.ring;
            if (humanoid.GetEquipmentItem(equipmentCategory).amount == 0) {
                FetchEquipment(equipmentCategory);
            }
        }

        if (allEquipmentsFetched) {
            return TaskStatus.Success;
        } else {
            return TaskStatus.Failure;
        }

    }

    private TaskStatus FetchEquipment(Item.ItemEquipmentCategory equipmentCategory) {

            allEquipmentsFetched = false;

            if(itemToFetch == null) {
                itemToFetch = humanoid.GetEquipmentItem(equipmentCategory);
                if (humanoid.GetAutoAssignBestEquipment()) {
                    itemToFetch = BuildingsManager.Instance.GetBestItemTierAvailable(itemToFetch);
                }
            }

            List<Chest> chestList = BuildingsManager.Instance.GetChestStoringEquipment(itemToFetch);

            Chest closestChest = FindClosestChest(chestList);
            if(closestChest != null) {

                ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                humanoid.SetHumanoidActionDescription("Fetching equipment");

                if (colliderDistance2DToChest.distance < .5f) {

                int amountThatCanBeFetched = 0;

                if(closestChest.GetChestInventory().AmountInventoryHasOfType(itemToFetch) >= 5) {
                    amountThatCanBeFetched = 5;
                } else {
                    amountThatCanBeFetched = closestChest.GetChestInventory().AmountInventoryHasOfType(itemToFetch);
                }

                Debug.Log(amountThatCanBeFetched);
                Item itemToFetchWithAmount = new Item { itemType = itemToFetch.itemType, amount = amountThatCanBeFetched };

                closestChest.GetChestInventory().RemoveItemAmount(itemToFetchWithAmount);
                humanoid.SetEquipmentType(itemToFetchWithAmount);
                itemToFetch = null;
                return TaskStatus.Success;
                }
                else {
                    return TaskStatus.Running;
                }
        }
        itemToFetch = null;
        return TaskStatus.Failure;
    }

    private Chest FindClosestChest(List<Chest> chestList) {
        Chest closestChest = null;
        float distanceToClosestChest = Mathf.Infinity;
        foreach (Chest chest in chestList) {
            float distanceToChest = Vector3.Distance(transform.position, chest.transform.position);
                if (distanceToChest < distanceToClosestChest) {
                closestChest = chest;
                distanceToClosestChest = distanceToChest;
            }
        }
        return closestChest;
    }
}
