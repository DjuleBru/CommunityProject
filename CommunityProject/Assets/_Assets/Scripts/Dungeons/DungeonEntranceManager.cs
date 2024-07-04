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
        Debug.Log("saving dungeon entrances");
        dungeonEntrancesSavedIDList = new List<int>();

        foreach (DungeonEntrance dungeonEntrance in dungeonEntranceList) {
            dungeonEntrancesSavedIDList.Add(dungeonEntrance.GetInstanceID());
            ES3.Save(dungeonEntrance.GetInstanceID().ToString(), dungeonEntrance);
        }

        ES3.Save("dungeonEntrancesSavedIDList", dungeonEntrancesSavedIDList);
    }

    public void SaveStatsBoardsInOverworld() {
        Debug.Log("saving stats boards");
        statsBoardsSavesIDList = new List<int>();
        foreach (DungeonStatsBoard statsBoard in statsBoardsList) {
            statsBoardsSavesIDList.Add(statsBoard.GetInstanceID());
            ES3.Save(statsBoard.GetInstanceID().ToString(), statsBoard);
        }

        ES3.Save("statsBoardsSavesIDList", statsBoardsSavesIDList);
    }

    public void LoadDungeonEntrancesInOverworld() {
        Debug.Log("loading dungeon entrances");
        dungeonEntrancesSavedIDList = ES3.Load("dungeonEntrancesSavedIDList", new List<int>());

        foreach (int id in dungeonEntrancesSavedIDList) {
            ES3.Load(id.ToString());
        }
    } 
    
    public void LoadStatsBoardsInOverworld() {
        Debug.Log("loading stat boards");
        statsBoardsSavesIDList = ES3.Load("statsBoardsSavesIDList", new List<int>());

        foreach (int id in statsBoardsSavesIDList) {
            ES3.Load(id.ToString());
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
