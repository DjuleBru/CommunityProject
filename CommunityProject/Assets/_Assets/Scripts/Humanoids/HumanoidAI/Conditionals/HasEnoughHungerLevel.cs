using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HasEnoughHungerLevel : Conditional {

    public HumanoidNeeds humanoidNeeds;

    public override void OnAwake() {
        humanoidNeeds = GetComponent<HumanoidNeeds>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidNeeds.GetHunger() > 90) {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
