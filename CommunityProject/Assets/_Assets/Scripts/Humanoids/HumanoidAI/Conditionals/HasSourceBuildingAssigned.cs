using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasSourceBuildingAssigned : Conditional {

    public HumanoidHaul humanoidCarry;

    public override void OnAwake() {
        humanoidCarry = GetComponent<HumanoidHaul>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidCarry.GetSourceBuilding() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
