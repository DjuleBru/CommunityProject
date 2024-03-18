using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance { get; private set; }

    private Rigidbody2D rb;
    private Vector2 moveDir;

    [SerializeField] private float moveSpeed;


    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        moveDir = new Vector2(inputVector.x,inputVector.y);

        Vector2 force = moveDir * moveSpeed;
        rb.velocity = force;
    }

    public Vector2 GetMovementVectorNormalized() {
        return moveDir;
    }

}
