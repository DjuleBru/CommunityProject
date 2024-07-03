using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnAttackStarted;
    public event EventHandler OnAttackCanceled;
    public event EventHandler OnPlaceBuilding;
    public event EventHandler OnPlaceBuildingCancelled;

    private PlayerInputActions playerInputActions;
    private void Awake() {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Attack.performed += Attack_performed;
        playerInputActions.Player.Attack.started += Attack_started;
        playerInputActions.Player.Attack.canceled += Attack_canceled;
        playerInputActions.Player.PlaceBuilding.performed += PlaceBuilding_performed;
        playerInputActions.Player.CancelPlaceBuilding.performed += CancelPlaceBuilding_performed;
    }

    private void Attack_canceled(InputAction.CallbackContext obj) {
        OnAttackCanceled?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetZoomVector() {
        Vector2 zoomInput = playerInputActions.Player.Zoom.ReadValue<Vector2>();

        return zoomInput;
    }

    private void CancelPlaceBuilding_performed(InputAction.CallbackContext obj) {
        OnPlaceBuildingCancelled?.Invoke(this, EventArgs.Empty);
    }

    private void PlaceBuilding_performed(InputAction.CallbackContext obj) {
        OnPlaceBuilding?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_started(InputAction.CallbackContext obj) {
        OnAttackStarted?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(InputAction.CallbackContext obj) {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Attack.performed -= Attack_performed;

        playerInputActions.Dispose();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }

}
