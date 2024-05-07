using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeInteraction : MonoBehaviour, IInteractable {

    [SerializeField] protected Collider2D solidCollider;
    [SerializeField] protected GameObject hoveredGameObject;
    protected ResourceNode resourceNode;
    protected bool playerInTriggerArea;
    protected bool resourceNodeDepleted;

    protected void Awake() {
        hoveredGameObject.SetActive(false);
        resourceNode = GetComponentInParent<ResourceNode>();
    }

    protected void Start() {
        PlayerAttack.Instance.OnPlayerAttackEnded += PlayerAttack_OnPlayerAttackEnded;
        resourceNode.OnResourceNodeDepleted += ResourceNode_OnResourceNodeDepleted;
    }

    protected void ResourceNode_OnResourceNodeDepleted(object sender, System.EventArgs e) {
        resourceNodeDepleted = true;
        hoveredGameObject.SetActive(false);
    }

    protected void PlayerAttack_OnPlayerAttackEnded(object sender, System.EventArgs e) {
        if (playerInTriggerArea) {
            HitResourceNode();
        }
    }

    protected void HitResourceNode() {
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
