using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour {
    public static PlayerAnimatorManager Instance { get; private set; }

    private Animator animator;
    [SerializeField] private Animator bodyAnimator;

    private Vector2 lastMoveDir;

    private string bodyAnimationType;

    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Player.Instance.OnPlayerDamaged += Player_OnPlayerDamaged;
        PlayerAttack.Instance.OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        PlayerAttack.Instance.OnActiveWeaponSOChanged += PlayerAttack_OnActiveWeaponSOChanged;

        bodyAnimationType = PlayerAttack.Instance.GetActiveWeaponSO().bodyAnimationType.ToString();
        animator.SetBool(bodyAnimationType, true);
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
        if(lastMoveDir.y == 0) {
            lastMoveDir.y = -0.01f;
        }
        if(lastMoveDir.x == 0) {
            lastMoveDir.x = 0.01f;
        }
        animator.SetFloat("X", lastMoveDir.x);
        animator.SetFloat("Y", lastMoveDir.y);
    }

    private void PlayerAttack_OnPlayerAttack(object sender, System.EventArgs e) {
        animator.SetTrigger("Attack");
    }

    private void Player_OnPlayerDamaged(object sender, System.EventArgs e) {
        bodyAnimator.SetTrigger("Damaged");
    }

    private void PlayerAttack_OnActiveWeaponSOChanged(object sender, EventArgs e) {
        bodyAnimator.SetBool(bodyAnimationType, false);

        bodyAnimationType = PlayerAttack.Instance.GetActiveWeaponSO().bodyAnimationType.ToString();
        bodyAnimator.SetBool(bodyAnimationType, true);
    }
    public Vector2 GetLastMoveDir() {
        return lastMoveDir;
    }
}
