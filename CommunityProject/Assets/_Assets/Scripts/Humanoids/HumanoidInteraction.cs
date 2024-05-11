using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidInteraction : MonoBehaviour, IInteractable {

    [SerializeField] private GameObject hoveredGameObject;
    [SerializeField] private Collider2D solidCollider;

    private bool playerInTriggerArea;
    private bool hovered;

    private Humanoid humanoid;
    private void Awake() {
        solidCollider = GetComponent<Collider2D>();
        hoveredGameObject.SetActive(false);
        humanoid = GetComponentInParent<Humanoid>();
    }

    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if(hovered) {
            HumanoidUI.Instance.SetHumanoid(humanoid);

            if(!HumanoidUI.Instance.GetPanelOpen()) {
                HumanoidUI.Instance.OpenPanel();
            }
        }
    }

    public void ClosePanel() {
        //HumanoidUI.Instance.ClosePanel();
    }

    public Collider2D GetSolidCollider() {
        return solidCollider;
    }

    public void SetHovered(bool hovered) {
        hoveredGameObject.SetActive(hovered);
        this.hovered = hovered;
    }

    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        this.playerInTriggerArea = playerInTriggerArea;
    }

    public bool GetPlayerInTriggerArea() {
        return playerInTriggerArea;
    }

}
