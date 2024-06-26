using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyBuildingButton : MonoBehaviour
{
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            FreeCameraViewManager.Instance.SetFreeCamera(true);
            FreeCameraViewManager.Instance.SetDestroyBuilding();
            CursorManager.Instance.SetDestroyBuildingCursor();
        });
    }
}
