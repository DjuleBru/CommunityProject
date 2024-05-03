using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour
{


    public static BuildUI Instance { get; private set; }

    [SerializeField] private GameObject buildHotbar;

    private bool buildHotBarOpen;

    private void Awake() {
        Instance = this;
        buildHotbar.SetActive(false);
    }

    public void OpenCloseBuildHotbar() {
        buildHotBarOpen = !buildHotBarOpen;
        buildHotbar.SetActive(buildHotBarOpen);

        if(buildHotBarOpen ) {
            HumanoidsMenuUI.Instance.CloseHumanoidsMenu();
        }
    }

    public void CloseBuildHotbar() {
        buildHotBarOpen = false;
        buildHotbar.SetActive(buildHotBarOpen);
    }

}
