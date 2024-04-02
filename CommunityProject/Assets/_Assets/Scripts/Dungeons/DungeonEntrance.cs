using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    bool playerIsInEntranceArea;
    [SerializeField] private GameObject dungeonEntranceUI;
    [SerializeField] private GameObject enterUI;
    [SerializeField] private DungeonSO dungeonSO;

    [SerializeField] private Transform exitDungeonSpawnPoint;
    [SerializeField] private Chest dungeonChest;
    [SerializeField] private DungeonStatsBoard dungeonStatsBoard;

    [SerializeField] private bool dungeonIsComplete;

    private void Start() {
        dungeonEntranceUI.SetActive(false);
        enterUI.SetActive(false);

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();

        if (player != null) {
            playerIsInEntranceArea = true;
            dungeonEntranceUI.SetActive(true);
            enterUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();

        if (player != null) {
            playerIsInEntranceArea = false;
            dungeonEntranceUI.SetActive(false);
            enterUI.SetActive(false);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (playerIsInEntranceArea) {
            SavingSystem.Instance.SetLastDungeonEntrance(this);
            SceneLoader.Load(SceneLoader.Scene.Dungeon);
        }
    }

    public DungeonSO GetDungeonSO() {
        return dungeonSO;
    }

    public void RecordDungeon(List<Item> loot, float time) {
        dungeonStatsBoard.RecordDungeon(loot, time);
        SaveDungeon();
    }

    public void RecordLastRun(List<Item> loot, float time) {
        dungeonStatsBoard.RecordLastRun(loot, time);
        SaveDungeon();
    }

    public void AddItemsToInventory(List<Item> itemList) {
        dungeonChest.AddItemsToChest(itemList);
    }

    public void SetDungeonAsComplete() {
        dungeonIsComplete = true;
        dungeonChest.gameObject.SetActive(true);
        dungeonStatsBoard.gameObject.SetActive(true);

        SaveDungeon();
    }

    public bool GetDungeonComplete() {
        return dungeonIsComplete;
    }

    public void SaveDungeon() {
        ES3AutoSaveMgr.Current.Save();
    }

}
