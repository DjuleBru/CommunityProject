using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    public enum Job {
        Unassigned,
        Dungeoneer,
        Worker,
        Haulier,
        Shipper
    }

    [SerializeField] private HumanoidSO humanoidSO;
    [SerializeField] private Job job;
    [SerializeField] private bool debugJob;

    private HumanoidWork humanoidWork;
    [SerializeField] private HumanoidVisual humanoidVisual;
    [SerializeField] private HumanoidInteraction humanoidInteraction;
    private Collider2D collider2D;

    private float workingSpeed;

    private Job jobAssigned;

    private void Awake() {
        humanoidWork = GetComponent<HumanoidWork>();
        collider2D = GetComponent<Collider2D>();
        if (debugJob) {
            jobAssigned = job;
        }

        workingSpeed = humanoidSO.workingSpeed;
    
    }
    private void Start() {
        humanoidWork.OnHumanoidWorkStarted += HumanoidWork_OnHumanoidWorkStarted;
        humanoidWork.OnHumanoidWorkStopped += HumanoidWork_OnHumanoidWorkStopped;
    }

    private void HumanoidWork_OnHumanoidWorkStopped(object sender, System.EventArgs e) {
        humanoidVisual.gameObject.SetActive(true);
        humanoidInteraction.gameObject.SetActive(false);
        collider2D.enabled = true;
    }

    private void HumanoidWork_OnHumanoidWorkStarted(object sender, System.EventArgs e) {
        humanoidVisual.gameObject.SetActive(false);
        humanoidInteraction.gameObject.SetActive(false);
        collider2D.enabled = false;
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
}
