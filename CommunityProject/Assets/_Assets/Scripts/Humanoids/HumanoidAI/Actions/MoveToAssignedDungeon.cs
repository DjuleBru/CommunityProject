using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToAssignedDungeon : Action {

    public Humanoid humanoid;
    public HumanoidDungeonCrawl humanoidCrawl;
    public HumanoidMovement humanoidMovement;

    public Collider2D humanoidCollider;

    public override void OnAwake() {
        humanoidCrawl = GetComponent<HumanoidDungeonCrawl>();
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCollider = GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoidCrawl.GetDungeonEntranceAssigned() == null) {
            return TaskStatus.Failure;
        }

        ColliderDistance2D colliderDistance2DToDungeonEntranceCollider = humanoidCrawl.GetDungeonEntranceAssigned().GetColliderForDungeoneers().Distance(humanoidCollider);

        humanoidMovement.MoveToDestination(colliderDistance2DToDungeonEntranceCollider.pointA);
        humanoid.SetHumanoidActionDescription("Heading to dungeon Entrance");
        if (colliderDistance2DToDungeonEntranceCollider.distance < .5f) {
            humanoidCrawl.StartCrawling();
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Running;
        }

    }

}