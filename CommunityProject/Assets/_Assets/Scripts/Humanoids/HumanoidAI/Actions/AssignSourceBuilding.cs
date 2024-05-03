using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignSourceBuilding : Action {

    public Humanoid humanoid;
    public HumanoidHaul humanoidCarry;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidCarry = GetComponent<HumanoidHaul>();
    }

    public override TaskStatus OnUpdate() {

        if (humanoid.GetAutoAssign()) {
            if (humanoidCarry.IdentifyBestSourceBuilding(humanoidCarry.GetItemToCarry()) != null) {
                return TaskStatus.Success;
            }
        }
        else {
            if (humanoidCarry.GetSourceBuilding() != null) {
                return TaskStatus.Success;
            }
            else {
                return TaskStatus.Running;
            }
        }

        return TaskStatus.Failure;
    }
}
