using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class HasEnoughHealth : Conditional {
    private Humanoid humanoid;

    public override void OnAwake() {
        humanoid = GetComponent<Humanoid>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoid.GetHealthNormalized() <= .1f) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}