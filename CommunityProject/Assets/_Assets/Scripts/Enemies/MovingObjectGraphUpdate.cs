using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectGraphUpdate : MonoBehaviour
{
    private Collider2D collider;

    private float graphUpdateTimer;
    [SerializeField] private float graphUpdateRate;

    private void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        graphUpdateTimer -= Time.deltaTime;
        if(graphUpdateTimer < 0 ) {
            graphUpdateTimer = graphUpdateRate;
            AstarPath.active.UpdateGraphs(collider.bounds);
        }
    }
}
