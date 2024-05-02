using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringItemsToDestination : Action {

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

        if (humanoidCarry.GetDestinationBuilding() == null || humanoidCarry.GetSourceBuilding() == null) {
            return TaskStatus.Failure;
        }

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidCarry.GetDestinationBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Carrying items");

        if (colliderDistance2DToBuildingCollider.distance > .75f) {
            return TaskStatus.Running;
        }
        else {
            if(humanoidCarry.DropItemsInDestinationBuilding()) {
                humanoidCarry.TryAssignBestDestinationBuilding();
                humanoidCarry.IdentifyBestSourceBuilding(humanoidCarry.GetItemToCarry());
                return TaskStatus.Success;
            } else {
                return TaskStatus.Failure;
            };

        }

    }
}
