using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCarryingItems : Conditional {

    public HumanoidCarry humanoidCarry;

    public override void OnAwake() {
        humanoidCarry = GetComponent<HumanoidCarry>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidCarry.IsCarryingItem()) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}

