using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerationManager : MonoBehaviour {
    public static DungeonGenerationManager Instance { get; private set; }

    [SerializeField] private int totalRoomNumber;
    [SerializeField] private List<DungeonRoom> dungeonRoomPoolList;

    private List<DungeonRoom> dungeonRoomList = new List<DungeonRoom>();
    private Dictionary<int, Vector3> dungeonRoomPositionDictionary = new Dictionary<int, Vector3>();

    DungeonRoom previousDungeonRoom;

    private void Awake() {
        Instance = this;
        GenerateDungeon();
        DeActivateRooms();
    }

    private void GenerateDungeon() {
        Vector3 absoluteRoomPosition = new Vector3(0, 0, 0);

        DungeonRoom initialDungeonRoom = dungeonRoomPoolList[Random.Range(0, dungeonRoomPoolList.Count)];
        previousDungeonRoom = initialDungeonRoom;

        for (int i = 0; i < totalRoomNumber; i++) {

            DungeonRoom pooledDungeonRoom = dungeonRoomPoolList[Random.Range(0, dungeonRoomPoolList.Count)];


            Vector3 newRoomEnterPosition = pooledDungeonRoom.GetRoomEnterPosition();

            Vector3 relativeRoomExitPosition = new Vector3(Mathf.Abs(previousDungeonRoom.GetRoomExitPosition().x - newRoomEnterPosition.x), 0, 0);
            absoluteRoomPosition = relativeRoomExitPosition;

            if (i == 0) {
                absoluteRoomPosition = Vector3.zero;
            }

            DungeonRoom newDungeonRoom = Instantiate(pooledDungeonRoom.transform, absoluteRoomPosition, Quaternion.identity, this.transform).GetComponent<DungeonRoom>();

            if (i == 0) {
                newDungeonRoom.SetRoomAsFirstDungeonRoom();
            }

            dungeonRoomList.Add(newDungeonRoom);
            dungeonRoomPositionDictionary[i] = absoluteRoomPosition;

            previousDungeonRoom = newDungeonRoom.GetComponent<DungeonRoom>();
        }
    }

    private void DeActivateRooms() {
        for(int i = 1;i < totalRoomNumber;i++) {
            dungeonRoomList[i].gameObject.SetActive(false);
        }
    }

    public List<DungeonRoom> GetDungeonRoomList() { 
        return dungeonRoomList; 
    }

    public Dictionary<int, Vector3> GetDungeonRoomPositionDictionary() {
        return dungeonRoomPositionDictionary;
    }
}
