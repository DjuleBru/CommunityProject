using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
   [SerializeField] private List<Obstacle> obstacles;

    [SerializeField] private List<int> obstaclesSavedIDList;

    public static ObstaclesManager Instance;

    public void SaveObstaclesInOverworld() {
        obstaclesSavedIDList = new List<int>();

        foreach (Obstacle obstacle in obstacles) {
            obstacle.enabled = true;
            obstaclesSavedIDList.Add(obstacle.GetInstanceID());
            ES3.Save(obstacle.GetInstanceID().ToString(), obstacle.gameObject);
        }

        ES3.Save("obstaclesSavedIDList", obstaclesSavedIDList);
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LoadObstacles();
    }

    private void LoadObstacles() {

        obstaclesSavedIDList = ES3.Load("obstaclesSavedIDList", new List<int>());

        foreach (int id in obstaclesSavedIDList) {
            ES3.Load(id.ToString());
        }
    }
}
