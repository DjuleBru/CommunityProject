using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HasHousingAssigned : Conditional {
    private HumanoidNeeds humanoidNeeds;

    public override void OnAwake() {
        humanoidNeeds = GetComponent<HumanoidNeeds>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidNeeds.GetAssignedHousing() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

}
