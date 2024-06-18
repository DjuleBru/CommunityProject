using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using System.Runtime.CompilerServices;

public class FetchMissingEquipment : Action {

    private Humanoid humanoid;
    public HumanoidMovement humanoidMovement;
    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        bool allEquipmentsFetched = true;

        if (humanoid.GetMainHandItem() != null) {

            if(humanoid.GetMainHandItem().amount == 0) {
                allEquipmentsFetched = false;
                List<Chest> chestList = BuildingsManager.Instance.GetBuildingStoringEquipment(humanoid.GetMainHandItem());

                Chest closestChest = FindClosestChest(chestList);

                if(closestChest != null) {
                    ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                    humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                    humanoid.SetHumanoidActionDescription("Fetching equipment");

                    if (colliderDistance2DToChest.distance < .5f) {
                        Item itemToFetch = new Item { itemType = humanoid.GetMainHandItem().itemType, amount = 1 };

                        closestChest.GetChestInventory().RemoveItemAmount(itemToFetch);
                        humanoid.SetMainHandItem(itemToFetch);

                    }
                    else {
                        return TaskStatus.Running;
                    }
                }
            }
        }

        if (humanoid.GetSecondaryHandItem() != null) {

            if (humanoid.GetSecondaryHandItem().amount == 0) {
                allEquipmentsFetched = false;
                List<Chest> chestList = BuildingsManager.Instance.GetBuildingStoringEquipment(humanoid.GetSecondaryHandItem());

                Chest closestChest = FindClosestChest(chestList);

                if (closestChest != null) {
                    ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                    humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                    humanoid.SetHumanoidActionDescription("Fetching equipment");

                    if (colliderDistance2DToChest.distance < .5f) {
                        Item itemToFetch = new Item { itemType = humanoid.GetSecondaryHandItem().itemType, amount = 1 };

                        closestChest.GetChestInventory().RemoveItemAmount(itemToFetch);
                        humanoid.SetSecondaryHandItem(itemToFetch);

                    }
                    else {
                        return TaskStatus.Running;
                    }
                }
            }
        }

        if (humanoid.GetHeadItem() != null) {

            if (humanoid.GetHeadItem().amount == 0) {
                allEquipmentsFetched = false;
                List<Chest> chestList = BuildingsManager.Instance.GetBuildingStoringEquipment(humanoid.GetHeadItem());

                Chest closestChest = FindClosestChest(chestList);

                if (closestChest != null) {
                    ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                    humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                    humanoid.SetHumanoidActionDescription("Fetching equipment");

                    if (colliderDistance2DToChest.distance < .5f) {
                        Item itemToFetch = new Item { itemType = humanoid.GetHeadItem().itemType, amount = 1 };

                        closestChest.GetChestInventory().RemoveItemAmount(itemToFetch);
                        humanoid.SetHeadItem(itemToFetch);

                    }
                    else {
                        return TaskStatus.Running;
                    }
                }
            }
        }

        if (humanoid.GetBootsItem() != null) {

            if (humanoid.GetBootsItem().amount == 0) {
                allEquipmentsFetched = false;
                List<Chest> chestList = BuildingsManager.Instance.GetBuildingStoringEquipment(humanoid.GetBootsItem());

                Chest closestChest = FindClosestChest(chestList);

                if (closestChest != null) {
                    ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                    humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                    humanoid.SetHumanoidActionDescription("Fetching equipment");

                    if (colliderDistance2DToChest.distance < .5f) {
                        Item itemToFetch = new Item { itemType = humanoid.GetBootsItem().itemType, amount = 1 };

                        closestChest.GetChestInventory().RemoveItemAmount(itemToFetch);
                        humanoid.SetBootsItem(itemToFetch);

                    }
                    else {
                        return TaskStatus.Running;
                    }
                }
            }
        }

        if (humanoid.GetNecklaceItem() != null) {

            if (humanoid.GetNecklaceItem().amount == 0) {
                allEquipmentsFetched = false;
                List<Chest> chestList = BuildingsManager.Instance.GetBuildingStoringEquipment(humanoid.GetNecklaceItem());

                Chest closestChest = FindClosestChest(chestList);

                if (closestChest != null) {
                    ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                    humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                    humanoid.SetHumanoidActionDescription("Fetching equipment");

                    if (colliderDistance2DToChest.distance < .5f) {
                        Item itemToFetch = new Item { itemType = humanoid.GetNecklaceItem().itemType, amount = 1 };

                        closestChest.GetChestInventory().RemoveItemAmount(itemToFetch);
                        humanoid.SetNecklaceItem(itemToFetch);

                    }
                    else {
                        return TaskStatus.Running;
                    }
                }
            }
        }

        if (humanoid.GetRingItem() != null) {

            if (humanoid.GetRingItem().amount == 0) {
                allEquipmentsFetched = false;
                List<Chest> chestList = BuildingsManager.Instance.GetBuildingStoringEquipment(humanoid.GetRingItem());

                Chest closestChest = FindClosestChest(chestList);

                if (closestChest != null) {
                    ColliderDistance2D colliderDistance2DToChest = closestChest.GetComponent<Collider2D>().Distance(humanoidCollider);
                    humanoidMovement.MoveToDestination(colliderDistance2DToChest.pointA);
                    humanoid.SetHumanoidActionDescription("Fetching equipment");

                    if (colliderDistance2DToChest.distance < .5f) {
                        Item itemToFetch = new Item { itemType = humanoid.GetRingItem().itemType, amount = 1 };

                        closestChest.GetChestInventory().RemoveItemAmount(itemToFetch);
                        humanoid.SetRingItem(itemToFetch);

                    }
                    else {
                        return TaskStatus.Running;
                    }
                }
            }
        }

        if (allEquipmentsFetched) {
            return TaskStatus.Success;
        } else {
            return TaskStatus.Failure;
        }

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
