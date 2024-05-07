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

    [SerializeField] private GameObject dungeonExit;
    [SerializeField] private Transform playerSpawnPoint;

    [SerializeField] float cameraOrthographicSize;
    [SerializeField] private CinemachineVirtualCamera roomCamera;

    [SerializeField] private GameObject mobSpawnPoints;
    [SerializeField] private GameObject resourceNodesSpawnPoints;
    [SerializeField] private GameObject humanoidCagesSpawnPoints;

    private List<Transform> mobSpawnPointsList;
    private List<Transform> resourceNodesSpawnPointsList;
    private List<Transform> humanoidCagesSpawnPointsList;

    private List<Mob> mobsInRoom;
    private float dungeonRoomDifficultyValue;
    private int setDifficultyValue = 0;

    private int dungeonRoomResourceValue;
    private int resourcesSpawnedInRoom;

    private int humanoidCageValue;
    private int humanoidCageSpawnedInRoom;

    private bool isFirstDungeonRoom;
    private bool isLastDungeonRoom;
    private bool roomIsComplete;
    private bool playerIsInRoom;

    private void Awake() {
        roomCenterSprite.SetActive(false);
        roomExitPassageVisual.gameObject.SetActive(false);
        roomCamera.m_Lens.OrthographicSize = cameraOrthographicSize;

        FillMobSpawnPointsList();
        FillResourceNodesSpawnPointsList();
        FillHumanoidCagesSpawnPointsList();
    }

    private void Start() {
        mobsInRoom = new List<Mob>();

        if (isFirstDungeonRoom) {
            roomEnterPassageVisual.gameObject.SetActive(false);
        }

        while (setDifficultyValue < dungeonRoomDifficultyValue) {
            SpawnMobs();
        };

        SpawnResources();
        SpawnHumanoidCages();
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
            CompleteRoom();
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
            if (isLastDungeonRoom) return;
            GoToNextDungeonRoom();

        } else {
            GoBackToPreviousRoom();
        }
    }

    private void FillMobSpawnPointsList() {
        mobSpawnPointsList = new List<Transform>();
        Transform[] mobSpawnPointsArray = mobSpawnPoints.GetComponentsInChildren<Transform>();

        foreach(Transform t in mobSpawnPointsArray) {
            mobSpawnPointsList.Add(t);
        }
        mobSpawnPointsList.Remove(mobSpawnPointsList[0]);

        foreach (Transform mobSpawnPoint in mobSpawnPointsList) {
            mobSpawnPoint.gameObject.SetActive(false);
        }
    }
    private void FillResourceNodesSpawnPointsList() {
        resourceNodesSpawnPointsList = new List<Transform>();

        Transform[] resourceNodesSpawnPointsArray = resourceNodesSpawnPoints.GetComponentsInChildren<Transform>();

        foreach (Transform t in resourceNodesSpawnPointsArray) {
            resourceNodesSpawnPointsList.Add(t);
        }
        resourceNodesSpawnPointsList.Remove(resourceNodesSpawnPointsList[0]);

        foreach (Transform resourceNodeSpawnPoint in resourceNodesSpawnPointsList) {
            resourceNodeSpawnPoint.gameObject.SetActive(false);
        }
    }

    private void FillHumanoidCagesSpawnPointsList() {
        humanoidCagesSpawnPointsList = new List<Transform>();

        Transform[] humanoidCagesSpawnPointsArray = humanoidCagesSpawnPoints.GetComponentsInChildren<Transform>();

        foreach (Transform t in humanoidCagesSpawnPointsArray) {
            humanoidCagesSpawnPointsList.Add(t);
        }
        humanoidCagesSpawnPointsList.Remove(humanoidCagesSpawnPointsList[0]);

        foreach (Transform humanoidCageSpawnPoint in humanoidCagesSpawnPointsList) {
            humanoidCageSpawnPoint.gameObject.SetActive(false);
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

                Vector3 spawnPosition = Utils.Randomize2DPoint(mobSpawnPoint.transform.position, 1.5f);

                Mob mobSpawned = Instantiate(mobToSwawn, spawnPosition, Quaternion.identity, this.transform).GetComponent<Mob>();
                mobSpawned.SetParentDungeonRoom(this);
                mobSpawned.gameObject.SetActive(false);
                mobsInRoom.Add(mobSpawned);

                setDifficultyValue += mobToSpawnSO.mobDifficultyValue;
            } 
        }
    }

    private void SpawnResources() {
        foreach (Transform resourceNodeSpawnPoint in resourceNodesSpawnPointsList) {

            if (resourcesSpawnedInRoom < dungeonRoomResourceValue) {
                // Spawn if we have not reached the room's resource node number

                // Pick a random resource node
                Transform resourceNodeToSpawn = DungeonManager.Instance.GetResourceNodesList()[Random.Range(0, DungeonManager.Instance.GetResourceNodesList().Count)].transform;

                Vector3 spawnPosition = Utils.Randomize2DPoint(resourceNodeSpawnPoint.transform.position, 0f);
                Instantiate(resourceNodeToSpawn, spawnPosition, Quaternion.identity, this.transform).GetComponent<Mob>();

                resourcesSpawnedInRoom++;
            }
        }
    }

    private void SpawnHumanoidCages() {
        foreach (Transform humanoidCageSpawnPoint in humanoidCagesSpawnPointsList) {

            if (humanoidCageSpawnedInRoom < humanoidCageValue) {
                // Spawn if we have not reached the room's resource node number

                // Pick a random resource node
                Transform humanoidCageToSpawn = DungeonManager.Instance.GetHumanoidCagesList()[Random.Range(0, DungeonManager.Instance.GetHumanoidCagesList().Count)].transform;

                Vector3 spawnPosition = Utils.Randomize2DPoint(humanoidCageSpawnPoint.transform.position, 0f);
                HumanoidCage humanoidCageSpawned = Instantiate(humanoidCageToSpawn, spawnPosition, Quaternion.identity, this.transform).GetComponent<HumanoidCage>();

                GameObject humanoidToSpawn = DungeonManager.Instance.GetHumanoidSpawnedInDungeon();
                humanoidCageSpawned.SetHumanoidGameObject(humanoidToSpawn);
                humanoidCageSpawnedInRoom++;
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

        if (isLastDungeonRoom) {
            OpenDungeonExit();
            return;
        }

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

        if (mobsInRoom.Count > 0) {
            foreach (Mob mob in mobsInRoom) {
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
        } else {
            CompleteRoom();
        }
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
    }

    private void OpenDungeonExit() {
        OpenExitDoor();
        Instantiate(dungeonExit, roomExitPosition.position, Quaternion.identity);
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

    public void SetRoomAsLastDungeonRoom() {
        isLastDungeonRoom = true;
    }

    public void SetRoomDifficultyValue(int difficultyValue) {
        dungeonRoomDifficultyValue = difficultyValue;
    }

    public void SetRoomResourceValue(int resourceValue) {
        dungeonRoomResourceValue = resourceValue;
    }

    public void SetRoomHumanoidCageValue(int humanoidCageValue) {
        this.humanoidCageValue = humanoidCageValue;
    }

    public Transform GetPlayerSpawnPoint() {
        return playerSpawnPoint;
    }

}
