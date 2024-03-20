using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimatorManager : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] private Animator bodyAnimator;
    protected MobMovement mobMovement;
    protected MobAttack mobAttack;
    protected Mob mob;

    private bool dead;

    private void Awake() {
        animator = GetComponent<Animator>();
        mobMovement = GetComponentInParent<MobMovement>();
        mobAttack = GetComponentInParent<MobAttack>();
        mob = GetComponentInParent<Mob>();
    }

    protected virtual void Start() {
        mob.OnIDamageableHealthChanged += Mob_OnIDamageableHealthChanged;
        mob.OnMobDied += Mob_OnModDied;
        mobAttack.OnMobAttack += MobAttack_OnMobAttack;
    }

    private void Mob_OnIDamageableHealthChanged(object sender, IDamageable.OnIDamageableHealthChangedEventArgs e) {
        if (animator != null) {
            bodyAnimator.SetTrigger("Damaged");
        }
    }

    private void MobAttack_OnMobAttack(object sender, System.EventArgs e) {
        animator.SetTrigger("Attack");
    }

    private void Mob_OnModDied(object sender, System.EventArgs e) {
        animator.SetTrigger("Die");
        dead = true;
    }

    private void Update() {

        if (dead) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .95) {
                //Die animation finished playing
                mob.DestroyMob();
            }
        }

        Vector3 moveDirNormalized = mobMovement.GetMoveDirNormalized();
        bool reachedEndOfPath = mobMovement.GetReachedEndOfPath();

        if (!reachedEndOfPath) {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
        else {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
        animator.SetFloat("X", moveDirNormalized.x);
        animator.SetFloat("Y", moveDirNormalized.y);
    }
}
