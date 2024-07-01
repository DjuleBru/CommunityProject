using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
    public enum DungeonType {
        GreenForest,
        AutumnForest,
        Cave,
        Swamp,
        Desert,

    }

    private DungeonType dungeonType;

    public static DungeonManager Instance { get; private set; }

    private DungeonSO dungeonSO;
    [SerializeField] private DungeonSO defaultDungeonSO;

    private int totalRoomNumber;
    private int dungeonDifficultyValue;

    [SerializeField] private List<MobSO> completeMobList;
    private List<MobSO> dungeonMobList = new List<MobSO>();

    [SerializeField] private List<GameObject> humanoidsCagesList;
    private GameObject humanoidSpawnedInDungeon;

    private List<GameObject> resourceNodesInDungeon;
    private int resourceNodesNumberInDungeon;
    private int humanoidCageNumberInDungeon;

    [SerializeField] private InventoryUI dungeonInventoryUI;
    private Inventory dungeonInventory;

    private int humanoidsSaved;
    private float dungeonTimer;

    private void Awake() {
        Instance = this;
        dungeonSO = ES3.Load("lastDungeonEnteredDungeonSO", defaultDungeonSO);
        dungeonType = dungeonSO.dungeonType;
        FillCompleteMobList();

        dungeonMobList = dungeonSO.mobsFoundInDungeon;
        resourceNodesInDungeon = dungeonSO.resourceNodesInDungeon;
        resourceNodesNumberInDungeon = dungeonSO.resourceNodesNumberInDungeon;
        humanoidCageNumberInDungeon = ES3.Load("lastDungeonEnteredRemainingHumanoidsToSave", 0);
        totalRoomNumber = dungeonSO.totalRoomNumber;
        dungeonDifficultyValue = dungeonSO.dungeonDifficulty;
        humanoidSpawnedInDungeon = dungeonSO.humanoidTypeFoundInDungeon.humanoidPrefab;
    }

    private void Start() {
        dungeonInventory = new Inventory(true, 8, 3, false, null);
        dungeonInventoryUI.SetInventory(dungeonInventory);
    }

    private void Update() {
        dungeonTimer += Time.deltaTime;
    }

    private void FillCompleteMobList() {
        foreach(MobSO mobSO in completeMobList) {
            if(mobSO.foundInDungeonTypes.Contains(dungeonType)) {
                dungeonMobList.Add(mobSO);
            }
        }
    }

    public void SetHumanoidsAsSaved() {
        humanoidsSaved++;
    }

    public void CompleteDungeon() {
        SavingSystem.Instance.CompleteDungeon(dungeonInventory.GetItemList(), dungeonTimer, humanoidsSaved);
    }

    public List<MobSO> GetDungeonMobList() {
        return dungeonMobList;
    }

    public List<GameObject> GetResourceNodesList() {
        return resourceNodesInDungeon;
    }

    public List<GameObject> GetHumanoidCagesList() {
        return humanoidsCagesList;
    }

    public int GetDungeonDifficultyValue() {
        return dungeonDifficultyValue;
    }

    public int GetResourceNumberInDungeon() {
        return resourceNodesNumberInDungeon;
    }

    public int GetHumanoidCageNumberInDungeon() {
        return humanoidCageNumberInDungeon;
    }

    public GameObject GetHumanoidSpawnedInDungeon() {
        return humanoidSpawnedInDungeon;
    }

    public int GetTotalRoomNumber() {
        return totalRoomNumber;
    }

    public Inventory GetDungeonInventory() {
        return dungeonInventory;
    }

    public DungeonSO GetDungeonSO() {
        return dungeonSO;
    }
}