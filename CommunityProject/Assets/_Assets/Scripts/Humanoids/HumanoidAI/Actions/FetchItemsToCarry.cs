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

        if (humanoidCarry.GetDestinationBuilding() == null || humanoidCarry.GetSourceBuilding() == null) {
            return TaskStatus.Failure;
        }

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidCarry.GetSourceBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Fetching items");

        if (colliderDistance2DToBuildingCollider.distance > .5f) {
            return TaskStatus.Running;
        } else {
            // Humanoid is close to destination building

            if (humanoidCarry.FetchItemsInSourceBuilding()) {

                Debug.Log("fetched items success!");

                if (humanoid.GetAutoAssign()) {
                    humanoidCarry.TryAssignBestDestinationBuilding();
                    humanoidCarry.IdentifyBestSourceBuilding(humanoidCarry.GetItemToCarry());
                }

                return TaskStatus.Success;
            } else {

                Debug.Log("fetched items fails!"); 

                if (humanoid.GetAutoAssign()) {
                    humanoidCarry.TryAssignBestDestinationBuilding();
                    humanoidCarry.IdentifyBestSourceBuilding(humanoidCarry.GetItemToCarry());
                }

                return TaskStatus.Failure;
            }


        }

    }

}