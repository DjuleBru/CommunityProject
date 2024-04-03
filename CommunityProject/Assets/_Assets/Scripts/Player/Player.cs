using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private CinemachineVirtualCamera battleCamera;

    private Inventory playerInventory;
    [SerializeField] private InventoryUI playerInventoryUI;

    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerDamaged;
    public event EventHandler<IDamageable.OnIDamageableHealthChangedEventArgs> OnIDamageableHealthChanged;

    private int playerBaseHP = 100;
    private int playerHP;
    private int playerBaseDamage = 5;

    private void Awake() {
        Instance = this;
        playerHP = playerBaseHP;

        playerInventory = new Inventory(true, 3, 3);
        playerInventoryUI.SetInventory(playerInventory);
    }

    private void Start() {
        playerInventory.AddItem(new Item { itemType = Item.ItemType.Wood, amount = 5 });
    }

    public void SetBattleCameraAsPriority() {
        battleCamera.m_Priority = 12;
    }

    public void ResetBattleCameraPriority() {
        battleCamera.m_Priority = 9;
    }

    public void TakeDamage(int damage) {
        playerHP -= damage;
        OnIDamageableHealthChanged?.Invoke(this, new IDamageable.OnIDamageableHealthChangedEventArgs {
            previousHealth = playerHP + damage,
            newHealth = playerHP
        });
    }

    public int GetPlayerBaseAttackDamage() {
        return playerBaseDamage;
    }

    public void SetDead() {

    }

    public int GetHP() {
        return playerHP;
    }

    public int GetMaxHP() {
        return playerBaseHP;
    }

    public Inventory GetInventory() {
        return playerInventory;
    }

    public void DisablePlayerActions() {
        PlayerMovement.Instance.DisableMovement();
        PlayerAttack.Instance.DisableAttacks();
    }

    public void EnablePlayerActions() {
        PlayerMovement.Instance.EnableMovement();
        PlayerAttack.Instance.EnableAttacks();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

        if(itemWorld != null) {
            if(SavingSystem.Instance.GetSceneIsOverworld()) {
                playerInventory.AddItem(itemWorld.GetItem());
            } 
            if(SavingSystem.Instance.GetSceneIsDungeon()) {
                DungeonManager.Instance.GetDungeonInventory().AddItem(itemWorld.GetItem());
            }

            itemWorld.DestroySelf();
        }
    }
}
