using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceManager : MonoBehaviour
{

    public static DungeonEntranceManager Instance { get; private set; }

    private List<DungeonEntrance> dungeonEntranceList = new List<DungeonEntrance>();
    [SerializeField] private List<int> dungeonEntrancesSavedIDList;

    private void Awake() {
        Instance = this;
        foreach (DungeonEntrance entrance in GetComponentsInChildren<DungeonEntrance>()) {
            dungeonEntranceList.Add(entrance);
        };

        LoadDungeonEntrancesInOverworld();
    }

    public List<DungeonEntrance> GetAllDungeonEntrances() {
        return dungeonEntranceList;
    }

    public void SaveDungeonEntrancesInOverworld() {
        dungeonEntrancesSavedIDList = new List<int>();
        foreach (DungeonEntrance dungeonEntrance in dungeonEntranceList) {
            dungeonEntrancesSavedIDList.Add(dungeonEntrance.GetInstanceID());
            ES3.Save(dungeonEntrance.GetInstanceID().ToString(), dungeonEntrance.gameObject);
            Debug.Log("saved dungeon entrance " + dungeonEntrance.GetInstanceID().ToString());
        }

        ES3.Save("dungeonEntrancesSavedIDList", dungeonEntrancesSavedIDList);
    }


    public void LoadDungeonEntrancesInOverworld() {
        dungeonEntrancesSavedIDList = ES3.Load("dungeonEntrancesSavedIDList", new List<int>());

        foreach (int id in dungeonEntrancesSavedIDList) {
            ES3.Load(id.ToString());
            Debug.Log("loaded dungeon entrance " + id.ToString());
        }
    }


    public void SaveDungeonEntrance(DungeonEntrance entrance) {
        SaveDungeonEntrancesInOverworld();
    }

}
