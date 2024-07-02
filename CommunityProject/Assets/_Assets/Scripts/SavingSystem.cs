using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour {
    public static SavingSystem Instance { get; private set; }

    [SerializeField] private DungeonEntrance lastDungeonEntrance;

    private bool sceneIsOverWorld;
    private bool sceneIsDungeon;

    private bool playerExitedDungeon;

    private void Awake() {

        Instance = this;

        // Did the player just exit a dungeon ?
        playerExitedDungeon = ES3.Load("playerExitedDungeon", false);

        if (SceneManager.GetActiveScene().name == SceneTransitionManager.Scene.OverWorld.ToString()) {
            sceneIsOverWorld = true;
        }
        if (SceneManager.GetActiveScene().name == SceneTransitionManager.Scene.Dungeon.ToString()) {
            sceneIsDungeon = true;
        }
    }

    private void Start() {
        LoadLastDungeonData();
    }

    private void LoadLastDungeonData() {
        if (playerExitedDungeon) {
            lastDungeonEntrance = ES3.Load("lastDungeonEntered", lastDungeonEntrance);

            List<Item> lastDungeonLootedItems = GetLastDungeonLootedItems();
            float timeToCompleteDungeon = ES3.Load("lastDungeonTime", 0f);
            int humanoidsSaved = ES3.Load("lastDungeonHumanoidsSaved", 0);

            if (!lastDungeonEntrance.GetDungeonComplete()) {
                lastDungeonEntrance.SetDungeonAsComplete();
                lastDungeonEntrance.RecordDungeon(lastDungeonLootedItems, timeToCompleteDungeon, humanoidsSaved);
            }
            else {
                lastDungeonEntrance.RecordLastRun(lastDungeonLootedItems, timeToCompleteDungeon, humanoidsSaved);
            }

            lastDungeonEntrance.AddItemsToInventory(lastDungeonLootedItems);
            Player.Instance.transform.position = lastDungeonEntrance.transform.position;

            ES3.Save("playerExitedDungeon", false);
        }
    }

    public void SetLastDungeonEntrance(DungeonEntrance dungeonEntrance) {
        ES3.Save("lastDungeonEntered", dungeonEntrance);
        ES3.Save("lastDungeonEnteredDungeonSO", dungeonEntrance.GetDungeonSO());

        int humanoidsToSave = dungeonEntrance.GetDungeonSO().humanoidsToSaveFromDungeon - dungeonEntrance.GetDungeonStatsBoard().GetRecordedHumanoidsNumberSaved();
        ES3.Save("lastDungeonEnteredRemainingHumanoidsToSave", humanoidsToSave);
    }

    public void CompleteDungeon(List<Item> itemList, float timeToCompleteDungeon, int humanoidsNumberSaved) {
        ES3.Save("lastDungeonItemList", itemList);
        ES3.Save("lastDungeonTime", timeToCompleteDungeon);
        ES3.Save("lastDungeonHumanoidsSaved", humanoidsNumberSaved);
        ES3.Save("playerCompletedLastDungeon", true);
        ES3.Save("playerExitedDungeon", true);

        HumanoidsManager.Instance.SaveHumanoidsSavedFromDungeon();
    }

    public List<Item> GetLastDungeonLootedItems() {
        return ES3.Load("lastDungeonItemList", new List<Item>());
    }

    public bool GetSceneIsOverworld() {
        return sceneIsOverWorld;
    }

    public bool GetSceneIsDungeon() {
        return sceneIsDungeon;
    }

    public void SaveOverworld() {
        HumanoidsManager.Instance.SaveHumanoidsInOverworld();
        BuildingsManager.Instance.SaveBuildingsInOverworld();
        ResearchMenuUI.Instance.SaveResearch();
        Player.Instance.SavePlayer();
    }

    public void OnApplicationQuit() {
        if(sceneIsOverWorld) {
            SaveOverworld();
        }
    }
}
