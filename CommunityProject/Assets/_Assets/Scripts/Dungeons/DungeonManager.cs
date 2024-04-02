using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {
    public enum DungeonType {
        GreenForest,
        AutumnForest,
    }

    public DungeonType dungeonType;

    public static DungeonManager Instance { get; private set; }

    [SerializeField] private int totalRoomNumber;
    [SerializeField] private int dungeonDifficultyValue;

    [SerializeField] private List<MobSO> completeMobList;
    private List<MobSO> dungeonMobList = new List<MobSO>();


    [SerializeField] private InventoryUI dungeonInventoryUI;
    private Inventory dungeonInventory;

    private float dungeonTimer;

    private void Awake() {
        Instance = this;

        FillMobList();
    }

    private void Start() {
        dungeonInventory = new Inventory(false, 10, 3);
        dungeonInventoryUI.SetInventory(dungeonInventory);
    }

    private void Update() {
        dungeonTimer += Time.deltaTime;
    }

    private void FillMobList() {
        foreach(MobSO mobSO in completeMobList) {
            if(mobSO.foundInDungeonTypes.Contains(dungeonType)) {
                dungeonMobList.Add(mobSO);
            }
        }
    }

    public void CompleteDungeon() {
        SavingSystem.Instance.CompleteDungeon(dungeonInventory.GetItemList(), dungeonTimer);
    }

    public List<MobSO> GetDungeonMobList() {
        return dungeonMobList;
    }

    public int GetDungeonDifficultyValue() {
        return dungeonDifficultyValue;
    }

    public int GetTotalRoomNumber() {
        return totalRoomNumber;
    }

    public Inventory GetDungeonInventory() {
        return dungeonInventory;
    }
}