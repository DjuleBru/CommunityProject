using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchItemsToCarry : Action {

    public Humanoid humanoid;
    public HumanoidCarry humanoidCarry;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidCarry = GetComponent<HumanoidCarry>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidCarry.GetSourceBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Fetching items");

        if (colliderDistance2DToBuildingCollider.distance > .5f) {
            return TaskStatus.Running;
        } else {
            if (humanoidCarry.FetchItemsInSourceBuilding()) {
                humanoidCarry.IdentifyBestDestinationBuilding();
                humanoidCarry.IdentifyBestSourceBuilding(humanoidCarry.GetItemToCarry());
                return TaskStatus.Success;
            } else {
                humanoidCarry.IdentifyBestDestinationBuilding();
                humanoidCarry.IdentifyBestSourceBuilding(humanoidCarry.GetItemToCarry());
                return TaskStatus.Failure;
            }
        }

    }

}