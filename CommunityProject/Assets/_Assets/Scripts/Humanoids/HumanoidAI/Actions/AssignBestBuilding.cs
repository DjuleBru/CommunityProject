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
        humanoidWork.AssignBuilding(humanoidWork.FindBestWorkingBuilding());
        if(humanoidWork.GetAssignedBuilding() != null) {
            humanoidWork.GetAssignedBuilding().AssignHumanoid(humanoid);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
