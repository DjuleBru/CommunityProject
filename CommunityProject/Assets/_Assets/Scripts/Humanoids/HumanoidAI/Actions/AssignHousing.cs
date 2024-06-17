using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class AssignHousing : Action {

    private HumanoidNeeds humanoidNeeds;
    private Humanoid humanoid;

    public override void OnAwake() {
        humanoidNeeds = GetComponent<HumanoidNeeds>();
    }
    public override TaskStatus OnUpdate() {

        if (humanoidNeeds.GetAssignedHousing() == null) {
            humanoidNeeds.AssignHousing();
        }
        return TaskStatus.Success;
    }
}
