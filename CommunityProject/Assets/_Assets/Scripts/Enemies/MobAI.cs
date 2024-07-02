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
    private MobAttack.AttackType attackType;
    private Collider2D collider2D;
    private Collider2D playerCollider;

    private float aggroRange;
    private float meleeAttackRange;
    private float rangedAttackRange;
    private float meleeAttackTriggerRange;
    private float rangedAttackTriggerRange;
    private bool playerEnteredAggroRange;

    private float distanceToPlayerCollider;

    private bool dead;

    private void Awake() {
        collider2D = GetComponent<Collider2D>();
        mobMovement = GetComponent<MobMovement>();
        mobAttack = GetComponent<MobAttack>();
        mob = GetComponent<Mob>();
        playerCollider = Player.Instance.GetComponent<Collider2D>();

        attackType = mob.GetMobSO().attackType;
        Debug.Log(attackType);
        aggroRange = mob.GetMobSO().mobAggroRange;
        meleeAttackRange = mob.GetMobSO().mobMeleeAttackRange;
        rangedAttackRange = mob.GetMobSO().mobRangedAttackRange;

        meleeAttackTriggerRange = meleeAttackRange / 2;
        rangedAttackTriggerRange = rangedAttackRange / 2;
    }

    private void Start() {
        mob.OnMobDied += Mob_OnMobDied;
    }

    private void Update() {

        if (dead) {
            return;
        }

        ColliderDistance2D colliderDistance2DToPlayerCollider = playerCollider.Distance(collider2D);
        distanceToPlayerCollider = colliderDistance2DToPlayerCollider.distance;

        if (!playerEnteredAggroRange) {
            CheckIfPlayerEntersAggroRange();
        } else {

            if(attackType == MobAttack.AttackType.Melee) {
                if (distanceToPlayerCollider < meleeAttackTriggerRange) {
                    mobAttack.AttackTarget(Player.Instance.transform.position);
                }
                else {
                    FollowPlayer(colliderDistance2DToPlayerCollider);
                }
            }

            if (attackType == MobAttack.AttackType.Ranged) {
                if (distanceToPlayerCollider < rangedAttackTriggerRange) {
                    mobAttack.RangedAttackTarget(Player.Instance.transform.position);
                }
                else {
                    FleePlayer(colliderDistance2DToPlayerCollider);
                }
            }


            if (attackType == MobAttack.AttackType.MeleeAndRanged) {
                if (distanceToPlayerCollider < rangedAttackTriggerRange) {
                    mobAttack.AttackTarget(Player.Instance.transform.position);
                }
                else {
                    FleePlayer(colliderDistance2DToPlayerCollider);
                }
            }

        }
    }

    private void FollowPlayer(ColliderDistance2D colliderDistance2DToPlayerCollider) {
        mobMovement.CalculatePath(colliderDistance2DToPlayerCollider.pointB, colliderDistance2DToPlayerCollider.pointA);
    }

    private void FleePlayer(ColliderDistance2D colliderDistance2DToPlayerCollider) {
        mobMovement.CalculateFleePath(colliderDistance2DToPlayerCollider.pointA);
    }

    private void CheckIfPlayerEntersAggroRange() {
        if(distanceToPlayerCollider < aggroRange) {
            playerEnteredAggroRange = true;
        }
    }

    private void Mob_OnMobDied(object sender, System.EventArgs e) {
        dead = true;
    }

}
