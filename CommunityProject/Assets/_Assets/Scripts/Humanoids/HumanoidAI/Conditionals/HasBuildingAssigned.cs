using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasBuildingAssigned : Conditional {

    public Humanoid humanoid;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoid.GetAssignedBuilding() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
