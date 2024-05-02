using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraViewManager : MonoBehaviour
{

    public static FreeCameraViewManager Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera freeLookCamera;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private Transform freeLookCameraFollowTransform;
    [SerializeField] private GameObject freeCameraViewMouseTransform;

    private bool freeCamera;
    private Vector3 moveDirNormalized;
    [SerializeField] private float freeCameraMoveSpeed;

    float velocity = 0f;
    float zoom;
    [SerializeField] float zoomMultiplier;
    [SerializeField] float cameraZoomSpeed;
    [SerializeField] float minOrtho;
    [SerializeField] float maxOrtho;
    [SerializeField] float smoothTime;



    private void Awake() {
        Instance = this; 
    }

    private void Start() {
        zoom = freeLookCamera.m_Lens.OrthographicSize;
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Y)) {
            SetFreeCamera(!freeCamera);
        }

        if (!freeCamera) return;

        HandleMovementInput();
        HandleZoom();
        MoveCamera();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        freeCameraViewMouseTransform.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    public void SetFreeCamera(bool freeCamera) {
        this.freeCamera = freeCamera;

        if(freeCamera) {
            freeLookCameraFollowTransform.position = Player.Instance.transform.position; 
            OverworldCamera.Instance.DeActivatePlayerCamera();
            freeLookCamera.gameObject.SetActive(true);
            HotbarUI.Instance.SetHotbarActive(false);
            PlayerMovement.Instance.DisableMovement();
            freeCameraViewMouseTransform.GetComponent<Collider2D>().enabled = true;
        } else {
            OverworldCamera.Instance.ActivatePlayerCamera();
            freeLookCamera.gameObject.SetActive(false);
            OverworldCamera.Instance.ActivatePlayerCamera();
            PlayerMovement.Instance.EnableMovement();
            HotbarUI.Instance.SetHotbarActive(true);
            freeCameraViewMouseTransform.GetComponent<Collider2D>().enabled = false;
        }
    }

    public bool CameraIsInFreeView() {
        return freeCamera;
    }

    private void HandleMovementInput() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        moveDirNormalized = new Vector3(inputVector.x, inputVector.y, 0).normalized;
    }

    private void MoveCamera() {
        freeLookCameraFollowTransform.transform.position += moveDirNormalized * freeCameraMoveSpeed * Time.deltaTime;
    }

    private void HandleZoom() {
        float scroll = GameInput.Instance.GetZoomVector().y;

        zoom -= scroll * cameraZoomSpeed;
        zoom = Mathf.Clamp(zoom, minOrtho, maxOrtho);
        freeLookCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(freeLookCamera.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime);
    }

}
