using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasJobAssigned : Conditional
{
    public Humanoid humanoid;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoid.GetHasJobAssigned()) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
