using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGrid : MonoBehaviour
{
    public static OverworldGrid Instance { get; private set; }

    private GridSystem gridSystem;

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private int gridCellSize;


    private void Awake() {
        Instance = this;

        gridSystem = new GridSystem(gridWidth, gridHeight, gridCellSize, 0f);
    }


    public GridPosition GetGridPosition(Vector3 worldPosition) {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return gridSystem.GetWorldPosition(gridPosition);
    }

}
