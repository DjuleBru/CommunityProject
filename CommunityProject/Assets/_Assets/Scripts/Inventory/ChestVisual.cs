using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVisual : MonoBehaviour {

    private Chest chest;
    [SerializeField] private SpriteRenderer chestRenderer;
    [SerializeField] private SpriteRenderer chestShadowRenderer;

    [SerializeField] private GameObject chestHoveredVisual;

    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite closedSprite;

    [SerializeField] private Sprite openedSpriteShadow;
    [SerializeField] private Sprite closedSpriteShadow;

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

    public void SetChestHovered(bool hovered) {
        chestHoveredVisual.SetActive(hovered);
    }

    public void OpenChestVisual() {
        chestRenderer.sprite = openedSprite;
        chestShadowRenderer.sprite = openedSpriteShadow;
    }

    public void CloseChestVisual() {
        chestRenderer.sprite = closedSprite;
        chestShadowRenderer.sprite = closedSpriteShadow;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();

        if (player != null) {
            playerInTriggerArea = true;
            SetChestHovered(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();

        if (player != null) {
            playerInTriggerArea = false;
            SetChestHovered(false);
            CloseChestVisual();
            chest.CloseInventory();
            chestOpen = false;
        }
    }

}
