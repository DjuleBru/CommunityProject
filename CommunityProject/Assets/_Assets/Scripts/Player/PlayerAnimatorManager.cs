using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour {
    public static PlayerAnimatorManager Instance { get; private set; }

    public enum PlayerInteractBool {
        CutTree,
        Mine,


    }

    private Animator animator;
    [SerializeField] private Animator bodyAnimator;

    private Vector2 lastMoveDir;

    private string bodyAnimationType;

    private bool attacking;
    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Player.Instance.OnIDamageableHealthChanged += Player_OnIDamageableHealthChanged;
        PlayerAttack.Instance.OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        PlayerAttack.Instance.OnActiveWeaponSOChanged += PlayerAttack_OnActiveWeaponSOChanged;

        bodyAnimationType = PlayerAttack.Instance.GetActiveWeaponSO().bodyAnimationType.ToString();
        animator.SetBool(bodyAnimationType, true);
    }


    private void Update() {

        CheckIfPlayerFinishedAttackAnimation();

        if (CheckAttackingAnimationProgress(.7f)) return;
        // Update movedir around 2/3 of the attack animation

        Vector2 moveDir = PlayerMovement.Instance.GetMovementVectorNormalized();
        if (moveDir != Vector2.zero) {
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }

        HandleLastMoveDir(moveDir); 
        
        if (attacking) return;
        // Don't allow player to change watch dir during attack animation
        Vector2 watchDir = PlayerMovement.Instance.GetWatchVectorNormalized();

        if (watchDir != Vector2.zero) {
            animator.SetFloat("X", watchDir.x);
            animator.SetFloat("Y", watchDir.y);
        } else {
            animator.SetFloat("X", lastMoveDir.x);
            animator.SetFloat("Y", lastMoveDir.y);
        }
    }

    private void CheckIfPlayerFinishedAttackAnimation() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Thrust") | animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slash") | animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_CutTree")) {
            // Attack animation is playing
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < .7f) {
                attacking = true;
            }
            else {
                if(attacking) {
                    PlayerAttack.Instance.SetAttackEnded();
                    attacking = false;
                }
            }
        }

        else {
            if (attacking) {
                PlayerAttack.Instance.SetAttackEnded();
                attacking = false;
            }
        }

        PlayerAttack.Instance.SetAttacking(attacking);
    }

    public bool CheckAttackingAnimationProgress(float animationProgress) {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Thrust") | animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Slash") | animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_CutTree")) {
            // Attack animation is playing
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < animationProgress) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private void HandleLastMoveDir(Vector2 moveDir) {
        if (moveDir == Vector2.zero) {
            moveDir = lastMoveDir;
        }

        lastMoveDir = moveDir;
        if (lastMoveDir.y == 0) {
            lastMoveDir.y = -0.01f;
        }
        if (lastMoveDir.x == 0) {
            lastMoveDir.x = 0.01f;
        }
    }

    private void PlayerAttack_OnPlayerAttack(object sender, System.EventArgs e) {
        animator.SetTrigger("Attack");
    }


    private void Player_OnIDamageableHealthChanged(object sender, IDamageable.OnIDamageableHealthChangedEventArgs e) {
        bodyAnimator.SetTrigger("Damaged");
    }

    private void PlayerAttack_OnActiveWeaponSOChanged(object sender, EventArgs e) {
        animator.SetBool(bodyAnimationType, false);
        bodyAnimationType = PlayerAttack.Instance.GetActiveWeaponSO().bodyAnimationType.ToString();
        animator.SetBool(bodyAnimationType, true);
    }

    public Vector2 GetLastMoveDir() {
        return lastMoveDir;
    }
}
