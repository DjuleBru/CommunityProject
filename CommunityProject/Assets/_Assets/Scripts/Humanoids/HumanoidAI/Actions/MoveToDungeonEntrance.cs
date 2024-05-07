using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDungeonEntrance : Action {

    public Humanoid humanoid;
    public HumanoidMovement humanoidMovement;
    public HumanoidVisual humanoidVisual;

    public Vector3 dungeonEntrance;
    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidVisual = humanoid.GetHumanoidVisual();
        dungeonEntrance = DungeonGenerationManager.Instance.GetDungeonRoomList()[0].GetRoomEnterPosition();
    }

    public override TaskStatus OnUpdate() {

        humanoidMovement.MoveToDestination(dungeonEntrance);
        if (Vector2.Distance(transform.position, dungeonEntrance) < 2.5f) {
            humanoidVisual.HideVisual();
            return TaskStatus.Success;
        }
        else {
            return TaskStatus.Running;
        }

    }

}