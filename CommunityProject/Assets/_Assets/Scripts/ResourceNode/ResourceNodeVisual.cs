using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer shadowSpriteRenderer;
    [SerializeField] private Sprite emptyResourceNodeSprite;
    private ResourceNode resourceNode;

    private void Awake() {
        resourceNode = GetComponentInParent<ResourceNode>();
    }

    private void Start() {
        resourceNode.OnResourceNodeDepleted += ResourceNode_OnResourceNodeDepleted;
    }

    private void ResourceNode_OnResourceNodeDepleted(object sender, System.EventArgs e) {
        spriteRenderer.sprite = emptyResourceNodeSprite;
        shadowSpriteRenderer.sprite = null;
    }
}
