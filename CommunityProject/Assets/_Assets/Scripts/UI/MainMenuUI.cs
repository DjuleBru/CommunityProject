using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUIGameObject;
    [SerializeField] private GameObject settingsUIGameIObject;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitGameButton;

    [SerializeField] private TextMeshProUGUI newGameText;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<VideoClip> videoClipList;

    private float videoClipDuration;
    private float videoClipTimer;

    private bool newGameClickedOnce;

    private void Awake() {
        settingsUIGameIObject.SetActive(false);

        if (!ES3.Load("gameExists", false)) {
            continueButton.interactable = false;
        }

        continueButton.onClick.AddListener(() => {
            SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.Scene.OverWorld);
        });

        newGameButton.onClick.AddListener(() => {

            TryNewGame();
        });

        settingsButton.onClick.AddListener(() => {
            settingsUIGameIObject.SetActive(true);
            CancelNewGame();
        });

        exitGameButton.onClick.AddListener(() => {

            Application.Quit();
        });

    }



    private void Update() {

        videoClipTimer += Time.deltaTime;
        if(videoClipTimer >= videoClipDuration) {
            ChangeVideoClip();
        }

        if(Input.GetMouseButtonDown(1)) {
            CancelNewGame();
        }
    }

    private void ChangeVideoClip() {
        int currentClipIndex = videoClipList.IndexOf(videoPlayer.clip);

        int nextClipIndex = currentClipIndex;

        while(nextClipIndex == currentClipIndex) {
            nextClipIndex = Random.Range(0, videoClipList.Count);
        }

        videoPlayer.clip = videoClipList[nextClipIndex];
        videoClipDuration = (float)videoClipList[nextClipIndex].length;
        videoClipTimer = 0;
    }

    private void TryNewGame() {

        if(System.IO.File.Exists(Application.persistentDataPath + "/SaveFile.es3")) {

            if (newGameClickedOnce) {

                if (System.IO.File.Exists(Application.persistentDataPath + "/SaveFile.es3")) {
                    File.Delete(Application.persistentDataPath + "/SaveFile.es3");
                };

                continueButton.interactable = false;
            }
            else {
                newGameClickedOnce = true;
                newGameText.text = "Are you sure ?";
            }

        } else {
            SceneTransitionManager.Instance.LoadScene(SceneTransitionManager.Scene.OverWorld);
        }

    }

    private void CancelNewGame() {
        newGameClickedOnce = false;
        newGameText.text = "New Game";
    }
}
