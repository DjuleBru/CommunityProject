using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringItemsToDestination : Action {

    public Humanoid humanoid;
    public HumanoidHaul humanoidHaul;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidHaul = GetComponent<HumanoidHaul>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoidHaul.GetDestinationBuilding() == null || humanoidHaul.GetSourceBuilding() == null) {
            return TaskStatus.Failure;
        }

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidHaul.GetDestinationBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Carrying items");

        if (colliderDistance2DToBuildingCollider.distance > .75f) {
            return TaskStatus.Running;
        }
        else {
            if (humanoidHaul.DropItemsInBuilding(humanoidHaul.GetDestinationBuilding())) {
                if(humanoid.GetAutoAssign()) {
                    humanoidHaul.TryAssignBestDestinationBuilding();
                    humanoidHaul.IdentifyBestSourceBuilding(humanoidHaul.GetItemToCarry());
                }

                return TaskStatus.Success;
            } else {
                return TaskStatus.Failure;
            };

        }

    }
}
