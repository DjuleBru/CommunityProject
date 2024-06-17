using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Sleep : Action {
    private HumanoidNeeds humanoidNeeds;

    public override void OnAwake() {
        humanoidNeeds = GetComponent<HumanoidNeeds>();
    }

    public override TaskStatus OnUpdate() {

        if (!humanoidNeeds.GetExhausted()) {
            return TaskStatus.Success;
        } else {
            if(!humanoidNeeds.GetSleeping()) {
                humanoidNeeds.SetSleeping(true);
            }
            return TaskStatus.Running;
        }
    }
}
