using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class FindHealthRegeneration : Action {

    private Humanoid humanoid;
    private HumanoidNeeds humanoidNeeds;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidNeeds = GetComponent<HumanoidNeeds>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }
    public override TaskStatus OnUpdate() {

        if (humanoid.GetHealthNormalized() >= 1f) {
            humanoid.StopHealing();
            return TaskStatus.Success;
        }

        if(humanoidNeeds.GetAssignedHousing() != null) {
            ColliderDistance2D colliderDistance2DToHealthRegen = humanoidNeeds.GetAssignedHousing().GetComponent<Collider2D>().Distance(humanoidCollider);

            humanoidMovement.MoveToDestination(colliderDistance2DToHealthRegen.pointA);
            humanoid.SetHumanoidActionDescription("Heading to heal");

            if (colliderDistance2DToHealthRegen.distance < .5f) {
                humanoid.Heal();
            }
        } else {
            humanoid.Heal();
        }

        return TaskStatus.Running;
    }

}
