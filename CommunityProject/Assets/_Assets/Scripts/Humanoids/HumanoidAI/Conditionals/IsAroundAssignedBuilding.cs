using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAroundAssignedBuilding : Conditional {

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

        if (colliderDistance2DToBuildingCollider.distance < humanoidWork.GetRoamDistanceToBuilding()) {
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Failure;
        }

    }
}
