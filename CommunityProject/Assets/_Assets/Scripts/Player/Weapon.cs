using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private bool weaponCanPierce;
    private int targetCount;

    private void Start() {
        weaponCanPierce = PlayerAttack.Instance.GetActiveWeaponSO().weaponCanPierce;
        PlayerAttack.Instance.OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        PlayerAttack.Instance.OnPlayerAttackEnded += PlayerAttack_OnPlayerAttackEnded;
    }

    private void PlayerAttack_OnPlayerAttackEnded(object sender, System.EventArgs e) {
        targetCount = 0;
    }

    private void PlayerAttack_OnPlayerAttack(object sender, System.EventArgs e) {
        targetCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Mob mob)) {
            if(weaponCanPierce) {
                // Weapon hits all targets

                mob.TakeDamage(PlayerAttack.Instance.GetTotalAttackDamage());
                mob.TakeKnockback(transform.position, PlayerAttack.Instance.GetAttackKnockback());
            } else {
                // Weapon hits only the first target
                if(targetCount < 1) {
                    mob.TakeDamage(PlayerAttack.Instance.GetTotalAttackDamage());
                    mob.TakeKnockback(transform.position, PlayerAttack.Instance.GetAttackKnockback());
                    targetCount++;
                }
            }

        }
    }
}
