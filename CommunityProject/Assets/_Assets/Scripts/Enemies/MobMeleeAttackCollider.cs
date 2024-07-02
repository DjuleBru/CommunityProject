using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMeleeAttackCollider : MonoBehaviour
{
    private Collider2D meleeAttackCollider;
    [SerializeField] private MobAttack mobAttack;

    private void Awake() {
        meleeAttackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.TryGetComponent(out Player player)) {
            player.TakeDamage(mobAttack.GetAttackDamage());
            player.TakeKnockback(mobAttack.GetMeleeKnockback(), transform.position);
        }
    }
}
