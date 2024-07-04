using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    [SerializeField] private Animator transitionAnimator;

    public enum Scene {
        MainMenu,
        LoadingScene,
        OverWorld,
        Dungeon,
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        if(!(SceneManager.GetActiveScene().name == Scene.MainMenu.ToString())) {
            transitionAnimator.SetTrigger("End");
        }

    }

    public void TriggerTransition() {
        transitionAnimator.SetTrigger("Start");
    }

    public void LoadScene(Scene targetScene) {
        StartCoroutine(LoadLevel(targetScene));
    }

    private IEnumerator LoadLevel(Scene targetScene) {
        TriggerTransition();
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(targetScene.ToString());
    }
}
