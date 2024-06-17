using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToAssignedHousing : Action {

    private HumanoidNeeds humanoidNeeds;
    private Humanoid humanoid;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidNeeds = GetComponent<HumanoidNeeds>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }
    public override TaskStatus OnUpdate() {

        if (humanoidNeeds.GetAssignedHousing() == null) {
            return TaskStatus.Success;
        }

        ColliderDistance2D colliderDistance2DToHouse = humanoidNeeds.GetAssignedHousing().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToHouse.pointA);
        humanoid.SetHumanoidActionDescription("Heading to sleep to house");

        if (colliderDistance2DToHouse.distance < .5f) {
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Running;
        }

    }

}
