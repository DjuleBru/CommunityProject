using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVisual : MonoBehaviour, IInteractable {

    private Chest chest;
    [SerializeField] private SpriteRenderer chestRenderer;
    [SerializeField] private SpriteRenderer chestShadowRenderer;

    [SerializeField] private GameObject chestHoveredVisual;

    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;

    [SerializeField] private Sprite openedSpriteShadow;
    [SerializeField] private Sprite closedSpriteShadow;

    [SerializeField] private Collider2D solidChestCollider;

    private bool chestOpen;
    private bool playerInTriggerArea;
    private void Awake() {
        chest = GetComponentInParent<Chest>();

        chestHoveredVisual.SetActive(false);
        chestOpen = false;

        chestRenderer.sprite = closedSprite;
        chestShadowRenderer.sprite = closedSpriteShadow;
    }

    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (playerInTriggerArea) {
            if (chestOpen) {
                chest.CloseInventory();
                CloseChestVisual();
                chestOpen = false;
            } else {
                chest.OpenInventory();
                OpenChestVisual();
                chestOpen = true;
            }
        }
    }

    public void OpenChestVisual() {
        chestRenderer.sprite = openedSprite;
        chestShadowRenderer.sprite = openedSpriteShadow;
    }

    public void CloseChestVisual() {
        chestRenderer.sprite = closedSprite;
        chestShadowRenderer.sprite = closedSpriteShadow;
    }

    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        this.playerInTriggerArea = playerInTriggerArea;

    }

    public void SetHovered(bool hovered) {
        chestHoveredVisual.SetActive(hovered);
    }

    public void ClosePanel() {
        CloseChestVisual();
        chest.CloseInventory();
        chestOpen = false;
    }


    public Collider2D GetSolidCollider() {
        return solidChestCollider;
    }
}
