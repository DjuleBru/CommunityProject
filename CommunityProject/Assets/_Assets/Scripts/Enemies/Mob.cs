using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour, IDamageable
{
    [SerializeField] private MobSO mobSO;

    public event EventHandler OnMobDamaged;
    public event EventHandler OnMobDied;

    public MobSO GetMobSO() {
        return mobSO;
    }

    private int mobHP;

    private void Awake() {
        mobHP = mobSO.mobHP;
    }

    public void TakeDamage(int damage) {
        mobHP -= damage;
        OnMobDamaged?.Invoke(this, EventArgs.Empty);
        if (mobHP < 0 ) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
