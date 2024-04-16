using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimatorManager : MonoBehaviour
{
    protected Animator animator;
    protected HumanoidMovement humanoidMovement;
    protected Humanoid humanoid;


    private void Awake() {
        animator = GetComponent<Animator>();
        humanoidMovement = GetComponentInParent<HumanoidMovement>();
        humanoid = GetComponentInParent<Humanoid>();
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
}
