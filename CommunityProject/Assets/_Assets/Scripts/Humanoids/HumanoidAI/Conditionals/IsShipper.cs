using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsShipper : Conditional {
    public Humanoid humanoid;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoid.IsShipper()) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
