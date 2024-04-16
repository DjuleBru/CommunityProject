using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{

    public static OverworldCamera Instance { get; private set; }

    private CinemachineVirtualCamera v_camera;

    private void Awake() {
        Instance = this;
        v_camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void DeActivatePlayerCamera() {
        v_camera.enabled = false;
    }

    public void ActivatePlayerCamera() {
        v_camera.enabled = true;
    }
}
