using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    private SpriteRenderer weaponSpriteRenderer;
    private Animator animator;

    [SerializeField] private Transform WeaponHoldPoint;

    private void Awake() {
        weaponSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        PlayerAttack.Instance.OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        PlayerAttack.Instance.OnActiveWeaponSOChanged += PlayerAttack_OnActiveWeaponSOChanged;

        UpdateWeaponVisuals();
    }

    private void Update() {
        //transform.position = WeaponHoldPoint.position;

        animator.SetFloat("X", PlayerAnimatorManager.Instance.GetLastMoveDir().x);
        animator.SetFloat("Y", PlayerAnimatorManager.Instance.GetLastMoveDir().y);
    }

    private void PlayerAttack_OnPlayerAttack(object sender, System.EventArgs e) {
        animator.SetTrigger("Attack");
    }

    private void PlayerAttack_OnActiveWeaponSOChanged(object sender, System.EventArgs e) {
        UpdateWeaponVisuals();
    }

    private void UpdateWeaponVisuals() {
        //weaponSpriteRenderer.sprite = PlayerAttack.Instance.GetActiveWeaponSO().weaponSprite;
        animator.runtimeAnimatorController = PlayerAttack.Instance.GetActiveWeaponSO().weaponAnimatorController;
    }

}
