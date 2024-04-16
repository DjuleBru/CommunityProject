using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBuildingAssigned : Conditional {

    public HumanoidWork humanoidWork;

    public override void OnAwake() {
        humanoidWork = GetComponent<HumanoidWork>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidWork.GetAssignedBuilding() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
