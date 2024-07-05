using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    private bool open;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button saveButton;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private bool dungeonScene;

    public static event EventHandler OnPauseMenuOpen;
    public static event EventHandler OnPauseMenuClosed;

    private void Awake() {
        pauseMenuPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);

        settingsButton.onClick.AddListener(() => {
            settingsPanel.gameObject.SetActive(true);
        });

        exitGameButton.onClick.AddListener(() => {
            Application.Quit();
        });

        saveButton.onClick.AddListener(() => {
            if(dungeonScene) {
                SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.Scene.OverWorld);
            } else {
                SavingSystem.Instance.SaveOverworld();
            }
        });

        backToMenuButton.onClick.AddListener(() => {
            SavingSystem.Instance.SaveOverworld();
            SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.Scene.MainMenu);
        });
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            open = !open;
            pauseMenuPanel.gameObject.SetActive(open);
        }
    }
}
