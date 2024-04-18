using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Building : MonoBehaviour
{
    public enum BuildingUICategory {
        Workstation,
        AssemblyStation,
        FoodProduction,
        Storage
    }

    public enum BuildingWorksCategory {
        WoodWork,
        MetalWork,
        Storage
    }

    public enum BuildingType {
        lumberMill,
        stonecutter,
        brickyard,
        woodworkBench,
        woodenchest,
    }

    [SerializeField] protected BuildingSO buildingSO;
    [SerializeField] protected int buildingSizeX;
    [SerializeField] protected int buildingSizeY;

    [SerializeField] protected CinemachineVirtualCamera buildingCamera;

    protected Rigidbody2D rb;
    protected Collider2D buildingCollider;
    [SerializeField] protected Collider2D interactionCollider;
    protected bool isValidBuildingPlacement;
    protected bool buildingPlaced;
    protected int collideCount;

    protected Humanoid assignedHumanoid;

    protected bool playerInteractingWithBuilding;
    protected bool workerInteractingWithBuilding;

    public event EventHandler OnBuildingIsValidPlacement;
    public event EventHandler OnBuildingIsUnvalidPlacement;
    public event EventHandler OnBuildingPlaced;

    protected virtual void Awake() {
        buildingCollider = GetComponent<Collider2D>();
        buildingCamera.enabled = false;

        interactionCollider.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        buildingCollider.isTrigger = true;
        isValidBuildingPlacement = true;
    }

    protected virtual void Start() {
        GameInput.Instance.OnPlaceBuilding += GameInput_OnPlaceBuilding;
        GameInput.Instance.OnPlaceBuildingCancelled += GameInput_OnPlaceBuildingCancelled;
        BuildingsManager.Instance.SetBuildingSpawned();
    }

    protected virtual void Update() {
        if (!buildingPlaced) {
            HandleBuildingPlacement();
            return;
        }
    }

    protected void GameInput_OnPlaceBuildingCancelled(object sender, EventArgs e) {
        CancelBuildingPlacement();
    }

    protected void GameInput_OnPlaceBuilding(object sender, System.EventArgs e) {
        if(!buildingPlaced) {
            if(TryPlaceBuilding()) {
                PlaceBuilding();
            };
        }
    }

    protected void HandleBuildingPlacement() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0;

        GridPosition buildingGridPosition = OverworldGrid.Instance.GetGridPosition(mousePosition);
        if (buildingSizeX % 2 == 0) {
            // Size X is even
            transform.position = OverworldGrid.Instance.GetWorldPosition(buildingGridPosition) - new Vector3(.5f, .5f, 0);
        } else {
            //Size X is odd
            transform.position = OverworldGrid.Instance.GetWorldPosition(buildingGridPosition);
        }
    }

    protected bool TryPlaceBuilding() {
        if (isValidBuildingPlacement) return true;
        return false;
    }

    protected virtual void PlaceBuilding() {
        SpendBuildingMaterials();
        buildingPlaced = true;
        buildingCollider.isTrigger = false;
        interactionCollider.enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

        OnBuildingPlaced?.Invoke(this, EventArgs.Empty);
        BuildingsManager.Instance.SetBuildingPlacedOrCancelled();
        BuildingsManager.Instance.AddBuilding(this);
        AstarPath.active.UpdateGraphs(buildingCollider.bounds);
    }

    protected void SpendBuildingMaterials() {
        foreach(Item item in buildingSO.buildingCostItems) {
            Player.Instance.GetInventory().RemoveItemAmount(item);
        }
    }

    protected void CancelBuildingPlacement() {
        if (buildingPlaced) return;
        BuildingsManager.Instance.SetBuildingPlacedOrCancelled();
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision) {
        if (buildingPlaced) return;
        if (collision.gameObject.GetComponent<IInteractable>() != null) return;

        OnBuildingIsUnvalidPlacement?.Invoke(this, EventArgs.Empty);
        collideCount++;
        isValidBuildingPlacement = false;
    }
    
    protected void OnTriggerExit2D(Collider2D collision) {
        if (buildingPlaced) return;
        if (collision.gameObject.GetComponent<IInteractable>() != null) return;

        collideCount--;
        if(collideCount == 0) {
            isValidBuildingPlacement = true;
            OnBuildingIsValidPlacement?.Invoke(this, EventArgs.Empty);
        }
    }

    protected void OnDestroy() {
        GameInput.Instance.OnPlaceBuilding -= GameInput_OnPlaceBuilding;
        GameInput.Instance.OnPlaceBuildingCancelled -= GameInput_OnPlaceBuildingCancelled;
    }

    public virtual void AssignHumanoid(Humanoid humanoid) {
        this.assignedHumanoid = humanoid;
    }

    public BuildingSO GetBuildingSO() {
        return buildingSO;
    }

    public bool GetBuildingPlaced() {
        return buildingPlaced;
    }

    public virtual void OpenBuildingUI() {

    }

    public virtual void InteractWithBuilding() {
        buildingCamera.enabled = true;
        OverworldCamera.Instance.DeActivatePlayerCamera();
    }

    public virtual void StopInteractingWithBuilding() {
        buildingCamera.enabled = false;
        OverworldCamera.Instance.ActivatePlayerCamera();
    }

    public virtual void ClosePanel() {

    }

    public bool GetInteractingWithBuilding() {
        return playerInteractingWithBuilding;
    }

    public void SetBuildingVisualDebugScore(string score) {
        GetComponentInChildren<BuildingVisual>().SetBuildingScoreText(score);
    }

}
