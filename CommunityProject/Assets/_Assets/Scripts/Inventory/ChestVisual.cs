using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVisual : BuildingVisual, IInteractable {

    protected Chest chest;
    [SerializeField] protected SpriteRenderer chestRenderer;
    [SerializeField] protected SpriteRenderer chestShadowRenderer;

    [SerializeField] protected GameObject chestHoveredVisual;

    [SerializeField] protected Sprite openedSprite;
    [SerializeField] protected Sprite closedSprite;

    [SerializeField] protected Sprite openedSpriteShadow;
    [SerializeField] protected Sprite closedSpriteShadow;

    [SerializeField] protected Collider2D solidChestCollider;

    protected bool chestOpen;
    protected void Awake() {
        chest = GetComponentInParent<Chest>();

        chestHoveredVisual.SetActive(false);
        chestOpen = false;

        chestRenderer.sprite = closedSprite;
        chestShadowRenderer.sprite = closedSpriteShadow;
    }

    protected override void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    protected override void GameInput_OnInteractAction(object sender, System.EventArgs e) {
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

    public override void ClosePanel() {
        CloseChestVisual();
        chest.CloseInventory();
        chestOpen = false;
    }
}
