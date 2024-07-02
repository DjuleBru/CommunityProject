using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobAttack : MonoBehaviour
{
    public enum AttackType {
        Melee,
        Ranged,
        MeleeAndRanged,
    }

    public event EventHandler OnMobAttack;
    public event EventHandler OnMobRangedAttack;

    private Mob mob;
    private AttackType attackType;
    private int attackDmg;
    private float attackRate;
    private float attackDelay;

    [SerializeField] private GameObject projectileGameObject;
    [SerializeField] private Transform projectileSpawnPosition;
    private float attackTimer;
    private float rangedAttackTimer;

    private float meleeAttackKnockback;
    private float rangedAttackKnockback;

    private bool attackAnimationStarted;

    private void Awake() {
        mob = GetComponent<Mob>();
        attackType = mob.GetMobSO().attackType;
        attackDmg = mob.GetMobSO().attackDmg;
        attackRate = mob.GetMobSO().mobAttackRate;
        attackDelay = mob.GetMobSO().mobAttackDelay;
        rangedAttackTimer = mob.GetMobSO().rangedAttackAnimationDelay;
        meleeAttackKnockback = mob.GetMobSO().meleeAttackKnockback;
        rangedAttackKnockback = mob.GetMobSO().rangedAttackKnockback;
        attackTimer = attackDelay;
    }

    public void AttackTarget(Vector3 targetPosition) {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0) {
            attackTimer = attackRate;

            OnMobAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RangedAttackTarget(Vector3 targetPosition) {
        attackTimer -= Time.deltaTime;
        GetComponent<MobMovement>().StopMoving();

        if (attackTimer < 0) {

            if(!attackAnimationStarted) {
                attackAnimationStarted = true;
                OnMobRangedAttack?.Invoke(this, EventArgs.Empty);
            }

            rangedAttackTimer -= Time.deltaTime;

            if(rangedAttackTimer < 0) {

                attackTimer = attackRate;
                rangedAttackTimer = mob.GetMobSO().rangedAttackAnimationDelay;
                InstantiateProjectile(targetPosition);
                attackAnimationStarted = false;
            }
        }
    }

    private void InstantiateProjectile(Vector3 targetPosition) {
        Projectile projectile = Instantiate(projectileGameObject, projectileSpawnPosition.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.InitializeProjectile(targetPosition, mob.GetMobSO().projectileSpeed, mob.GetMobSO().projectileDamage, mob.GetMobSO().projectileSprite);
    }

    public void ResetAttackTimer() {
        attackTimer = attackDelay;
    }

    public int GetAttackDamage() {
        return attackDmg;
    }

    public float GetMeleeKnockback() {
        return meleeAttackKnockback;
    }
    public float GetRangedeKnockback() {
        return rangedAttackKnockback;
    }

    public AttackType GetAttackType() {
        return attackType;
    }
}
