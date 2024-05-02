using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkInAssignedBuilding : Action
{
    public Humanoid humanoid;
    public HumanoidWork humanoidWork;
    public HumanoidMovement humanoidMovement;
    public override void OnAwake() {
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {

        humanoidWork.Work();
        if(!humanoidWork.GetWorking()) {
            return TaskStatus.Failure;
        }

        if(humanoid.GetAssignedBuilding() != null) {
            return TaskStatus.Running;
        } else {
            return TaskStatus.Failure;
        }
    }

}
