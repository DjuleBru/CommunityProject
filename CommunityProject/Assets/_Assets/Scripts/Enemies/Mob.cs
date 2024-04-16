using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mob : MonoBehaviour, IDamageable
{
    [SerializeField] private MobSO mobSO;
    private Collider2D mobCollider;
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
        mobCollider = GetComponent<Collider2D>();
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
        mobCollider.enabled = false;
        OnMobDied?.Invoke(this, EventArgs.Empty);
        parentDungeonRoom.RemoveMobFromDungeonRoomMobList(this);
        SpawnItems();
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

    public void SpawnItems() {

        List<float> dropProbabilities = new List<float>();

        float dropProbability = 100;

        foreach(ItemDropRate dropRate in mobSO.itemDropRateList) {
            dropProbability -= dropRate.dropRate;
            dropProbabilities.Add(dropProbability);
        }

        for (int i = 0; i < mobSO.maxItemDrops; i++) {
            int dropAttempt = UnityEngine.Random.Range(0, 100);

            if (dropAttempt <= mobSO.dropProbability) {
                // Successful drop

                float itemAttempt = UnityEngine.Random.Range(0, 100);

                for (int j = 0; j < dropProbabilities.Count; j++) {
                    if (itemAttempt >= dropProbabilities[j]) {
                        ItemSO droppedItemSO = mobSO.itemDropRateList[j].itemSO;
                        int dropAmount = 1;

                        if (droppedItemSO.isStackable) {
                            dropAmount = UnityEngine.Random.Range(0, mobSO.itemDropRateList[j].maxDropAmount);
                        }

                        SpawnItem(mobSO.itemDropRateList[j].itemSO, dropAmount);
                        
                        break;
                    };
                }
            }

        }
    }

    public void SpawnItem(ItemSO itemSO, int dropAmount) {
        Item droppedItem = new Item { itemType = itemSO.itemType, amount = dropAmount };
        ItemWorld.DropItem(transform.position, droppedItem, 5f, false);
    }

}
