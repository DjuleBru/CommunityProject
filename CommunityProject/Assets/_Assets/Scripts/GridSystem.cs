using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem 
{
    public int width;
    public int height;
    private float cellSize;
    private float offset;

    public GridSystem(int width, int height, float cellSize, float offset) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.offset = offset;

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                GridPosition gridPosition = new GridPosition(x, y);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return (new Vector3(gridPosition.x + offset, gridPosition.y + offset, 0)) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        int x = Mathf.RoundToInt((worldPosition.x + offset) / cellSize);
        int y = Mathf.RoundToInt((worldPosition.y + offset) / cellSize);

        return new GridPosition(x, y);
    }

}
