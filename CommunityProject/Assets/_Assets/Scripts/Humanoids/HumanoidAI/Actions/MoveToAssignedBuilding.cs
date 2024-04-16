using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveToAssignedBuilding : Action {

    public HumanoidWork humanoidWork;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;
    public override void OnAwake() {
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidWork.GetAssignedBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);
        humanoidMovement.CalculatePath(colliderDistance2DToBuildingCollider.pointB, colliderDistance2DToBuildingCollider.pointA);

        if (colliderDistance2DToBuildingCollider.distance < 1.5f) {
           return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

}
