using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class BringItemsToDungeonChest : Action {
    public Humanoid humanoid;
    public HumanoidCarry humanoidCarry;
    public HumanoidDungeonCrawl humanoidDungeonCrawl;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidDungeonCrawl = GetComponent<HumanoidDungeonCrawl>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCarry = GetComponent<HumanoidCarry>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoidDungeonCrawl.GetDungeonEntranceAssigned() == null) {
            return TaskStatus.Failure;
        }

        Chest dungeonChest = humanoidDungeonCrawl.GetDungeonEntranceAssigned().GetDungeonChest();
        ColliderDistance2D colliderDistance2DToBuildingCollider = dungeonChest.GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Carrying items");

        if (colliderDistance2DToBuildingCollider.distance > .75f) {
            return TaskStatus.Running;
        }

        else {
            if (humanoidCarry.DropItemListCarryingInChest(dungeonChest)) {
                return TaskStatus.Success;
            }
            else {
                return TaskStatus.Failure;
            };

        }

    }
}