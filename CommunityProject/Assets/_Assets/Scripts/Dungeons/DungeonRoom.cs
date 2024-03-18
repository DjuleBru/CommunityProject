using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] private Transform roomEnterPosition;
    [SerializeField] private Transform roomExitPosition;
    [SerializeField] private GameObject roomCenterSprite;

    [SerializeField] private GameObject roomExitDoorVisual;
    [SerializeField] private GameObject roomEnterDoorVisual;
    [SerializeField] private GameObject roomExitPassageVisual;
    [SerializeField] private GameObject roomEnterPassageVisual;

    [SerializeField] float cameraOrthographicSize;

    private bool isFirstDungeonRoom;
    private bool roomIsComplete;
    private bool playerIsInRoom;

    private void Awake() {
        roomCenterSprite.SetActive(false);
        roomExitPassageVisual.SetActive(false);
    }

    private void Start() {
        if(isFirstDungeonRoom) {
            roomEnterPassageVisual.SetActive(false);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            CompleteRoom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.TryGetComponent(out Player player)) return;
        playerIsInRoom = true;

        if(!roomIsComplete) {
            CloseEntryDoor();
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

    private void CompleteRoom() {
        roomIsComplete = true;
        OpenExitDoor();
        OpenNextDungeonRoom();

        if(!isFirstDungeonRoom) {
            OpenEntryDoor();
        }
    }

    #region RoomManagement
    public void OpenNextDungeonRoom() {
        int roomIndex = DungeonGenerationManager.Instance.GetDungeonRoomList().IndexOf(this);
        DungeonRoom nextDungeonRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[roomIndex + 1];
        nextDungeonRoom.gameObject.SetActive(true);
        nextDungeonRoom.OpenDungeonRoomEntrance();


        roomExitDoorVisual.SetActive(false);
        roomExitPassageVisual.SetActive(true);
    }

    private void GoToNextDungeonRoom() {
        int roomIndex = DungeonGenerationManager.Instance.GetDungeonRoomList().IndexOf(this);
        DungeonRoom nextDungeonRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[roomIndex + 1];
        DungeonCameraManager.Instance.MoveToRoom(nextDungeonRoom);
    }

    private void CloseEntryDoor() {
        roomEnterDoorVisual.SetActive(true);
    }

    public void OpenEntryDoor() {
        roomEnterDoorVisual.SetActive(false);
    }
    public void OpenExitDoor() {
        roomExitDoorVisual.SetActive(false);
    }

    public void GoBackToPreviousRoom() {
        int roomIndex = DungeonGenerationManager.Instance.GetDungeonRoomList().IndexOf(this);

        DungeonRoom previousDungeonRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[roomIndex - 1];
        DungeonCameraManager.Instance.MoveToRoom(previousDungeonRoom);
    }

    public void OpenDungeonRoomEntrance() {
        roomEnterDoorVisual.SetActive(false);
    }

    #endregion

    public float GetCameraOrthographicSize() {
        return cameraOrthographicSize;
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
        isFirstDungeonRoom = true;
        playerIsInRoom = true;
    }

}
