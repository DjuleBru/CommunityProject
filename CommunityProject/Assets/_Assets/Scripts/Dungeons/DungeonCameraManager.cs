using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraManager : MonoBehaviour {
    public static DungeonCameraManager Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera v_cam;

    private bool movingToNextRoom;
    private Vector3 initialPosition;
    private Vector3 nextPosition;
    private float transitionStep;

    private float initialOrthographic;
    private float nextOrthographic;

    [SerializeField] private float transitionSpeed;
    [SerializeField] private float zoomSpeed;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DungeonRoom initialRoom = DungeonGenerationManager.Instance.GetDungeonRoomList()[0];
        Vector3 initialRoomPosition = DungeonGenerationManager.Instance.GetDungeonRoomPositionDictionary()[0];

        transform.position = new Vector3(initialRoomPosition.x, initialRoomPosition.y, -10);
        v_cam.m_Lens.OrthographicSize = initialRoom.GetCameraOrthographicSize();
    }

    private void Update() {
        if (!movingToNextRoom) return;
        SmoothCameraTransition(initialPosition, nextPosition);
        SmoothCameraZoom(initialOrthographic, nextOrthographic);

    }

    public void MoveToRoom(DungeonRoom nextRoom) {
        initialPosition = transform.position;   
        nextPosition = nextRoom.transform.position;

        initialOrthographic = v_cam.m_Lens.OrthographicSize;
        nextOrthographic = nextRoom.GetCameraOrthographicSize();

        transitionStep = Mathf.Abs(Vector3.Distance(nextPosition, initialPosition)) / 1000 * transitionSpeed;

        movingToNextRoom = true;
    }

    private void SmoothCameraTransition(Vector3 previousPosition, Vector3 nextPosition) {
        if (Vector3.Distance(transform.position, nextPosition) > .1f) {
            Vector3 moveDirNormalized = (nextPosition - previousPosition).normalized;

            transform.position += moveDirNormalized * Time.deltaTime * transitionSpeed;
        }
    }

    private void SmoothCameraZoom(float previousOrthographic,  float nextOrthographic) {

        if(Mathf.Abs(nextOrthographic - v_cam.m_Lens.OrthographicSize) > .5f) {
            float orthographicStep = (nextOrthographic - previousOrthographic) * Time.deltaTime * zoomSpeed;

            v_cam.m_Lens.OrthographicSize += orthographicStep;
        }
    }

}
