using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AttackTarget : Action {

    public MobAttack mobAttack;
    public override void OnAwake() {
        mobAttack = GetComponent<MobAttack>();
    }

    public override TaskStatus OnUpdate() {

        if (mobAttack.GetAttackType() == MobAttack.AttackType.Melee) {
            mobAttack.AttackTarget(Player.Instance.transform.position);
        }

        if (mobAttack.GetAttackType() == MobAttack.AttackType.Ranged) {
            mobAttack.RangedAttackTarget(Player.Instance.transform.position);
        }

        return TaskStatus.Success;

    }

}
