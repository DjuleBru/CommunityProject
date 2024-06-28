using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BehaviorDesigner.Runtime.BehaviorManager;
using BehaviorDesigner.Runtime;

public class HumanoidsManager : MonoBehaviour
{
    public static HumanoidsManager Instance { get; private set; }

    [SerializeField] private List<Humanoid> humanoidsInOverworld;
    [SerializeField] private List<Humanoid> humanoidsSavedFromDungeon;
    [SerializeField] private List<HumanoidSO> humanoidSOList;
    [SerializeField] private List<int> humanoidsSavedIDList;
    [SerializeField] private List<int> humanoidsSavedFromDungeonIDList;

    [SerializeField] private GameObject defaultHumanoid;

    [SerializeField] ExternalBehaviorTree workerBehaviorTree;
    [SerializeField] ExternalBehaviorTree haulierBehaviorTree;
    [SerializeField] ExternalBehaviorTree dungeoneerBehaviorTree;
    [SerializeField] ExternalBehaviorTree joblessBehaviorTree;
    [SerializeField] ExternalBehaviorTree justFreedBehaviorTree;

    [SerializeField] Sprite speedSprite;
    [SerializeField] Sprite strengthSprite;
    [SerializeField] Sprite intelligenceSprite;
    [SerializeField] Sprite agilitySprite;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        humanoidsInOverworld = new List<Humanoid>();
        humanoidsSavedFromDungeon = new List<Humanoid>();

        if (DungeonManager.Instance == null) {
            LoadHumanoidsInOverworld();
            LoadHumanoidsSavedFromDungeonsInOverworld();
        }
    }

    public void AddHumanoidInOverworld(Humanoid humanoid) {
        if(humanoidsInOverworld.Contains(humanoid)) return;
        humanoidsInOverworld.Add(humanoid);
    }
    public void AddHumanoidSavedFromDungeon(Humanoid humanoid) {
        humanoidsSavedFromDungeon.Add(humanoid);
    }

    public List<Humanoid> GetHumanoids() {
        return humanoidsInOverworld;
    }

    public void SaveHumanoidsInOverworld() {
        humanoidsSavedIDList = new List<int>();

        foreach (Humanoid humanoid in humanoidsInOverworld) {
            humanoidsSavedIDList.Add(humanoid.GetInstanceID());
            ES3.Save(humanoid.GetInstanceID().ToString(), humanoid.gameObject);
            Debug.Log("saved " + humanoid);

        }

        ES3.Save("humanoidsSavedIDList", humanoidsSavedIDList);
    }


    public void SaveHumanoidsSavedFromDungeon() {
        humanoidsSavedFromDungeonIDList = new List<int>();

        foreach (Humanoid humanoid in humanoidsSavedFromDungeon) {
            humanoidsSavedFromDungeonIDList.Add(humanoid.GetInstanceID());
            ES3.Save(humanoid.GetInstanceID().ToString(), humanoid.gameObject);
        }

        ES3.Save("humanoidsSavedFromLastDungeon", humanoidsSavedFromDungeonIDList);
    }

    public void LoadHumanoidsInOverworld() {
        humanoidsSavedIDList = ES3.Load("humanoidsSavedIDList", new List<int>());
        Debug.Log("loading " + humanoidsSavedIDList.Count + " humanoids ");
        foreach (int id in humanoidsSavedIDList) {
            ES3.Load(id.ToString());
        }
    }

    public void LoadHumanoidsSavedFromDungeonsInOverworld() {

        humanoidsSavedFromDungeonIDList = ES3.Load("humanoidsSavedFromLastDungeon", new List<int>());

        foreach (int id in humanoidsSavedFromDungeonIDList) {
            ES3.Load(id.ToString());
        }
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


    public List<HumanoidSO> GetBuildingHumanoidTypeProficiency(BuildingSO buildingSO) {
        Building.BuildingCategory worksCategory = buildingSO.buildingCategory;
        List<HumanoidSO> humanoidWithProficiency = new List<HumanoidSO>();

        foreach (HumanoidSO humanoidSO in humanoidSOList) {
            if(humanoidSO.humanoidProficiencies.Contains(worksCategory)) {
                humanoidWithProficiency.Add(humanoidSO);
            }
        }
        return humanoidWithProficiency;
    }

    public Sprite GetStatSprite(Humanoid.Stat stat) {
        if(stat == Humanoid.Stat.speed) {
            return speedSprite;
        }

        if (stat == Humanoid.Stat.intelligence) {
            return intelligenceSprite;
        }

        if (stat == Humanoid.Stat.agility) {
            return agilitySprite;
        }

        if (stat == Humanoid.Stat.strength) {
            return strengthSprite;
        }
        return null;

    }
}
