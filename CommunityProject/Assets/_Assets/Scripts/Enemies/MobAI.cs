using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    private enum State {
        Idle,
        FollowingPlayer,
        Attacking,
    };

    private Mob mob;
    private MobMovement mobMovement;
    private MobAttack mobAttack;
    private Collider2D collider2D;
    private Collider2D playerCollider;

    private float aggroRange;
    private float attackRange;
    private float attackTriggerRange;
    private bool playerEnteredAggroRange;

    private float distanceToPlayerCollider;
    private void Awake() {
        collider2D = GetComponent<Collider2D>();
        mobMovement = GetComponent<MobMovement>();
        mobAttack = GetComponent<MobAttack>();
        mob = GetComponent<Mob>();
        playerCollider = Player.Instance.GetComponent<Collider2D>();

        aggroRange = mob.GetMobSO().mobAggroRange;
        attackRange = mob.GetMobSO().mobAttackRange;

        attackTriggerRange = attackRange / 2;
    }

    private void Update() {
        ColliderDistance2D colliderDistance2DToPlayerCollider = playerCollider.Distance(collider2D);
        distanceToPlayerCollider = colliderDistance2DToPlayerCollider.distance;

        if (!playerEnteredAggroRange) {
            CheckIfPlayerEntersAggroRange();
        } else {
            if(distanceToPlayerCollider < attackTriggerRange) {
                mobAttack.AttackTarget(Player.Instance.transform.position);
            } else {
                FollowPlayer(colliderDistance2DToPlayerCollider);
            }
        }
    }

    private void FollowPlayer(ColliderDistance2D colliderDistance2DToPlayerCollider) {
        mobMovement.CalculatePath(colliderDistance2DToPlayerCollider.pointA);
    }

    private void CheckIfPlayerEntersAggroRange() {
        if(distanceToPlayerCollider < aggroRange) {
            playerEnteredAggroRange = true;
        }
    }

}
