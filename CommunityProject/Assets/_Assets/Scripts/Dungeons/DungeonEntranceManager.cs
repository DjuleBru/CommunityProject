using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceManager : MonoBehaviour
{

    public static DungeonEntranceManager Instance { get; private set; }

    private List<DungeonEntrance> dungeonEntranceList = new List<DungeonEntrance>();
    private List<DungeonStatsBoard> statsBoardsList = new List<DungeonStatsBoard>();
    [SerializeField] private List<int> dungeonEntrancesSavedIDList;
    [SerializeField] private List<int> statsBoardsSavesIDList;

    private void Awake() {
        Instance = this;
        foreach (DungeonEntrance entrance in GetComponentsInChildren<DungeonEntrance>()) {
            dungeonEntranceList.Add(entrance);
            statsBoardsList.Add(entrance.GetDungeonStatsBoard());
        };

        LoadDungeonEntrancesInOverworld();
        LoadStatsBoardsInOverworld();
    }

    public List<DungeonEntrance> GetAllDungeonEntrances() {
        return dungeonEntranceList;
    }

    public void SaveDungeonEntrancesInOverworld() {
        dungeonEntrancesSavedIDList = new List<int>();

        foreach (DungeonEntrance dungeonEntrance in dungeonEntranceList) {
            dungeonEntrancesSavedIDList.Add(dungeonEntrance.GetInstanceID());
            ES3.Save(dungeonEntrance.GetInstanceID().ToString(), dungeonEntrance);
            Debug.Log("saved dungeon entrance " + dungeonEntrance.GetInstanceID().ToString());
        }

        ES3.Save("dungeonEntrancesSavedIDList", dungeonEntrancesSavedIDList);
    }

    public void SaveStatsBoardsInOverworld() {
        statsBoardsSavesIDList = new List<int>();
        foreach (DungeonStatsBoard statsBoard in statsBoardsList) {
            statsBoardsSavesIDList.Add(statsBoard.GetInstanceID());
            ES3.Save(statsBoard.GetInstanceID().ToString(), statsBoard);
            Debug.Log("saved stats board " + statsBoard.GetInstanceID().ToString());
        }

        ES3.Save("statsBoardsSavesIDList", statsBoardsSavesIDList);
    }

    public void LoadDungeonEntrancesInOverworld() {
        dungeonEntrancesSavedIDList = ES3.Load("dungeonEntrancesSavedIDList", new List<int>());

        foreach (int id in dungeonEntrancesSavedIDList) {
            ES3.Load(id.ToString());
            Debug.Log("loaded dungeon entrance " + id.ToString());
        }
    } 
    
    public void LoadStatsBoardsInOverworld() {
        statsBoardsSavesIDList = ES3.Load("statsBoardsSavesIDList", new List<int>());

        foreach (int id in statsBoardsSavesIDList) {
            ES3.Load(id.ToString());
            Debug.Log("loaded stats board " + id.ToString());
        }
    }

    public void OnApplicationQuit() {
        SaveDungeonEntrancesInOverworld();
        SaveStatsBoardsInOverworld();
    }

    public void SaveDungeonEntrance(DungeonEntrance entrance) {
        SaveDungeonEntrancesInOverworld();
        SaveStatsBoardsInOverworld();
    }

}
