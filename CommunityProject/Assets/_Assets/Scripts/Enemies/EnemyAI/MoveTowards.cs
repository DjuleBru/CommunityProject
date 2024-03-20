using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class MoveTowards : Action
{
    public float moveSpeed;
    public MobMovement mobMovement;
    public Collider2D playerCollider;
    public Collider2D collider;

    public override void OnAwake() {
        moveSpeed = GetComponent<Mob>().GetMobSO().mobMoveSpeed;
        mobMovement = GetComponent<MobMovement>();
        collider = GetComponent<Collider2D>();

        playerCollider = Player.Instance.GetComponent<Collider2D>();
    }

    public override TaskStatus OnUpdate() {
        ColliderDistance2D colliderDistance2DToPlayerCollider = playerCollider.Distance(collider);
        mobMovement.CalculatePath(colliderDistance2DToPlayerCollider.pointB, colliderDistance2DToPlayerCollider.pointA);

        return TaskStatus.Running;
    }

}
