using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance { get; private set; }

    private Rigidbody2D rb;
    private Vector2 moveDirNormalized;
    private Vector2 watchDirNormalized;

    [SerializeField] private float moveSpeed;
    private bool canMove = true;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!canMove || Player.Instance.GetPlayerWorking()) return;

        if (!PlayerAnimatorManager.Instance.CheckAttackingAnimationProgress(.5f)) {
            HandleMovementInput();
        } else {
            moveDirNormalized = Vector2.zero;
        }

        HandleMovement();

        if (!PlayerAttack.Instance.GetAttacking()) {
            HandleWatchDir();
        }
    }

    private void HandleWatchDir() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = Player.Instance.transform.position;

        watchDirNormalized = (mousePos - playerPos).normalized;
    }

    private void HandleMovementInput() {
        // If Player is not attacking, move

        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        moveDirNormalized = new Vector2(inputVector.x, inputVector.y).normalized;
    }

    private void HandleMovement() {
        Vector2 force = moveDirNormalized * moveSpeed * Time.fixedDeltaTime;
        rb.velocity = force;
    }

    public Vector2 GetMovementVectorNormalized() {
        return moveDirNormalized;
    }

    public Vector2 GetWatchVectorNormalized() {
        return watchDirNormalized;
    }

    public void DisableMovement() {
        rb.velocity = Vector2.zero;
        moveDirNormalized = Vector2.zero;
        canMove = false;
    }

    public void EnableMovement() {
        canMove = true;
    }
}
