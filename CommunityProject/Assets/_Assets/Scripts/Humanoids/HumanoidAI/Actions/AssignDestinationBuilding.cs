using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignDestinationBuilding : Action {

    public Humanoid humanoid;
    public HumanoidCarry humanoidCarry;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
        humanoidCarry = GetComponent<HumanoidCarry>();
    }

    public override TaskStatus OnUpdate() {
        
        if(humanoid.GetAutoAssign()) {
            if (humanoidCarry.TryAssignBestDestinationBuilding() != null) {
                return TaskStatus.Success;
            }
        } else {
            if (humanoidCarry.GetDestinationBuilding() != null) {
                return TaskStatus.Success;
            }
            else {
                return TaskStatus.Running;
            }
        }
        
        return TaskStatus.Failure;
    }

}
