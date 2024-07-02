using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public static PlayerAttack Instance { get; private set; }

    [SerializeField] private WeaponSO activeWeaponSO;
    private WeaponSO storeWeaponSO;
    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerAttackEnded;
    public event EventHandler OnActiveWeaponSOChanged;

    private bool attacking;
    private bool canAttack = true;

    private int attackDamage;
    private float attackKnockback;
    private float attackRate;
    private float attackTimer;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
        UpdateActiveStats();
    }

    private void Update() {
        if(!attacking) {
            attackTimer -= Time.deltaTime;
        }
    }

    private void GameInput_OnAttackAction(object sender, EventArgs e) {
        if (!canAttack) return;
        if(attackTimer < 0) {
            attackTimer = attackRate;
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        }
    }


    public WeaponSO GetActiveWeaponSO() {
        return activeWeaponSO;
    }

    public void ChangeActiveWeaponSO(WeaponSO weaponSO) {
        activeWeaponSO = weaponSO;
        OnActiveWeaponSOChanged?.Invoke(this, EventArgs.Empty);
        UpdateActiveStats();
    }

    public void ChangeToolWeaponSO(WeaponSO toolWeaponSO) {
        storeWeaponSO = activeWeaponSO;
        activeWeaponSO = toolWeaponSO;
        OnActiveWeaponSOChanged?.Invoke(this, EventArgs.Empty);
        UpdateActiveStats();
    }

    public void RemoveToolWeaponSO() {
        activeWeaponSO = storeWeaponSO;
        OnActiveWeaponSOChanged?.Invoke(this, EventArgs.Empty);
        UpdateActiveStats();
    }

    private void UpdateActiveStats() {
        attackDamage = (int)PlayerEquipment.Instance.GetDamage();

        if(activeWeaponSO.isTool) {
            attackRate = activeWeaponSO.toolAttackRate;
        } else {
            attackRate = PlayerEquipment.Instance.GetAttackSpeed();
        }

        attackKnockback = activeWeaponSO.weaponKnockback;
    }

    public void SetAttacking(bool attacking) {
        this.attacking = attacking;
    }

    public void SetAttackEnded() {
        OnPlayerAttackEnded?.Invoke(this, EventArgs.Empty);
    }

    public float GetAttackTimerNormalized() {
        return attackTimer / attackRate;
    }

    public bool GetAttacking() {
        return attacking;
    }

    public float GetAttackRate() { 
        return attackRate;
    }

    public int GetTotalAttackDamage() {
        return attackDamage;
    }

    public float GetAttackKnockback() {
        return attackKnockback;
    }

    public void EnableAttacks() {
        canAttack = true;
    }

    public void DisableAttacks() {
        canAttack = false;
    }
}
