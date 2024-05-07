using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeVisual : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected SpriteRenderer shadowSpriteRenderer;
    [SerializeField] protected Sprite emptyResourceNodeSprite;
    protected ResourceNode resourceNode;

    protected void Awake() {
        resourceNode = GetComponentInParent<ResourceNode>();
    }

    protected void Start() {
        resourceNode.OnResourceNodeDepleted += ResourceNode_OnResourceNodeDepleted;
    }

    protected void ResourceNode_OnResourceNodeDepleted(object sender, System.EventArgs e) {
        spriteRenderer.sprite = emptyResourceNodeSprite;
        shadowSpriteRenderer.sprite = null;
    }
}
