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
    [SerializeField] private Collider2D dungeonEntranceColliderForDungeoneers;

    private List<Humanoid> humanoidsAssigned = new List<Humanoid>();

    private void Start() {
        LoadDungeon();

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
            HumanoidsManager.Instance.SaveHumanoidsInOverworld();
            SavingSystem.Instance.SetLastDungeonEntrance(this);
            SavingSystem.Instance.SaveOverworld();
            SceneLoader.Load(SceneLoader.Scene.Dungeon);
        }
    }

    public DungeonSO GetDungeonSO() {
        return dungeonSO;
    }

    public void RecordDungeon(List<Item> loot, float time, int humanoidsSaved) {
        dungeonStatsBoard.RecordDungeon(loot, time, humanoidsSaved);
        SaveDungeon();
    }

    public void RecordLastRun(List<Item> loot, float time, int humanoidsSaved) {
        dungeonStatsBoard.RecordLastRun(loot, time, humanoidsSaved);
    }

    public void AddItemsToInventory(List<Item> itemList) {
        Debug.Log("adding items to dungeon chest");
        dungeonChest.AddItemsToChest(itemList);
    }

    public void SetDungeonAsComplete() {
        dungeonIsComplete = true;
        dungeonChest.gameObject.SetActive(true);
        dungeonStatsBoard.gameObject.SetActive(true);
    }

    public bool GetDungeonComplete() {
        return dungeonIsComplete;
    }

    public void AssignHumanoid(Humanoid humanoid) {
        if(humanoidsAssigned.Contains(humanoid)) return;
        humanoidsAssigned.Add(humanoid);
    }

    public void DeAssignHumanoid(Humanoid humanoid) {
        if(humanoidsAssigned.Contains(humanoid)) {
            humanoidsAssigned.Remove(humanoid);
        }
    }

    public List<Humanoid> GetHumanoidsAssigned() {
        return humanoidsAssigned;
    }

    public float GetDungeonTimerRecorded() {
        return dungeonStatsBoard.GetDungeonTime();
    }

    public List<Item> GetDungeonLootRecorded() {
        return dungeonStatsBoard.GetDungeonLoot();
    }

    public DungeonStatsBoard GetDungeonStatsBoard() {
        return dungeonStatsBoard;
    }

    public Chest GetDungeonChest() {
        return dungeonChest;
    }

    public Collider2D GetColliderForDungeoneers() {
        return dungeonEntranceColliderForDungeoneers;
    }

    public void SaveDungeon() {
        DungeonEntranceManager.Instance.SaveDungeonEntrance(this);
    }

    public void LoadDungeon() {
        dungeonStatsBoard.LoadStatsBoardUI();

        if(dungeonStatsBoard.GetRecordedDungeonTime() != 0) {
            dungeonChest.gameObject.SetActive(true);
            dungeonIsComplete = true;
            Debug.Log(dungeonIsComplete);
        }

    }

}
