using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

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

    private bool followHumanoid;
    private Humanoid followingHumanoid;

    private bool destroyingBuildings;

    private void Awake() {
        Instance = this; 
    }

    private void Start() {
        zoom = playerCamera.m_Lens.OrthographicSize;
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Y)) {
            SetFreeCamera(!freeCamera);
        }

        if (!freeCamera) {
            HandlePlayerCameraZoom();
            return;
        };

        HandleMovementInput();
        HandleFreeCameraZoom();
        if(destroyingBuildings) {
            HandleDestroyBuilding();
        }

        if(followHumanoid) {
            freeLookCameraFollowTransform.transform.position = followingHumanoid.transform.position;
        } else {
            MoveCamera();
        }

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
            zoom = freeLookCamera.m_Lens.OrthographicSize;
            FreeCameraViewMouseTransform.Instance.EnableMouseTransform(true);
        } else {
            OverworldCamera.Instance.ActivatePlayerCamera();
            freeLookCamera.gameObject.SetActive(false);
            OverworldCamera.Instance.ActivatePlayerCamera();
            PlayerMovement.Instance.EnableMovement();
            HotbarUI.Instance.SetHotbarActive(true);
            freeCameraViewMouseTransform.GetComponent<Collider2D>().enabled = false;
            zoom = playerCamera.m_Lens.OrthographicSize;
            CursorManager.Instance.ResetCursor();
            FreeCameraViewMouseTransform.Instance.EnableMouseTransform(false);
        }
    }

    public void ZoomToLocation(Vector3 location) {
        freeLookCameraFollowTransform.transform.position = new Vector3(location.x, location.y, 0);
    }

    public void FollowHumanoid(Humanoid humanoid) {
        followHumanoid = true;
        followingHumanoid = humanoid;
        freeLookCamera.m_Lens.OrthographicSize = 10;
        zoom = freeLookCamera.m_Lens.OrthographicSize;
    }

    public bool CameraIsInFreeView() {
        return freeCamera;
    }

    private void HandleMovementInput() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        if(inputVector !=  Vector2.zero) {
            followHumanoid = false;
        }
        moveDirNormalized = new Vector3(inputVector.x, inputVector.y, 0).normalized;
    }

    private void MoveCamera() {
        freeLookCameraFollowTransform.transform.position += moveDirNormalized * freeCameraMoveSpeed * Time.deltaTime;
    }

    private void HandleFreeCameraZoom() {
        float scroll = GameInput.Instance.GetZoomVector().y;

        zoom -= scroll * cameraZoomSpeed;
        zoom = Mathf.Clamp(zoom, minOrtho, maxOrtho);
        freeLookCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(freeLookCamera.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime);
    }

    private void HandlePlayerCameraZoom() {
        float scroll = GameInput.Instance.GetZoomVector().y;

        zoom -= scroll * cameraZoomSpeed;
        zoom = Mathf.Clamp(zoom, minOrtho, maxOrtho);
        playerCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(playerCamera.m_Lens.OrthographicSize, zoom, ref velocity, smoothTime);
    }

    public void SetDestroyBuilding() {
        destroyingBuildings = true;
    }

    private void HandleDestroyBuilding() {
        if(Input.GetMouseButtonDown(0)) {

            Building buildingToDestroy = FreeCameraViewMouseTransform.Instance.GetBuildingHovered();

            if (buildingToDestroy != null) {
                
                buildingToDestroy.RemoveAssignedHumanoid();
                BuildingsManager.Instance.RemoveBuilding(buildingToDestroy);
                Destroy(buildingToDestroy.gameObject);
            }
        }

        if(Input.GetMouseButtonDown(1) ) {
            SetFreeCamera(false);
        }
        
    }

}
