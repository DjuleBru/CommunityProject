using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class FetchFoodToEat : Action {

    public HumanoidNeeds humanoidNeeds;
    public Humanoid humanoid;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidNeeds = GetComponent<HumanoidNeeds>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoidNeeds.IdentifyBestFoodBuilding() == null) {
            return TaskStatus.Failure;
        }

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidNeeds.GetBestFoodSourceBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Fetching food to eat");

        if (colliderDistance2DToBuildingCollider.distance > .5f) {
            return TaskStatus.Running;
        }
        else {
            // Humanoid is close to destination building
            humanoidNeeds.FetchFoodInBuilding();
            return TaskStatus.Success;

        }

    }

}
