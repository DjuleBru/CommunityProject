using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobAttack : MonoBehaviour
{
    public enum AttackType {
        Melee,
        Ranged
    }

    public event EventHandler OnMobAttack;

    private Mob mob;
    private AttackType attackType;
    private int attackDmg;
    private int attackRate;

    private float attackTimer;

    private void Awake() {
        mob = GetComponent<Mob>();
        attackType = mob.GetMobSO().attackType;
        attackDmg = mob.GetMobSO().attackDmg;
        attackRate = mob.GetMobSO().mobAttackRate;

        attackTimer = 0;
    }

    public void AttackTarget(Vector3 targetPosition) {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0) {
            attackTimer = attackRate;

            OnMobAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetAttackTimer() {
        attackTimer = 0;
    }

    public int GetAttackDamage() {
        return attackDmg;
    }
}
