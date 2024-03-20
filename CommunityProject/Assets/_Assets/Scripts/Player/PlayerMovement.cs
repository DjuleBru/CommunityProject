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


    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        HandleMovement();
        HandleWatchDir();
    }

    private void HandleWatchDir() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = Player.Instance.transform.position;

        watchDirNormalized = (mousePos - playerPos).normalized;
    }

    private void HandleMovement() {
        Vector2 force = Vector2.zero;

        if (!PlayerAttack.Instance.GetAttacking()) {
            // If Player is not attacking, move

            Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

            moveDirNormalized = new Vector2(inputVector.x, inputVector.y).normalized;

            force = moveDirNormalized * moveSpeed * Time.fixedDeltaTime;
        }
        rb.velocity = force;
    }

    public Vector2 GetMovementVectorNormalized() {
        return moveDirNormalized;
    }

    public Vector2 GetWatchVectorNormalized() {
        return watchDirNormalized;
    }

}
