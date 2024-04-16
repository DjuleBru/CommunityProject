using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidInteraction : MonoBehaviour, IInteractable {

    [SerializeField] private GameObject hoveredGameObject;
    [SerializeField] private Collider2D solidCollider;

    private void Awake() {
        solidCollider = GetComponent<Collider2D>();
        hoveredGameObject.SetActive(false);
    }

    public void ClosePanel() {
    }

    public Collider2D GetSolidCollider() {
        return solidCollider;
    }

    public void SetHovered(bool hovered) {
        hoveredGameObject.SetActive(hovered);
    }

    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {

    }
}
