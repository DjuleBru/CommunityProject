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

    private void Awake() {
        animator = GetComponent<Animator>();
        mobMovement = GetComponentInParent<MobMovement>();
        mobAttack = GetComponentInParent<MobAttack>();
        mob = GetComponentInParent<Mob>();
    }
    protected virtual void Start() {
        mob.OnMobDamaged += Mob_OnMobDamaged;
        mob.OnMobDied += Mob_OnModDied;
        mobAttack.OnMobAttack += MobAttack_OnMobAttack;
    }

    private void MobAttack_OnMobAttack(object sender, System.EventArgs e) {
        animator.SetTrigger("Attack");
    }

    private void Mob_OnModDied(object sender, System.EventArgs e) {
        animator.SetTrigger("Die");
    }

    private void Mob_OnMobDamaged(object sender, System.EventArgs e) {
        if (animator != null) {
            bodyAnimator.SetTrigger("Damaged");
        }
    }

    private void Update() {

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
