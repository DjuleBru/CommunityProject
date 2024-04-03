using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private GameObject buildHotbar;

    private bool buildHotBarOpen;

    private void Awake() {
        buildHotbar.SetActive(false);
    }

    public void OpenCloseBuildHotbar() {
        buildHotBarOpen = !buildHotBarOpen;
        buildHotbar.SetActive(buildHotBarOpen);
    }

}
