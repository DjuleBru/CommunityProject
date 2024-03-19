using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public static PlayerAttack Instance { get; private set; }

    [SerializeField] private WeaponSO activeWeaponSO;
    public event EventHandler OnPlayerAttack;
    public event EventHandler OnActiveWeaponSOChanged;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }

    private void GameInput_OnAttackAction(object sender, EventArgs e) {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }


    public WeaponSO GetActiveWeaponSO() {
        return activeWeaponSO;
    }

    public void ChangeActiveWeaponSO(WeaponSO weaponSO) {
        activeWeaponSO = weaponSO;
    }
}
