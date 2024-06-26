using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignBestBuilding : Action
{
    public Humanoid humanoid;
    public HumanoidWork humanoidWork;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidWork = GetComponent<HumanoidWork>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoid.GetAutoAssign()) {
            humanoid.AssignBuilding(humanoidWork.FindBestWorkingBuilding());

            if (humanoid.GetAssignedBuilding() != null) {
                humanoid.GetAssignedBuilding().AssignHumanoid(humanoid);
                return TaskStatus.Success;
            }

        }
        else {
            if (humanoid.GetAssignedBuilding() != null) {
                return TaskStatus.Success;
            } else {
                return TaskStatus.Running;
            }
        }


        return TaskStatus.Failure;
    }
}
