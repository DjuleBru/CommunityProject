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
        
        if(humanoidCarry.IdentifyBestDestinationBuilding() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

}