using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        SceneLoader.Load(SceneLoader.Scene.OverWorld);
    }
}
