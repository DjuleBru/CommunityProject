using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HasAutoAssign : Conditional
{
    public Humanoid humanoid;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoid.GetAutoAssign()) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
