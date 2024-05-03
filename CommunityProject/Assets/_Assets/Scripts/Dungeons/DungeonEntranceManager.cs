using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceManager : MonoBehaviour
{

    public static DungeonEntranceManager Instance { get; private set; }

    private List<DungeonEntrance> dungeonEntranceList = new List<DungeonEntrance>();

    private void Awake() {
        Instance = this; 
    }

    private void Start() {
        foreach(DungeonEntrance entrance in GetComponentsInChildren<DungeonEntrance>()) {
            dungeonEntranceList.Add(entrance);
        };
    }

    public List<DungeonEntrance> GetAllDungeonEntrances() {
        return dungeonEntranceList;
    }

}
