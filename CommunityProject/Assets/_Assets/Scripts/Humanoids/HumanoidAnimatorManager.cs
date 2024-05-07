using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimatorManager : MonoBehaviour
{
    protected Animator animator;
    protected HumanoidMovement humanoidMovement;
    protected HumanoidCarry humanoidCarry;
    protected Humanoid humanoid;


    private void Awake() {
        animator = GetComponent<Animator>();
        humanoidMovement = GetComponentInParent<HumanoidMovement>();
        humanoidCarry = GetComponentInParent<HumanoidCarry>();
        humanoid = GetComponentInParent<Humanoid>();
    }

    private void Start() {
        if(humanoidCarry != null) {
            humanoidCarry.OnCarryCompleted += HumanoidCarry_OnCarryCompleted;
            humanoidCarry.OnCarryStarted += HumanoidCarry_OnCarryStarted;
        }
    }

    private void HumanoidCarry_OnCarryStarted(object sender, System.EventArgs e) {
        animator.SetBool("Carrying", true);
    }

    private void HumanoidCarry_OnCarryCompleted(object sender, System.EventArgs e) {
        animator.SetBool("Carrying", false);
    }

    private void Update() {
        Vector3 moveDirNormalized = humanoidMovement.GetMoveDirNormalized();
        bool reachedEndOfPath = humanoidMovement.GetReachedEndOfPath();

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

    public void SetAnimator(AnimatorOverrideController animatorController) {
        animator.runtimeAnimatorController = animatorController;
    }
}
