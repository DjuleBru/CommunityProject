using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour, IDamageable
{
    [SerializeField] private MobSO mobSO;
    private Collider2D collider;
    private Rigidbody2D rb;

    public event EventHandler<IDamageable.OnIDamageableHealthChangedEventArgs> OnIDamageableHealthChanged;
    public event EventHandler OnMobDied;

    private DungeonRoom parentDungeonRoom;
    private bool allMobsSpawned;
    private bool dead;

    public MobSO GetMobSO() {
        return mobSO;
    }

    private int mobHP;

    private void Awake() {
        mobHP = mobSO.mobHP;
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) {
        mobHP -= damage;
        OnIDamageableHealthChanged?.Invoke(this, new IDamageable.OnIDamageableHealthChangedEventArgs {
            previousHealth = mobHP + damage,
            newHealth = mobHP
        });
        if (mobHP <= 0 ) {
            SetDead();
        }
    }

    public void TakeKnockback(Vector3 knockBackOrigin, float knockBackForce) {
        if (dead) return;
        Vector2 knockBackDir = (transform.position - knockBackOrigin).normalized;
        Vector2 knockBack = knockBackDir * knockBackForce;
        rb.AddForce(knockBack);
    }

    public void SetDead() {
        collider.enabled = false;
        OnMobDied?.Invoke(this, EventArgs.Empty);
        parentDungeonRoom.RemoveMobFromDungeonRoomMobList(this);
        dead = true;
    }

    public void SetAllMobsSpawned() {
        allMobsSpawned = true;
    }

    public bool GetAllMobsSpawned() {
        return allMobsSpawned;
    }

    public bool GetDead() {
        return dead;
    }

    public void SetParentDungeonRoom(DungeonRoom dungeonRoom) {
        parentDungeonRoom = dungeonRoom;
    }

    public void DestroyMob() {
        Destroy(gameObject);
    }

    public int GetHP() {
        return mobHP;
    }

    public int GetMaxHP() {
        return mobSO.mobHP;
    }

}
