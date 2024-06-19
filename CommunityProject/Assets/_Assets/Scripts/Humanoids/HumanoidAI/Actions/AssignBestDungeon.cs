using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class AssignBestDungeon : Action {
    public Humanoid humanoid;
    public HumanoidDungeonCrawl humanoidDungeonCrawl;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidDungeonCrawl = GetComponent<HumanoidDungeonCrawl>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoid.GetAutoAssign()) {

            humanoidDungeonCrawl.AssignDungeonEntrance(humanoidDungeonCrawl.FindBestDungeonEntrance());

            if (humanoidDungeonCrawl.GetDungeonEntranceAssigned() != null) {
                humanoidDungeonCrawl.GetDungeonEntranceAssigned().AssignHumanoid(humanoid);
                return TaskStatus.Success;
            }

        }
        else {
            if (humanoidDungeonCrawl.GetDungeonEntranceAssigned() != null) {
                return TaskStatus.Success;
            }
            else {
                return TaskStatus.Failure;
            }
        }


        return TaskStatus.Failure;
    }
}