using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AttackTarget : Action {

    public MobAttack mobAttack;
    public override void OnAwake() {
        mobAttack = GetComponent<MobAttack>();
    }

    public override TaskStatus OnUpdate() {
        mobAttack.AttackTarget(Player.Instance.transform.position);
        return TaskStatus.Running;
    }

}
