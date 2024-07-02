using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System;
using UnityEngine;

public class WithinAggroRange : Conditional
{
    public float aggroRange;

    public override void OnAwake() {
        aggroRange = GetComponent<Mob>().GetMobSO().mobAggroRange;
    }

    public override TaskStatus OnUpdate() {

        if(Vector3.Distance(Player.Instance.transform.position, transform.position) < aggroRange) {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

}
