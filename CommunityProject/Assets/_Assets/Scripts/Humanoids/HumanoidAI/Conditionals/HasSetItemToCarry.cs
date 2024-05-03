using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HasSetItemToCarry : Conditional {

    public HumanoidHaul humanoidCarry;

    public override void OnAwake() {
        humanoidCarry = GetComponent<HumanoidHaul>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidCarry.GetItemToCarry() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
