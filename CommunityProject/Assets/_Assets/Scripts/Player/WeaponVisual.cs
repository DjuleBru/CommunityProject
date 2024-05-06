using AllIn1SpriteShader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisual : MonoBehaviour {
    public static WeaponVisual Instance { get; private set; }

    [SerializeField] private SpriteRenderer weaponIdleSpriteRenderer;
    private SpriteRenderer weaponAnimatorSpriteRenderer;

    private Animator animator;

    [SerializeField] private Transform WeaponHoldPoint;


    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
        weaponAnimatorSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        PlayerAttack.Instance.OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        PlayerAttack.Instance.OnActiveWeaponSOChanged += PlayerAttack_OnActiveWeaponSOChanged;

        UpdateWeaponVisuals();
    }

    private void Update() {
        HandleWeaponIdleVisual();
        HandleWeaponSortingLayer();

        if (PlayerAttack.Instance.GetAttacking()) return;
        if (animator.runtimeAnimatorController == null) return;
        Vector2 watchDir = PlayerMovement.Instance.GetWatchVectorNormalized();

        if (watchDir != Vector2.zero) {
            animator.SetFloat("X", watchDir.x);
            animator.SetFloat("Y", watchDir.y);
        }
        else {
            animator.SetFloat("X", PlayerAnimatorManager.Instance.GetLastMoveDir().x);
            animator.SetFloat("Y", PlayerAnimatorManager.Instance.GetLastMoveDir().y);
        }
    }

    private void HandleWeaponIdleVisual() {
        if(!PlayerAttack.Instance.GetAttacking()) {
            weaponIdleSpriteRenderer.enabled = true;
        } else {
            weaponIdleSpriteRenderer.enabled = false;
        }
    }

    private void HandleWeaponSortingLayer() {
        Vector2 moveDir = PlayerMovement.Instance.GetMovementVectorNormalized();
        Vector2 watchDir = PlayerMovement.Instance.GetWatchVectorNormalized();

        if(watchDir != Vector2.zero) {

            if(watchDir.y > 0) {
                weaponIdleSpriteRenderer.sortingOrder = -1;
            } else {
                weaponIdleSpriteRenderer.sortingOrder = 1;
            }

        } else {
            if (moveDir.y > 0) {
                weaponIdleSpriteRenderer.sortingOrder = -1;
            }
            else {
                weaponIdleSpriteRenderer.sortingOrder = 1;
            }
        }
    }

    private void PlayerAttack_OnPlayerAttack(object sender, System.EventArgs e) {
        if (animator.runtimeAnimatorController == null) return;
        animator.SetTrigger("Attack");
    }

    private void PlayerAttack_OnActiveWeaponSOChanged(object sender, System.EventArgs e) {
        UpdateWeaponVisuals();
    }

    private void UpdateWeaponVisuals() {
        animator.runtimeAnimatorController = PlayerAttack.Instance.GetActiveWeaponSO().weaponAnimatorController;
        weaponIdleSpriteRenderer.sprite = PlayerAttack.Instance.GetActiveWeaponSO().weaponSprite;
    }

}
