using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{

    [SerializeField] private Transform roomEnterPosition;
    [SerializeField] private Transform roomExitPosition;
    [SerializeField] private GameObject roomCenterSprite;

    [SerializeField] private DungeonDoorVisual roomExitDoorVisual;
    [SerializeField] private DungeonDoorVisual roomEntryDoorVisual;
    [SerializeField] private DungeonDoorVisual roomExitDoorShadowVisual;
    [SerializeField] private DungeonDoorVisual roomEntryDoorShadowVisual;

    [SerializeField] private DungeonDoorVisual roomExitPassageVisual;
    [SerializeField] private DungeonDoorVisual roomEnterPassageVisual;

    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] float cameraOrthographicSize;
    [SerializeField] private CinemachineVirtualCamera roomCamera;

    [SerializeField] private GameObject mobSpawnPoints;
    private Transform[] mobSpawnPointsList;
    private List<Mob> mobsInRoom;
    private float dungeonRoomDifficultyValue;
    private int setDifficultyValue = 0;

    private bool isFirstDungeonRoom;
    private bool roomIsComplete;
    private bool playerIsInRoom;

    private void Awake() {
        roomCenterSprite.SetActive(false);
        roomExitPassageVisual.gameObject.SetActive(false);
        roomCamera.m_Lens.OrthographicSize = cameraOrthographicSize;

        FillMobSpawnPointsList();
    }

    private void Start() {
        if(isFirstDungeonRoom) {
            roomEnterPassageVisual.gameObject.SetActive(false);
        }

        while(setDifficultyValue < dungeonRoomDifficultyValue) {
            SpawnMobs();
        };
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T) && PlayerIsInRoom()) {
            OpenNextDungeonRoom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        playerIsInRoom = true;

        if(isFirstDungeonRoom) {
            CloseEntryDoor();
        } else {
            if (!roomIsComplete) {
                StartCoroutine(CloseDungeonRoom());
            }
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
        mobsInRoom = new List<Mob>();

        foreach (Transform mobSpawnPoint in mobSpawnPointsList) {
            if(setDifficultyValue < dungeonRoomDifficultyValue) {
                // Pick a random dungeon mob
                MobSO mobToSpawnSO = mobSOs[Random.Range(0, mobSOs.Count)];
                Transform mobToSwawn = mobToSpawnSO.mobToSpawnPrefab;

                // Spawn if we have not reached the room difficulty value

                Vector3 spawnPosition = Utils.Randomize2DPoint(mobSpawnPoint.transform.position, 1.5f);

                Mob mobSpawned = Instantiate(mobToSwawn, spawnPosition, Quaternion.identity, this.transform).GetComponent<Mob>();
                mobSpawned.SetParentDungeonRoom(this);
                mobSpawned.gameObject.SetActive(false);
                mobsInRoom.Add(mobSpawned);

                setDifficultyValue += mobToSpawnSO.mobDifficultyValue;
            } 
        }
    }

    public void RemoveMobFromDungeonRoomMobList(Mob mob) {
        mobsInRoom.Remove(mob);
        if(mobsInRoom.Count == 0) {
            CompleteRoom();
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

        roomExitDoorVisual.OpenDoor();
        roomExitDoorShadowVisual.OpenDoor();
        
        roomExitPassageVisual.gameObject.SetActive(true);
        roomExitPassageVisual.CloseDoor();
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

    private IEnumerator CloseDungeonRoom() {
        CloseEntryDoor();
        SetRoomCamera();

        //Disable player actions
        float delayToPlayerDeActivation = .5f;
        yield return new WaitForSeconds(delayToPlayerDeActivation);
        Player.Instance.DisablePlayerActions();

        float delayToMonsterActivation = 1f;
        yield return new WaitForSeconds(delayToMonsterActivation);

        StartCoroutine(StartCombat());
    }
    private void CloseEntryDoor() {
        roomEntryDoorVisual.CloseDoor();
        roomEntryDoorShadowVisual.CloseDoor();
    }
    private IEnumerator StartCombat() {

        float delayBetweenMobActivation = .2f;

        foreach(Mob mob in mobsInRoom) {
            mob.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayBetweenMobActivation);
        }

        foreach (Mob mob in mobsInRoom) {
            mob.SetAllMobsSpawned();
        }

        Player.Instance.EnablePlayerActions();

        float delayAfterMobActivationToReturnToBattleCamera = 1f;
        yield return new WaitForSeconds(delayAfterMobActivationToReturnToBattleCamera);

        // Recalculate pathfinding Graph
        AstarPath.active.Scan();

        //Enable player actions
        SetBattleCamera();
    }

    private void SetBattleCamera() {
        Player.Instance.SetBattleCameraAsPriority();
    }

    private void SetRoomCamera() {
        Player.Instance.ResetBattleCameraPriority();
    }

    public void OpenExitDoor() {
        roomExitDoorVisual.OpenDoor();
        roomExitDoorShadowVisual.OpenDoor();
    }

    public void OpenDungeonRoomEntrance() {
        roomEntryDoorVisual.OpenDoor();
        roomEntryDoorShadowVisual.OpenDoor();
        roomEnterPassageVisual.gameObject.SetActive(true);
        roomEnterPassageVisual.CloseDoor();

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

    public Transform GetPlayerSpawnPoint() {
        return playerSpawnPoint;
    }

}
