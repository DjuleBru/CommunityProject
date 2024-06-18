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

    private float strength;
    private float intelligence;
    private float moveSpeed;
    private float health;
    private float damage;
    private float armor;

    private int carryCapacity;

    private Job jobAssigned;
    private bool autoAssign = true;
    private bool freedFromDungeon;

    [SerializeField] private BehaviorDesigner.Runtime.BehaviorTree behaviorTree;

    public event EventHandler OnEquipmentChanged;
    private Item mainHandItem;
    private Item secondaryHandItem;
    private Item helmetItem;
    private Item bootsItem;
    private Item necklaceItem;
    private Item ringItem;

    private List<Item> equippedItems = new List<Item>();

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

        //StopTask();
        //assignedBuilding = null;
    }

    public void SetHumanoidActionDescription(string description) {
        humanoidActionDesriprion = description;
        ProductionBuildingUI.Instance.RefreshWorkerPanel();
    }

    public void SetMainHandItem(Item item) {
        StopTask();

        if(mainHandItem != null && equippedItems.Contains(mainHandItem)) {
            equippedItems.Remove(mainHandItem);
        }

        mainHandItem = item;

        equippedItems.Add(mainHandItem);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSecondaryHandItem(Item item) {
        StopTask();

        if (secondaryHandItem != null && equippedItems.Contains(secondaryHandItem)) {
            equippedItems.Remove(secondaryHandItem);
        }

        secondaryHandItem = item;
        equippedItems.Add(secondaryHandItem);

        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetHeadItem(Item item) {
        StopTask();

        if (helmetItem != null && equippedItems.Contains(helmetItem)) {
            equippedItems.Remove(helmetItem);
        }

        helmetItem = item;
        equippedItems.Add(helmetItem);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetBootsItem(Item item) {
        StopTask();

        if (bootsItem != null && equippedItems.Contains(bootsItem)) {
            equippedItems.Remove(bootsItem);
        }

        bootsItem = item;
        equippedItems.Add(bootsItem);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetNecklaceItem(Item item) {
        StopTask();

        if (necklaceItem != null && equippedItems.Contains(necklaceItem)) {
            equippedItems.Remove(necklaceItem);
        }

        necklaceItem = item;
        equippedItems.Add(necklaceItem);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetRingItem(Item item) {
        StopTask();

        if (ringItem != null && equippedItems.Contains(ringItem)) {
            equippedItems.Remove(ringItem);
        }

        ringItem = item;
        equippedItems.Add(ringItem);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    #endregion

    #region GET PARAMETERS

    public HumanoidSO GetHumanoidSO() {
        return humanoidSO;
    }

    public float GetIntelligence() {
        float totalIntelligence = intelligence;

        foreach(Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalIntelligence += ItemAssets.Instance.GetItemSO(item.itemType).intelligenceBonusValue;
        }

        return totalIntelligence;
    }

    public float GetMoveSpeed() {
        float totalMoveSpeed = moveSpeed;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalMoveSpeed += ItemAssets.Instance.GetItemSO(item.itemType).moveSpeedBonusValue;
        }

        return totalMoveSpeed;
    }

    public float GetHealth() {
        float totalHealth = health;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalHealth += ItemAssets.Instance.GetItemSO(item.itemType).healthBonusValue;
        }
        return totalHealth;
    }

    public float GetStrength() {
        float totalStrength = strength;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalStrength += ItemAssets.Instance.GetItemSO(item.itemType).strengthBonusValue;
        }
        return totalStrength;
    }

    public float GetArmor() {
        float totalArmor = armor;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalArmor += ItemAssets.Instance.GetItemSO(item.itemType).armorBonusValue;
        }
        return totalArmor;
    }

    public float GetDamage() {
        float totalDamage = damage;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalDamage += ItemAssets.Instance.GetItemSO(item.itemType).damageBonusValue;
        }
        return totalDamage;
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

    public Item GetMainHandItem() {
        return mainHandItem;
    }
    public Item GetSecondaryHandItem() {
        return secondaryHandItem;
    }
    public Item GetHeadItem() {
        return helmetItem;
    }
    public Item GetBootsItem() {
        return bootsItem;
    }
    public Item GetNecklaceItem() {
        return necklaceItem;
    }
    public Item GetRingItem() {
        return ringItem;
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

    public void ChangeIntelligence(float intelligenceAddition) {
        intelligence += intelligenceAddition;
    }

    public void MultiplyIntelligence(float intelligenceMultiplier) {
        intelligence *= intelligenceMultiplier;
    }

    public void ChangeStrength(float strengthAddition) {
        strength += strengthAddition;
    }

    public void ChangeArmor(float armorAddition) {
        armor += armorAddition;
    }

    public void ChangeDamage(float damageAddition) {
        damage += damageAddition;
    }


    #endregion

    public void StopTask() {
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

        if(intelligence == 0) {
            intelligence = humanoidSO.intelligence;
        }

        if(strength == 0) {
            strength = humanoidSO.strength;
        }

        if (health == 0) {
            health = humanoidSO.health;
        }

        if (damage == 0) {
            damage = humanoidSO.damage;
        }

        if (armor == 0) {
            armor = humanoidSO.armor;
        }

        if (carryCapacity == 0) {
            carryCapacity = humanoidSO.carryCapacity;
        }
    }

}
