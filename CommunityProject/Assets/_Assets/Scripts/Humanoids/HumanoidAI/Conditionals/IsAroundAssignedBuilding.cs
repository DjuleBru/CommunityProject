using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAroundAssignedBuilding : Conditional {

    public Humanoid humanoid;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;
    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoid.GetAssignedBuilding() == null) {
            return TaskStatus.Failure;
        }

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoid.GetAssignedBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        if (colliderDistance2DToBuildingCollider.distance < humanoid.GetRoamDistanceToBuilding()) {
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Failure;
        }

    }
}
