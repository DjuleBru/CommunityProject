using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [SerializeField] private Texture2D defaultBuildingTexture2D;
    [SerializeField] private Texture2D destroyBuildingTexture2D;

    private Vector2 cursorHotspot;

    private void Awake() {
        Instance = this;
    }

    public void SetDestroyBuildingCursor() {
        Cursor.SetCursor(destroyBuildingTexture2D, Vector2.zero, CursorMode.Auto); 
    }

    public void ResetCursor() {
        Cursor.SetCursor(defaultBuildingTexture2D, Vector2.zero, CursorMode.Auto);
    }

}
