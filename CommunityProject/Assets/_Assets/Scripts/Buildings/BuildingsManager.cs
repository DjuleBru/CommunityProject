using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{

    public static BuildingsManager Instance { get; private set; }


    public event EventHandler OnAnyBuildingSpawned;
    public event EventHandler OnAnyBuildingPlacedOrCancelled;

    private void Awake() {
        Instance = this; 
    }

    public void SetBuildingSpawned() {
        OnAnyBuildingSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void SetBuildingPlacedOrCancelled() {
        OnAnyBuildingPlacedOrCancelled?.Invoke(this, EventArgs.Empty);
    }
}
