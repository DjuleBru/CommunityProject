using BehaviorDesigner.Runtime;
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

    [SerializeField] private HumanoidVisual humanoidVisual;
    [SerializeField] private HumanoidInteraction humanoidInteraction;
    private Collider2D collider2D;

    private string humanoidName;
    private string humanoidActionDesriprion;

    private float workingSpeed;

    private Job jobAssigned;
    private bool autoAssign = true;

    [SerializeField] private BehaviorDesigner.Runtime.BehaviorTree behaviorTree;
    [SerializeField] ExternalBehaviorTree workerBehaviorTree;
    [SerializeField] ExternalBehaviorTree haulierBehaviorTree;
    [SerializeField] ExternalBehaviorTree dungeoneerBehaviorTree;

    private void Awake() {
        behaviorTree = GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
        humanoidWork = GetComponent<HumanoidWork>();
        humanoidHaul = GetComponent<HumanoidHaul>();
        humanoidDungeonCrawl = GetComponent<HumanoidDungeonCrawl>();
        collider2D = GetComponent<Collider2D>();

        if (debugJob) {
            jobAssigned = job;
            AssignBehaviorTree();
        }

        humanoidName = HumanoidNames.GetRandomName();
        workingSpeed = humanoidSO.workingSpeed;
    }

    private void Start() {
        humanoidWork.OnHumanoidWorkStarted += HumanoidWork_OnHumanoidWorkStarted;
        humanoidWork.OnHumanoidWorkStopped += HumanoidWork_OnHumanoidWorkStopped;

        HumanoidsManager.Instance.AddHumanoidSaved(this);
    }

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

    public HumanoidSO GetHumanoidSO() {
        return humanoidSO;
    }

    public float GetWorkingSpeed() {
        return workingSpeed;
    }

    public Job GetJob() {
        return jobAssigned;
    }

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
    public void SetJob(Job job) {
        if (jobAssigned == job) return;

        StopTask();

        jobAssigned = job;
        AssignBehaviorTree();
    }


    public void AssignBehaviorTree() {
        if (jobAssigned == Job.Worker) {
            behaviorTree.ExternalBehavior = workerBehaviorTree;
            return;
        }

        if (jobAssigned == Job.Haulier) {
            behaviorTree.ExternalBehavior = haulierBehaviorTree;
            return;
        }

        if (jobAssigned == Job.Dungeoneer) {
            behaviorTree.ExternalBehavior = dungeoneerBehaviorTree;
            return;
        }
    }

    public void AssignBuilding(Building building) {
        assignedBuilding = building;
    }

    public void RemoveAssignedBuilding() {
        assignedBuilding = null;
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
}
