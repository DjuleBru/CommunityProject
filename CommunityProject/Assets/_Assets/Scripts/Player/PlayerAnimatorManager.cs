using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator animator;
    private float X;
    private float Y;

    private Vector2 lastMoveDir;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        Vector2 moveDir = PlayerMovement.Instance.GetMovementVectorNormalized();

        if (moveDir != Vector2.zero) {
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }

        // Handle last move dir
        if (moveDir == Vector2.zero) {
            moveDir = lastMoveDir;
        }
        lastMoveDir = moveDir;

        animator.SetFloat("X", lastMoveDir.x);
        animator.SetFloat("Y", lastMoveDir.y);
    }

}
