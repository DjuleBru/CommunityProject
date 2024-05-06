using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponReloadUI : MonoBehaviour
{
    [SerializeField] private Image weaponReloadImage;
    [SerializeField] private Image weaponIcon;

    private bool reloading;

    private void Awake() {
        weaponReloadImage.fillAmount = 0;
    }
    private void Start() {
        PlayerAttack.Instance.OnPlayerAttackEnded += PlayerAttack_OnPlayerAttackEnded;
        PlayerAttack.Instance.OnActiveWeaponSOChanged += PlayerAttack_OnActiveWeaponSOChanged;
    }

    private void PlayerAttack_OnActiveWeaponSOChanged(object sender, System.EventArgs e) {
        weaponIcon.sprite = PlayerAttack.Instance.GetActiveWeaponSO().weaponSpriteUI;
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
