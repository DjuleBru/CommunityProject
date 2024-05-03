using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchItemsToCarry : Action {

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

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidHaul.GetSourceBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Fetching items");

        if (colliderDistance2DToBuildingCollider.distance > .5f) {
            return TaskStatus.Running;
        } else {
            // Humanoid is close to destination building

            if (humanoidHaul.FetchItemsInBuilding(humanoidHaul.GetSourceBuilding())) {

                if (humanoid.GetAutoAssign()) {
                    humanoidHaul.TryAssignBestDestinationBuilding();
                    humanoidHaul.IdentifyBestSourceBuilding(humanoidHaul.GetItemToCarry());
                }

                return TaskStatus.Success;
            } else {

                if (humanoid.GetAutoAssign()) {
                    humanoidHaul.TryAssignBestDestinationBuilding();
                    humanoidHaul.IdentifyBestSourceBuilding(humanoidHaul.GetItemToCarry());
                }

                return TaskStatus.Failure;
            }


        }

    }

}