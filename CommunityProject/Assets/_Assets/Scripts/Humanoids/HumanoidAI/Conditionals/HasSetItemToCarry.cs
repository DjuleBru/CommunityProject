using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HasSetItemToCarry : Conditional {

    public HumanoidCarry humanoidCarry;

    public override void OnAwake() {
        humanoidCarry = GetComponent<HumanoidCarry>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidCarry.GetItemToCarry() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
