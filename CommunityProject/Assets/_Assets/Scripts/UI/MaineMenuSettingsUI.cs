using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaineMenuSettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsGameObject;
    [SerializeField] private Button backButton;

    private void Awake() {
        backButton.onClick.AddListener(() => {
            settingsGameObject.gameObject.SetActive(false);
        });

    }
}
