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
        mobAttack.OnMobRangedAttack += MobAttack_OnMobRangedAttack;
    }

    private void MobAttack_OnMobRangedAttack(object sender, System.EventArgs e) {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetTrigger("RangedAttack");
    }

    private void Mob_OnIDamageableHealthChanged(object sender, IDamageable.OnIDamageableHealthChangedEventArgs e) {
        if (animator != null) {
            bodyAnimator.SetTrigger("Damaged");
        }
    }

    private void MobAttack_OnMobAttack(object sender, System.EventArgs e) {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
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

        animator.SetFloat("X", moveDirNormalized.x);
        animator.SetFloat("Y", moveDirNormalized.y);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("RangedAttack")) {

            animator.SetBool("Walking", false);
            animator.SetBool("Idle", false);
            return;
        }

        if (!mobMovement.GetCanMove()) {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
            return;
        }

        if (!reachedEndOfPath) {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }

    }
}
