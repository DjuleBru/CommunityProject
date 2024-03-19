using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera battleCamera;
    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerDamaged;
    private int playerHP = 100;

    private void Awake() {
        Instance = this;
    }

    public void SetBattleCameraAsPriority() {
        battleCamera.m_Priority = 12;
    }

    public void ResetBattleCameraPriority() {
        battleCamera.m_Priority = 9;
    }

    public void TakeDamage(int damage) {
        playerHP -= damage;
        OnPlayerDamaged?.Invoke(this, EventArgs.Empty);
    }
    

}
