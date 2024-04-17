using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : Action
{
    public HumanoidMovement humanoidMovement;
    public override void OnAwake() {
        humanoidMovement = GetComponent<HumanoidMovement>();
    }

    public override TaskStatus OnUpdate() {
        Debug.Log(humanoidMovement + " roam beh.tree");
        humanoidMovement.Roam(transform.position, 4f);
        return TaskStatus.Success;
    }
}
