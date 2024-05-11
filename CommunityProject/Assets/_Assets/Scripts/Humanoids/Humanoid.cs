using BehaviorDesigner.Runtime;
using ES3Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BehaviorDesigner.Runtime.BehaviorManager;

public class Humanoid : MonoBehaviour
{
    public enum Job {
        Unassigned,
        Dungeoneer,
        Worker,
        Haulier,
        Shipper
    }

    protected Building assignedBuilding;
    [SerializeField] private float roamDistanceToBuilding;

    [SerializeField] private HumanoidSO humanoidSO;
    [SerializeField] private Job job;
    [SerializeField] private bool debugJob;

    private HumanoidWork humanoidWork;
    private HumanoidHaul humanoidHaul;
    private HumanoidDungeonCrawl humanoidDungeonCrawl;
    private HumanoidAnimatorManager humanoidAnimatorManager;
    private HumanoidMovement humanoidMovement;
    private HumanoidCarry humanoidCarry;

    private HumanoidVisual humanoidVisual;
    private HumanoidInteraction humanoidInteraction;
    private Collider2D collider2D;

    private string humanoidName;
    private string humanoidActionDesriprion;

    private float moveSpeed;
    private float workingSpeed;
    private float productivity;
    private float hunger;
    private float energy;
    private float strength;
    private float armor;
    private int carryCapacity;

    [SerializeField] private float hungerDepletionRate;
    [SerializeField] private float energyDepletionRate;

    private Job jobAssigned;
    private bool autoAssign = true;
    private bool freedFromDungeon;

    [SerializeField] private BehaviorDesigner.Runtime.BehaviorTree behaviorTree;

    private void Awake() {
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidHaul = GetComponent<HumanoidHaul>();
        humanoidVisual = GetComponentInChildren<HumanoidVisual>();
        humanoidInteraction = GetComponentInChildren<HumanoidInteraction>();
        humanoidDungeonCrawl = GetComponent<HumanoidDungeonCrawl>();
        humanoidAnimatorManager = GetComponentInChildren<HumanoidAnimatorManager>();
        humanoidMovement = GetComponent<HumanoidMovement>();
        humanoidCarry = GetComponent<HumanoidCarry>();

        collider2D = GetComponent<Collider2D>();

        if (debugJob) {
            jobAssigned = job;
            AssignBehaviorTree();
        }

        if (humanoidName == null) {
            humanoidName = HumanoidNames.GetRandomName();
        }
    }

    private void Start() {
        humanoidWork.OnHumanoidWorkStarted += HumanoidWork_OnHumanoidWorkStarted;
        humanoidWork.OnHumanoidWorkStopped += HumanoidWork_OnHumanoidWorkStopped;

        LoadHumanoid();
        InitializeHumanoidStats();

        if (DungeonManager.Instance != null) {
            // This is a dungeon scene : Humanoid is being freed from dungeon
            freedFromDungeon = true;
            behaviorTree.ExternalBehavior = HumanoidsManager.Instance.GetJustFreedBehaviorTree();
            HumanoidsManager.Instance.AddHumanoidSavedFromDungeon(this);
            DungeonManager.Instance.SetHumanoidsAsSaved();
        }
        else {
            HumanoidsManager.Instance.AddHumanoidInOverworld(this);
        }
    }

    private void Update() {
        hunger -= hungerDepletionRate * Time.deltaTime;
        energy -= energyDepletionRate * Time.deltaTime;
    }

    #region EVENT RESPONSES
    private void HumanoidWork_OnHumanoidWorkStopped(object sender, System.EventArgs e) {
        humanoidVisual.gameObject.SetActive(true);
        humanoidInteraction.gameObject.SetActive(false);
        collider2D.enabled = true;

        SetHumanoidActionDescription("Worker is away");
    }

    private void HumanoidWork_OnHumanoidWorkStarted(object sender, System.EventArgs e) {
        humanoidVisual.gameObject.SetActive(false);
        humanoidInteraction.gameObject.SetActive(false);
        collider2D.enabled = false;

        SetHumanoidActionDescription("At work !");
    }

    #endregion

    #region SET PARAMETERS
    public void SetJob(Job job) {
        if (jobAssigned == job) return;

        StopTask();

        jobAssigned = job;
        AssignBehaviorTree();
    }

    public void AssignBehaviorTree() {

        if (jobAssigned == Job.Worker) {
            behaviorTree.ExternalBehavior = HumanoidsManager.Instance.GetWorkerBehaviorTree();
            return;
        }

        if (jobAssigned == Job.Haulier) {
            behaviorTree.ExternalBehavior = HumanoidsManager.Instance.GetHaulerBehaviorTree();
            return;
        }

        if (jobAssigned == Job.Dungeoneer) {
            behaviorTree.ExternalBehavior = HumanoidsManager.Instance.GetDungeoneerBehaviorTree();
            return;
        }

        if (jobAssigned == Job.Unassigned) {
            behaviorTree.ExternalBehavior = HumanoidsManager.Instance.GetUnassignedBehaviorTree();
            return;
        }
    }

