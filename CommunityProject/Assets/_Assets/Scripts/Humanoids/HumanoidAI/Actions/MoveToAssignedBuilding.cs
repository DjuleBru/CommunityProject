using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveToAssignedBuilding : Action {

    public Humanoid humanoid;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoid.GetAssignedBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Heading to work");

        if (colliderDistance2DToBuildingCollider.distance < humanoid.GetRoamDistanceToBuilding()) {
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Running;
        }

    }

}
