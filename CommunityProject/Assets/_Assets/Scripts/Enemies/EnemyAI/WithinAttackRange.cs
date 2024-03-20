using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class WithinAttackRange : Conditional {

    public float attackRange;

    public Collider2D playerCollider;
    public Collider2D collider;

    public override void OnAwake() {
        attackRange = GetComponent<Mob>().GetMobSO().mobAttackRange; 

        collider = GetComponent<Collider2D>();
        playerCollider = Player.Instance.GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {
        ColliderDistance2D colliderDistance2DToPlayerCollider = playerCollider.Distance(collider);

        if (colliderDistance2DToPlayerCollider.distance < attackRange / 2) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
