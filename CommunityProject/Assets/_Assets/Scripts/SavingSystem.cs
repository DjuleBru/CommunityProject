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
        ES3.Save("playerExitedDungeon", false);

        if (SceneManager.GetActiveScene().name == SceneLoader.Scene.OverWorld.ToString()) {
            sceneIsOverWorld = true;
        }
        if (SceneManager.GetActiveScene().name == SceneLoader.Scene.Dungeon.ToString()) {
            sceneIsDungeon = true;
        }

    }

    private void Start() {
        if (playerExitedDungeon) {
            lastDungeonEntrance = ES3.Load("dungeonEntrance", lastDungeonEntrance);

            List<Item> lastDungeonLootedItems = GetLastDungeonLootedItems();
            float timeToCompleteDungeon = ES3.Load("lastDungeonTime", 0f);

            if (!lastDungeonEntrance.GetDungeonComplete()) {
                lastDungeonEntrance.SetDungeonAsComplete();
                lastDungeonEntrance.RecordDungeon(lastDungeonLootedItems, timeToCompleteDungeon);
            } else {
                lastDungeonEntrance.RecordLastRun(lastDungeonLootedItems, timeToCompleteDungeon);
            }

            lastDungeonEntrance.AddItemsToInventory(lastDungeonLootedItems);
            Player.Instance.transform.position = lastDungeonEntrance.transform.position;
        }

    }

    public void SetLastDungeonEntrance(DungeonEntrance dungeonEntrance) {
        ES3.Save("dungeonEntrance", dungeonEntrance);
    }

    public void CompleteDungeon(List<Item> itemList, float timeToCompleteDungeon) {
        ES3.Save("lastDungeonItemList", itemList);
        ES3.Save("lastDungeonTime", timeToCompleteDungeon);
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
    }

    public void OnApplicationQuit() {
        if(sceneIsOverWorld) {
            SaveOverworld();
        }
    }
}
