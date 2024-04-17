using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveToAssignedBuilding : Action {

    public Humanoid humanoid;
    public HumanoidWork humanoidWork;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidWork.GetAssignedBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToBuildingCollider.pointA);
        humanoid.SetHumanoidActionDescription("Heading to work");

        if (colliderDistance2DToBuildingCollider.distance < humanoidWork.GetRoamDistanceToBuilding()) {
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Running;
        }

    }

}
