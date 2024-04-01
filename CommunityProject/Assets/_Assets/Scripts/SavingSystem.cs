using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour {
    public static SavingSystem Instance { get; private set; }

    [SerializeField] private DungeonEntrance dungeonEntrance;

    private bool sceneIsOverWorld;

    private void Awake() {

        Instance = this;

        if (SceneManager.GetActiveScene().name == SceneLoader.Scene.OverWorld.ToString()) {
            sceneIsOverWorld = true;
        }

    }

    private void Start() {
        if (sceneIsOverWorld) {
            dungeonEntrance = ES3.Load("dungeonEntrance", dungeonEntrance);
            Player.Instance.transform.position = dungeonEntrance.transform.position;
        }
    }

    public void SetLastDungeonEntrance(DungeonEntrance dungeonEntrance) {
        ES3.Save("dungeonEntrance", dungeonEntrance);
    }

    public bool GetSceneIsOverworld() {
        return sceneIsOverWorld;
    }
}
