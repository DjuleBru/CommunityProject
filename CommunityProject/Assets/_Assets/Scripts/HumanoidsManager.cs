using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BehaviorDesigner.Runtime.BehaviorManager;
using BehaviorDesigner.Runtime;

public class HumanoidsManager : MonoBehaviour
{
    public static HumanoidsManager Instance { get; private set; }

    [SerializeField] private List<Humanoid> humanoidsInOverworld;
    [SerializeField] private List<int> humanoidsSavedIDList;
    [SerializeField] private List<int> humanoidsSavedFromDungeonIDList;

    [SerializeField] private GameObject defaultHumanoid;

    [SerializeField] ExternalBehaviorTree workerBehaviorTree;
    [SerializeField] ExternalBehaviorTree haulierBehaviorTree;
    [SerializeField] ExternalBehaviorTree dungeoneerBehaviorTree;
    [SerializeField] ExternalBehaviorTree joblessBehaviorTree;
    [SerializeField] ExternalBehaviorTree justFreedBehaviorTree;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        humanoidsInOverworld = new List<Humanoid>();

        if(DungeonManager.Instance == null) {
            LoadHumanoidsInOverworld();
            //LoadHumanoidsSavedFromDungeonsInOverworld();
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            SaveHumanoidsInOverworld();
        }

        if(Input.GetKeyDown(KeyCode.U)) {
            LoadHumanoidsInOverworld();
        }
    }

    public void AddHumanoidInOverworld(Humanoid humanoid) {
        if(humanoidsInOverworld.Contains(humanoid)) return;
        humanoidsInOverworld.Add(humanoid);
    }

    public List<Humanoid> GetHumanoids() {
        return humanoidsInOverworld;
    }

    public void SaveHumanoidsInOverworld() {
        humanoidsSavedIDList = new List<int>();

        foreach (Humanoid humanoid in humanoidsInOverworld) {
            //humanoid.SaveHumanoid();
            humanoidsSavedIDList.Add(humanoid.GetInstanceID());
            ES3.Save(humanoid.GetInstanceID().ToString(), humanoid.gameObject);

        }

        ES3.Save("humanoidsSavedIDList", humanoidsSavedIDList);
    }

    public void LoadHumanoidsInOverworld() {
        humanoidsSavedIDList = ES3.Load("humanoidsSavedIDList", new List<int>());

        foreach (int id in humanoidsSavedIDList) {
            ES3.Load(id.ToString());
        }
    }
    public void AddHumanoidSavedFromDungeon(int id) {
        humanoidsSavedFromDungeonIDList.Add(id);
    }

    public void LoadHumanoidsSavedFromDungeonsInOverworld() {

        humanoidsSavedFromDungeonIDList = ES3.Load("humanoidsSavedFromLastDungeon", new List<int>());

        if (humanoidsSavedFromDungeonIDList.Count != 0) {
            foreach (int id in humanoidsSavedFromDungeonIDList) {
                //GameObject humanoidGO = Instantiate(defaultHumanoid);
                //GameObject humanoidGO = ES3.Load(id.ToString(), defaultHumanoid);
            }
        }

        ES3.Save("humanoidsSavedFromLastDungeon", new List<int>());
    }


    public void SaveHumanoidsSavedFromDungeon() {
        Debug.Log("saving humanoid savec from dungeons :");
        foreach(int id in humanoidsSavedFromDungeonIDList) {
            Debug.Log(id);  
        }
        ES3.Save("humanoidsSavedFromLastDungeon", humanoidsSavedFromDungeonIDList);
    }

    public ExternalBehaviorTree GetJustFreedBehaviorTree() {
        return justFreedBehaviorTree;
    }
    public ExternalBehaviorTree GetWorkerBehaviorTree() {
        return workerBehaviorTree;
    }
    public ExternalBehaviorTree GetHaulerBehaviorTree() {
        return haulierBehaviorTree;
    }
    public ExternalBehaviorTree GetDungeoneerBehaviorTree() {
        return dungeoneerBehaviorTree;
    }
    public ExternalBehaviorTree GetUnassignedBehaviorTree() {
        return joblessBehaviorTree;
    }

}
