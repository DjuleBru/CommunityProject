using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>() == null) return;
        DungeonManager.Instance.CompleteDungeon();
        SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.Scene.OverWorld);
    }
}
