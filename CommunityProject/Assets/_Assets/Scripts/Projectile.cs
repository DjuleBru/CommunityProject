using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float moveSpeed;
    private int damage;
    private Vector3 moveDirNormalized;

    private float projectileLifeTime = 10f;

    public void InitializeProjectile(Vector3 target, float moveSpeed, int damage, Sprite projectileSprite) {
        moveDirNormalized = (target - transform.position).normalized;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        spriteRenderer.sprite = projectileSprite;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveDirNormalized.y, moveDirNormalized.x) * Mathf.Rad2Deg);
    }


    private void Update() {
        projectileLifeTime -= Time.deltaTime;
        if(projectileLifeTime < 0 ) {
            Destroy(gameObject);
        }

        transform.position += moveDirNormalized * moveSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision != null ) {
            if(collision.gameObject.GetComponent<Player>() != null ) {
                Player.Instance.TakeDamage(damage);
            }
        }

        if (collision.gameObject.GetComponent<Mob>() == null && collision.gameObject.GetComponent<MobMeleeAttackCollider>() == null) {
            Destroy(gameObject);
        }
    }
}
