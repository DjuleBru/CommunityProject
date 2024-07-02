using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    public enum Scene {
        MainMenuScene,
        LoadingScene,
        OverWorld,
        Dungeon,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene) {

        SceneLoader.targetScene = targetScene;
        SceneManager.LoadScene(targetScene.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }


}
