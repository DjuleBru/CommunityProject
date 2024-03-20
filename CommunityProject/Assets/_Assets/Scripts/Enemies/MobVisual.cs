using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bodySpriteRenderer;

    private void Update() {
        if(Player.Instance.transform.position.y < transform.position.y) {
            bodySpriteRenderer.sortingOrder = -2;
        } else {
            // Set sprite behind weapon
            bodySpriteRenderer.sortingOrder = 0;
        }
    }
}
