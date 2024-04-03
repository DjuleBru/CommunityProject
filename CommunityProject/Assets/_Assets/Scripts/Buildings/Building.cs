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
    }

    public enum BuildingWorksCategory {
        WoodWork,
        MetalWork,

    }

    public enum BuildingType {
        lumberMill,
        stonecutter,
        brickyard
    }

    [SerializeField] private BuildingSO buildingSO;

    private Rigidbody2D rb;
    private Collider2D buildingCollider;
    private bool isValidBuildingPlacement;
    private bool buildingPlaced;
    private int collideCount;

    public event EventHandler OnBuildingIsValidPlacement;
    public event EventHandler OnBuildingIsUnvalidPlacement;
    public event EventHandler OnBuildingPlaced;

    private void Awake() {
        buildingCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        buildingCollider.isTrigger = true;
        isValidBuildingPlacement = true;

        BuildingsManager.Instance.SetBuildingSpawned();
    }

    private void Start() {
        GameInput.Instance.OnPlaceBuilding += GameInput_OnPlaceBuilding;
        GameInput.Instance.OnPlaceBuildingCancelled += GameInput_OnPlaceBuildingCancelled;
    }

    private void GameInput_OnPlaceBuildingCancelled(object sender, EventArgs e) {
        CancelBuildingPlacement();
    }

    private void GameInput_OnPlaceBuilding(object sender, System.EventArgs e) {
        if(!buildingPlaced) {
            if(TryPlaceBuilding()) {
                PlaceBuilding();
            };
        }
    }

    private void Update() {
        if(!buildingPlaced) {
            HandleBuildingPlacement();
        }
    }

    private void HandleBuildingPlacement() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    private bool TryPlaceBuilding() {
        if (isValidBuildingPlacement) return true;
        return false;
    }

    private void PlaceBuilding() {
        buildingPlaced = true;
        buildingCollider.isTrigger = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

        OnBuildingPlaced?.Invoke(this, EventArgs.Empty);
        BuildingsManager.Instance.SetBuildingPlacedOrCancelled();
    }

    private void CancelBuildingPlacement() {
        if (buildingPlaced) return;
        BuildingsManager.Instance.SetBuildingPlacedOrCancelled();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (buildingPlaced) return;

        OnBuildingIsUnvalidPlacement?.Invoke(this, EventArgs.Empty);
        collideCount++;
        isValidBuildingPlacement = false;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (buildingPlaced) return;

        collideCount--;
        if(collideCount == 0) {
            isValidBuildingPlacement = true;
            OnBuildingIsValidPlacement?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy() {
        GameInput.Instance.OnPlaceBuilding -= GameInput_OnPlaceBuilding;
        GameInput.Instance.OnPlaceBuildingCancelled -= GameInput_OnPlaceBuildingCancelled;
    }

}
