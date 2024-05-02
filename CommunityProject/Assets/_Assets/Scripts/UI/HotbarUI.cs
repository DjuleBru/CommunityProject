using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarUI : MonoBehaviour
{

    public static HotbarUI Instance { get; private set; }

    [SerializeField] private GameObject hotBarUIGameObject;

    private bool hotBarOpen;

    private void Awake() {
        Instance = this;
    }

    public void SetHotbarActive(bool hotBarActive) {
        hotBarOpen = hotBarActive;

        if (!hotBarOpen) {
            hotBarUIGameObject.SetActive(false);
        } else {
            hotBarUIGameObject.SetActive(true);
        }
    }

}
