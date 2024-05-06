using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerationManager : MonoBehaviour {
    public static DungeonGenerationManager Instance { get; private set; }

    [SerializeField] private DungeonRoom firstDungeonRoom;
    [SerializeField] private List<DungeonRoom> dungeonRoomPoolList;

    private List<DungeonRoom> dungeonRoomList = new List<DungeonRoom>();
    private Dictionary<int, Vector3> dungeonRoomPositionDictionary = new Dictionary<int, Vector3>();

    DungeonRoom previousDungeonRoom;

    private void Awake() {
        Instance = this;
        GenerateDungeon();
        SetRoomDifficultyValues();
        SetRoomTotalResourceNodes();
        DeActivateRooms();
    }

    private void GenerateDungeon() {
        Vector3 absoluteRoomPosition = new Vector3(0, 0, 0);

        DungeonRoom initialDungeonRoom = dungeonRoomPoolList[UnityEngine.Random.Range(0, dungeonRoomPoolList.Count)];
        previousDungeonRoom = initialDungeonRoom;

        for (int i = 0; i < DungeonManager.Instance.GetTotalRoomNumber(); i++) {

            DungeonRoom pooledDungeonRoom = dungeonRoomPoolList[UnityEngine.Random.Range(0, dungeonRoomPoolList.Count)];


            Vector3 newRoomEnterPosition = pooledDungeonRoom.GetRoomEnterPosition();

            Vector3 relativeRoomExitPosition = new Vector3(Mathf.Abs(previousDungeonRoom.GetRoomExitPosition().x - newRoomEnterPosition.x), 0, 0);
            absoluteRoomPosition = relativeRoomExitPosition;

            DungeonRoom newDungeonRoom = null;

            if (i == 0) {
                absoluteRoomPosition = Vector3.zero;
                pooledDungeonRoom = firstDungeonRoom;
            }

            Transform newDungeonRoomTransform = Instantiate(pooledDungeonRoom.transform, absoluteRoomPosition, Quaternion.identity, transform);
            newDungeonRoom = newDungeonRoomTransform.GetComponent<DungeonRoom>();

            if(i == 0) {
                newDungeonRoom.SetRoomAsFirstDungeonRoom();
                Player.Instance.transform.position = newDungeonRoom.GetPlayerSpawnPoint().position;
            }

            if( i == DungeonManager.Instance.GetTotalRoomNumber() -1) {
                newDungeonRoom.SetRoomAsLastDungeonRoom();
            }

            dungeonRoomList.Add(newDungeonRoom);
            dungeonRoomPositionDictionary[i] = absoluteRoomPosition;

            previousDungeonRoom = newDungeonRoom.GetComponent<DungeonRoom>();
        }
    }

    private void DeActivateRooms() {
        for(int i = 1;i < DungeonManager.Instance.GetTotalRoomNumber(); i++) {
            dungeonRoomList[i].gameObject.SetActive(false);
        }
    }

    private void SetRoomDifficultyValues() {
        int dungeonDifficultyValue = DungeonManager.Instance.GetDungeonDifficultyValue();

        List<float> roomDifficultyValues = new List<float>();
        float totalDifficultyValue = 0;

        for (int i = 0; i < DungeonManager.Instance.GetTotalRoomNumber(); i++) {

            float roomDifficultyValue = UnityEngine.Random.Range(0, 100);

            if (i == 0) {
                // First room : no mobs
                roomDifficultyValue = 0;
            }

            roomDifficultyValues.Add(roomDifficultyValue);
            totalDifficultyValue += roomDifficultyValue;

        }


        for (int i = 0; i < DungeonManager.Instance.GetTotalRoomNumber(); i++) {
            float roomDifficultyValue = roomDifficultyValues[i];
            float roomDifficultyValueScaled = (roomDifficultyValue / totalDifficultyValue)*dungeonDifficultyValue;

            dungeonRoomList[i].SetRoomDifficultyValue((int)Math.Round(roomDifficultyValueScaled));
        }

    }

    private void SetRoomTotalResourceNodes() {
        int dungeonTotalResourceNodes = DungeonManager.Instance.GetResourceNumberInDungeon();

        List<int> roomResourceNodesNumber = new List<int>();
        int totalResourcesNodesNumber = 0;

        for (int i = 0; i < DungeonManager.Instance.GetResourceNumberInDungeon(); i++) {

            int roomResourceValue = UnityEngine.Random.Range(0, 100);

            if (i == 0) {
                // First room : no mobs
                roomResourceValue = 0;
            }

            roomResourceNodesNumber.Add(roomResourceValue);
            totalResourcesNodesNumber += roomResourceValue;

        }


        for (int i = 0; i < DungeonManager.Instance.GetTotalRoomNumber(); i++) {
            float roomDifficultyValue = roomResourceNodesNumber[i];
            float roomDifficultyValueScaled = (roomDifficultyValue / totalResourcesNodesNumber) * dungeonTotalResourceNodes;

            dungeonRoomList[i].SetRoomResourceValue((int)Math.Round(roomDifficultyValueScaled));
        }
    }

    public List<DungeonRoom> GetDungeonRoomList() { 
        return dungeonRoomList; 
    }

    public Dictionary<int, Vector3> GetDungeonRoomPositionDictionary() {
        return dungeonRoomPositionDictionary;
    }
}