    public void AssignBuilding(Building building) {
        assignedBuilding = building;
    }

    public void RemoveAssignedBuilding() {
        assignedBuilding = null;
    }

    public void SetAutoAssign(bool autoAssignActive) {
        if (!autoAssignActive && !autoAssign) return;
        autoAssign = autoAssignActive;

        StopTask();
        assignedBuilding = null;
    }

    public void SetHumanoidActionDescription(string description) {
        humanoidActionDesriprion = description;
        ProductionBuildingUI.Instance.RefreshWorkerPanel();
    }

    #endregion

    #region GET PARAMETERS

    public HumanoidSO GetHumanoidSO() {
        return humanoidSO;
    }

    public float GetWorkingSpeed() {
        return workingSpeed;
    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }

    public float GetProductivity() {
        return productivity;
    }

    public float GetHunger() {
        return hunger;
    }

    public float GetEnergy() {
        return energy;
    }

    public float GetStrength() {
        return strength;
    }

    public float GetArmor() {
        return armor;
    }

    public int GetCarryCapacity() {
        return carryCapacity;
    }

    public Job GetJob() {
        return jobAssigned;
    }

    public Building GetAssignedBuilding() {
        return assignedBuilding;
    }

    public float GetRoamDistanceToBuilding() {
        return roamDistanceToBuilding;
    }

    public bool IsDungeoneer() {
        return jobAssigned == Job.Dungeoneer;
    }

    public bool IsWorker() {
        return jobAssigned == Job.Worker;
    }

    public bool IsShipper() {
        return jobAssigned == Job.Shipper;
    }

    public bool IsHaulier() {
        return jobAssigned == Job.Haulier;
    }

    public bool GetHasJobAssigned() {
        return jobAssigned != Job.Unassigned;
    }

    public string GetHumanoidName() {
        return humanoidName;
    }

    public string GetHumanoidActionDescription() {
        return humanoidActionDesriprion;
    }

    public bool GetAutoAssign() {
        return autoAssign;
    }

    public HumanoidVisual GetHumanoidVisual() {
        return humanoidVisual;
    }

    #endregion

    #region MODIFY STATS

    public void ChangeMoveSpeed(float moveSpeedAddition) {
        moveSpeed += moveSpeedAddition;
    }

    public void MultiplyMoveSpeed(float moveSpeedMultiplier) {
        moveSpeed *= moveSpeedMultiplier;
    }

    public void ChangeCarryCapacity(int carryCapacityAddition) {
        carryCapacity += carryCapacityAddition;
    }

    public void ChangeWorkingSpeed(float workingSpeedAddition) {
        workingSpeed += workingSpeedAddition;
    }

    public void MultiplyWorkingSpeed(float workingSpeedMultiplier) {
        workingSpeed *= workingSpeedMultiplier;
    }

    public void ChangeStrength(float strengthAddition) {
        strength += strengthAddition;
    }

    public void ChangeArmor(float armorAddition) {
        armor += armorAddition;
    }

    public void Feed(float hungerAddition) {
        hunger += hungerAddition;
    }

    public void FillEnergy() {
        energy = 100;
    }

    #endregion

    private void StopTask() {
        if (jobAssigned == Job.Worker) {
            if (humanoidWork.GetWorking()) {
                humanoidWork.StopWorking();
            }
        }

        if (jobAssigned == Job.Haulier) {
            humanoidHaul.StopCarrying();
        }

        if (IsDungeoneer()) {
            humanoidDungeonCrawl.StopCrawling();
        }
    }

    public void LoadHumanoid() {
        if(freedFromDungeon) {
            freedFromDungeon = false;
            transform.position = Vector3.zero;
        }

        AssignBehaviorTree();
        humanoidCarry.LoadHumanoidCarry();
        humanoidAnimatorManager.SetAnimator(humanoidSO.animatorController);
        humanoidMovement.LoadHumanoidMovement();
        humanoidWork.LoadHumanoidWork();
    }

    public void InitializeHumanoidStats() {
        if(moveSpeed == 0) {
            moveSpeed = humanoidSO.moveSpeed;
        }

        if(workingSpeed == 0) {
            workingSpeed = humanoidSO.workingSpeed;
        }

        if(strength == 0) {
            strength = humanoidSO.strength;
        }

        if(carryCapacity == 0) {
            carryCapacity = humanoidSO.carryCapacity;
        }

        // hunger and happiness set to <0 if already initialized and empty
        if(hunger == 0) {
            hunger = 100;
        }

        if(energy == 0) {
            energy = 100;
        }
    }

}
