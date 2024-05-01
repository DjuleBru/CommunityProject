using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamAroundAssignedBuilding : Action
{
    public HumanoidMovement humanoidMovement;
    public Humanoid humanoid;
    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        ColliderDistance2D colliderDistance2DToBuildingCollider = humanoid.GetAssignedBuilding().GetComponent<Collider2D>().Distance(humanoidCollider);
        
        humanoidMovement.Roam(colliderDistance2DToBuildingCollider.pointA, 4f);
        return TaskStatus.Success;

    }
}
