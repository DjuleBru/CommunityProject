using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeInteraction : MonoBehaviour, IInteractable {

    [SerializeField] private Collider2D solidCollider;
    [SerializeField] private GameObject hoveredGameObject;
    private ResourceNode resourceNode;
    private bool playerInTriggerArea;
    private bool resourceNodeDepleted;

    private void Awake() {
        hoveredGameObject.SetActive(false);
        resourceNode = GetComponentInParent<ResourceNode>();
    }

    private void Start() {
        PlayerAttack.Instance.OnPlayerAttackEnded += PlayerAttack_OnPlayerAttackEnded;
        resourceNode.OnResourceNodeDepleted += ResourceNode_OnResourceNodeDepleted;
    }

    private void ResourceNode_OnResourceNodeDepleted(object sender, System.EventArgs e) {
        resourceNodeDepleted = true;
        hoveredGameObject.SetActive(false);
    }

    private void PlayerAttack_OnPlayerAttackEnded(object sender, System.EventArgs e) {
        if (playerInTriggerArea) {
            HitResourceNode();
        }
    }

    private void HitResourceNode() {
        if (resourceNodeDepleted) return;
        resourceNode.HitResourceNode();
    }

    public void ClosePanel() {
    }

    public bool GetPlayerInTriggerArea() {
        return playerInTriggerArea;
    }

    public Collider2D GetSolidCollider() {
        return solidCollider;
    }

    public void SetHovered(bool hovered) {
        if (resourceNodeDepleted) return;
        hoveredGameObject.SetActive(hovered);
    }

    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        this.playerInTriggerArea = playerInTriggerArea;
    }

}
