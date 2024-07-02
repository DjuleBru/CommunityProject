using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class WithinAttackRange : Conditional {

    public float meleeAttackRange;
    public float rangedAttackRange;
    public MobAttack.AttackType attackType;

    public Collider2D playerCollider;
    public Collider2D collider;

    public override void OnAwake() {
        meleeAttackRange = GetComponent<Mob>().GetMobSO().mobMeleeAttackRange;
        rangedAttackRange = GetComponent<Mob>().GetMobSO().mobRangedAttackRange;
        attackType = GetComponent<Mob>().GetMobSO().attackType;

        collider = GetComponent<Collider2D>();
        playerCollider = Player.Instance.GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {
        ColliderDistance2D colliderDistance2DToPlayerCollider = playerCollider.Distance(collider);

        if (attackType == MobAttack.AttackType.Melee) {
            if (colliderDistance2DToPlayerCollider.distance < meleeAttackRange / 2) {
                return TaskStatus.Success;
            }
        }

        if (attackType == MobAttack.AttackType.Ranged) {
            if (colliderDistance2DToPlayerCollider.distance < meleeAttackRange / 2) {
                Debug.Log(collider + " too close to attack");
                return TaskStatus.Failure; ;
            }

            if (colliderDistance2DToPlayerCollider.distance < rangedAttackRange / 2) {
                return TaskStatus.Success;
            }
        }

        if (attackType == MobAttack.AttackType.MeleeAndRanged) {

            if (colliderDistance2DToPlayerCollider.distance < meleeAttackRange / 2) {
                return TaskStatus.Success;
            }

            if (colliderDistance2DToPlayerCollider.distance < rangedAttackRange / 2) {
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }
}
