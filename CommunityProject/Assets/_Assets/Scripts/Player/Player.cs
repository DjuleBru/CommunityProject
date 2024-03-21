using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private CinemachineVirtualCamera battleCamera;
    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerDamaged;
    public event EventHandler<IDamageable.OnIDamageableHealthChangedEventArgs> OnIDamageableHealthChanged;

    private int playerBaseHP = 100;
    private int playerHP;
    private int playerBaseDamage = 5;

    private void Awake() {
        Instance = this;

        playerHP = playerBaseHP;
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

    public void DisablePlayerActions() {
        PlayerMovement.Instance.DisableMovement();
        PlayerAttack.Instance.DisableAttacks();
    }

    public void EnablePlayerActions() {
        PlayerMovement.Instance.EnableMovement();
        PlayerAttack.Instance.EnableAttacks();
    }
}
