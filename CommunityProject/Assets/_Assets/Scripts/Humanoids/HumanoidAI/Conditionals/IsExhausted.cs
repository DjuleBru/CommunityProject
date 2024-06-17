using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsExhausted : Conditional {
    private HumanoidNeeds humanoidNeeds;

    public override void OnAwake() {
        humanoidNeeds = GetComponent<HumanoidNeeds>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidNeeds.GetExhausted()) {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
