using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInAssignedBuilding : Action
{

    public HumanoidWork humanoidWork;
    public HumanoidMovement humanoidMovement;
    public override void OnAwake() {
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidMovement = GetComponent<HumanoidMovement>();
    }

    public override TaskStatus OnUpdate() {
        humanoidWork.Work();
        if(!humanoidWork.GetWorking()) {
            return TaskStatus.Failure;
        }
        return TaskStatus.Running;
    }

}
