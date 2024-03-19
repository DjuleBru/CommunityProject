using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{

    [SerializeField] private Transform roomEnterPosition;
    [SerializeField] private Transform roomExitPosition;
    [SerializeField] private GameObject roomCenterSprite;

    [SerializeField] private GameObject roomExitDoorVisual;
    [SerializeField] private GameObject roomEntryDoorVisual;
    [SerializeField] private GameObject roomExitPassageVisual;
    [SerializeField] private GameObject roomEnterPassageVisual;
    [SerializeField] private GameObject roomExitDoorShadowVisual;
    [SerializeField] private GameObject roomEntryDoorShadowVisual;

    [SerializeField] float cameraOrthographicSize;
    [SerializeField] private CinemachineVirtualCamera roomCamera;

    [SerializeField] private GameObject mobSpawnPoints;
    private Transform[] mobSpawnPointsList;
    private List<Mob> mobsInRoom = new List<Mob>();
    private float dungeonRoomDifficultyValue;
    private int setDifficultyValue = 0;

    private bool isFirstDungeonRoom;
    private bool roomIsComplete;
    private bool playerIsInRoom;

    private void Awake() {
        roomCenterSprite.SetActive(false);
        roomExitPassageVisual.SetActive(false);
        roomCamera.m_Lens.OrthographicSize = cameraOrthographicSize;

        FillMobSpawnPointsList();
    }

    private void Start() {
        if(isFirstDungeonRoom) {
            roomEnterPassageVisual.SetActive(false);
        }

        while(setDifficultyValue < dungeonRoomDifficultyValue) {
            SpawnMobs();
        };

        Debug.Log("initial room difficulty value " + dungeonRoomDifficultyValue + "Set room difficulty value" + setDifficultyValue);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T) && PlayerIsInRoom()) {
            CompleteRoom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        playerIsInRoom = true;

        if(!roomIsComplete) {
            StartCombat();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        playerIsInRoom = false;

        if (Player.Instance.transform.position.x > transform.position.x) {
            GoToNextDungeonRoom();
        } else {
            GoBackToPreviousRoom();
        }
    }

    private void FillMobSpawnPointsList() {
        mobSpawnPointsList = mobSpawnPoints.GetComponentsInChildren<Transform>();
        foreach (Transform mobSpawnPoint in mobSpawnPointsList) {
            mobSpawnPoint.gameObject.SetActive(false);
        }
    }

    private void SpawnMobs() {
        List<MobSO> mobSOs = DungeonManager.Instance.GetDungeonMobList();

        foreach (Transform mobSpawnPoint in mobSpawnPointsList) {
            if(setDifficultyValue < dungeonRoomDifficultyValue) {
                // Pick a random dungeon mob
                MobSO mobToSpawnSO = mobSOs[Random.Range(0, mobSOs.Count)];
                Transform mobToSwawn = mobToSpawnSO.mobToSpawnPrefab;

                // Spawn if we have not reached the room difficulty value

                Instantiate(mobToSwawn, mobSpawnPoint.transform.position, Quaternion.identity);

                Mob mobSpawned = mobSpawnPoint.GetComponent<Mob>();
                mobsInRoom.Add(mobSpawned);

                setDifficultyValue += mobToSpawnSO.mobDifficultyValue;
            } 
        }
    }

    private void CompleteRoom() {
        roomIsComplete = true;

        Player.Instance.ResetBattleCameraPriority();
        SetRoomCameraAsMainCamera();

        OpenExitDoor();
        OpenNextDungeonRoom();

        if(!isFirstDungeonRoom) {
            OpenDungeonRoomEntrance();
        }
    }

    #region RoomManagement
    public void OpenNextDungeonRoom() {
        int roomIndex = DungeonGenerationManager.Instance.GetDungeonRoomList().IndexOf(this);
        DungeonRoom nextDungeonRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[roomIndex + 1];
        nextDungeonRoom.gameObject.SetActive(true);
        nextDungeonRoom.OpenDungeonRoomEntrance();


        roomExitDoorVisual.SetActive(false);
        roomExitDoorShadowVisual.SetActive(false);
        roomExitPassageVisual.SetActive(true);
    }

    private void GoToNextDungeonRoom() {
        int roomIndex = DungeonGenerationManager.Instance.GetDungeonRoomList().IndexOf(this);
        DungeonRoom nextDungeonRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[roomIndex + 1];
        nextDungeonRoom.SetRoomCameraAsMainCamera();
        ResetRoomCameraPriority();
    }
    public void GoBackToPreviousRoom() {
        int roomIndex = DungeonGenerationManager.Instance.GetDungeonRoomList().IndexOf(this);

        DungeonRoom previousDungeonRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[roomIndex - 1];
        ResetRoomCameraPriority();
        previousDungeonRoom.SetRoomCameraAsMainCamera();
    }
    private void StartCombat() {
        roomEntryDoorVisual.SetActive(true);
        roomEntryDoorShadowVisual.SetActive(true);

        // Recalculate pathfinding Graph
        AstarPath.active.Scan();

        StartCoroutine(SetBattleCamera());
    }

    private IEnumerator SetBattleCamera() {
        yield return new WaitForSeconds(2f);
        Player.Instance.SetBattleCameraAsPriority();
    }

    public void OpenExitDoor() {
        roomExitDoorVisual.SetActive(false);
        roomExitDoorShadowVisual.SetActive(false);
    }

    public void OpenDungeonRoomEntrance() {
        roomEntryDoorVisual.SetActive(false);
        roomEntryDoorShadowVisual.SetActive(false);

        // Recalculate pathfinding Graph
        AstarPath.active.Scan();
    }

    #endregion

    public float GetCameraOrthographicSize() {
        return cameraOrthographicSize;
    }

    public void SetRoomCameraAsMainCamera() {
        roomCamera.m_Priority = 11;
    }

    public void ResetRoomCameraPriority() {
        roomCamera.m_Priority = 9;
    }

    public Vector3 GetRoomEnterPosition() { 
        return roomEnterPosition.position; 
    }

    public Vector3 GetRoomExitPosition() {  
        return roomExitPosition.position; 
    }

    public bool PlayerIsInRoom() {
        return playerIsInRoom;
    }

    public bool RoomIsComplete() {
        return roomIsComplete;
    }

    public bool IsFirstDungeonRoom() {
        return isFirstDungeonRoom;
    }

    public void SetRoomAsFirstDungeonRoom() {
        // Recalculate pathfinding Graph
        AstarPath.active.Scan();

        isFirstDungeonRoom = true;
        playerIsInRoom = true;
    }

    public void SetRoomDifficultyValue(int difficultyValue) {
        dungeonRoomDifficultyValue = difficultyValue;
    }

}
