using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReloadUI : MonoBehaviour
{
    [SerializeField] private Image weaponReloadImage;

    private bool reloading;

    private void Awake() {
        weaponReloadImage.fillAmount = 0;
    }
    private void Start() {
        PlayerAttack.Instance.OnPlayerAttackEnded += PlayerAttack_OnPlayerAttackEnded;
    }

    private void Update() {
        if(reloading) {
            weaponReloadImage.fillAmount = PlayerAttack.Instance.GetAttackTimerNormalized();

            if(PlayerAttack.Instance.GetAttackTimerNormalized() <= 0) {
                reloading = false;
            }
        }
    }

    private void PlayerAttack_OnPlayerAttackEnded(object sender, System.EventArgs e) {
        reloading = true;
    }
}
