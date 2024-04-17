using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamAroundAssignedBuilding : Action
{
    public HumanoidMovement humanoidMovement;
    public HumanoidWork humanoidWork;
    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoidWork.GetAssignedBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);

        humanoidMovement.Roam(colliderDistance2DToBuildingCollider.pointA, 4f);
        return TaskStatus.Success;

    }
}
