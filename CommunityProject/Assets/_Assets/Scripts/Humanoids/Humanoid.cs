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

    public enum Stat {
        strength,
        intelligence,
        agility,
        speed,
        health,
        damage,
        armor
    }

    protected Building assignedBuilding;

    [SerializeField] private float roamDistanceToBuilding;

    [SerializeField] private HumanoidSO humanoidSO;
    [SerializeField] private Job job;
    [SerializeField] private bool debugJob;

    private HumanoidWork humanoidWork;
    private HumanoidHaul humanoidHaul;
    private HumanoidNeeds humanoidNeeds;
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
    private float agility;
    private float damage;
    private float armor;

    private int carryCapacity;

    private Job jobAssigned;
    private bool autoAssign = true;
    private bool autoAssignBestEquipment = true;
    private bool freedFromDungeon;

    private float health;
    private float maxHealth;
    private bool healing;
    public event EventHandler OnHealingStarted;
    public event EventHandler OnHealingStopped;


    [SerializeField] private BehaviorDesigner.Runtime.BehaviorTree behaviorTree;

    public event EventHandler OnEquipmentChanged;
    private Item mainHandItem;
    private Item secondaryHandItem;
    private Item helmetItem;
    private Item bootsItem;
    private Item necklaceItem;
    private Item ringItem;

    private float mainHandItemDurability;
    private float secondaryHandItemDurability;
    private float helmetItemDurability;
    private float bootsItemDurability;
    private float necklaceItemDurability;
    private float ringItemDurability;

    private float mainHandItemMaxDurability;
    private float secondaryHandItemMaxDurability;
    private float helmetItemMaxDurability;
    private float bootsItemMaxDurability;
    private float necklaceItemMaxDurability;
    private float ringItemMaxDurability;

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
        humanoidNeeds = GetComponent<HumanoidNeeds>();

        collider2D = GetComponent<Collider2D>();

        if (debugJob) {
            jobAssigned = job;
            AssignBehaviorTree();
        }

        if (humanoidName == null) {
            humanoidName = HumanoidNames.GetRandomName(humanoidSO.humanoidType);
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
        HandleEquipmentDurability();
    }

    private void HandleEquipmentDurability() {

        if(mainHandItem != null && mainHandItem.amount > 0) {
            mainHandItemDurability -= Time.deltaTime;

            if(mainHandItemDurability < 0 ) {
                mainHandItem.amount --;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (mainHandItem.amount == 0) {
                    mainHandItemDurability = 0;
                }
                else {
                    mainHandItemDurability = mainHandItemMaxDurability;
                }
            }
        }

        if (secondaryHandItem != null && secondaryHandItem.amount > 0) {
            secondaryHandItemDurability -= Time.deltaTime;

            if (secondaryHandItemDurability < 0) {
                secondaryHandItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);

                if (secondaryHandItem.amount == 0) {
                    secondaryHandItemDurability = 0;
                }
                else {
                    secondaryHandItemDurability = secondaryHandItemMaxDurability;
                }
            }
        }

        if (helmetItem != null && helmetItem.amount > 0) {
            helmetItemDurability -= Time.deltaTime;

            if (helmetItemDurability < 0) {
                helmetItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (helmetItem.amount == 0) {
                    helmetItemDurability = 0;
                }
                else {
                    helmetItemDurability = helmetItemMaxDurability;
                }
            }
        }

        if (bootsItem != null && bootsItem.amount > 0) {
            bootsItemDurability -= Time.deltaTime;

            if (bootsItemDurability < 0) {
                bootsItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (bootsItem.amount == 0) {
                    bootsItemDurability = 0;
                }
                else {
                    bootsItemDurability = bootsItemMaxDurability;
                }
            }
        }

        if (necklaceItem != null && necklaceItem.amount > 0) {
            necklaceItemDurability -= Time.deltaTime;

            if (necklaceItemDurability < 0) {
                necklaceItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (necklaceItem.amount == 0) {
                    necklaceItemDurability = 0;
                }
                else {
                    necklaceItemDurability = necklaceItemMaxDurability;
                }
            }
        }

        if (ringItem != null && ringItem.amount > 0) {
            ringItemDurability -= Time.deltaTime;

            if (ringItemDurability < 0) {
                ringItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);

                if (ringItem.amount == 0) {
                    ringItemDurability = 0;
                } else {
                    ringItemDurability = ringItemMaxDurability;
                }
            }
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

    public void SetAutoAssignBestEquipment(bool autoAssignActive) {
        if (!autoAssignActive && !autoAssign) return;
        autoAssignBestEquipment = autoAssignActive;

        //StopTask();
        //assignedBuilding = null;
    }

    public void SetHumanoidActionDescription(string description) {
        humanoidActionDesriprion = description;
        ProductionBuildingUI.Instance.RefreshWorkerPanel();
    }
    
    public void SetEquipmentType(Item item) {
        StopTask();
        Item.ItemEquipmentCategory category = ItemAssets.Instance.GetItemSO(item.itemType).itemEquipmentCategory;

        if (category == Item.ItemEquipmentCategory.main) {
            if (mainHandItem != null && equippedItems.Contains(mainHandItem)) {
                equippedItems.Remove(mainHandItem);
            }
            mainHandItem = item;
            mainHandItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            mainHandItemMaxDurability = mainHandItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.secondary) {
            if (secondaryHandItem != null && equippedItems.Contains(secondaryHandItem)) {
                equippedItems.Remove(secondaryHandItem);
            }

            secondaryHandItem = item;
            secondaryHandItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            secondaryHandItemMaxDurability = secondaryHandItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.head) {
            if (helmetItem != null && equippedItems.Contains(helmetItem)) {
                equippedItems.Remove(helmetItem);
            }

            helmetItem = item;
            helmetItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            helmetItemMaxDurability = helmetItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.boots) {
            if (bootsItem != null && equippedItems.Contains(bootsItem)) {
                equippedItems.Remove(bootsItem);
            }

            bootsItem = item;
            bootsItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            bootsItemMaxDurability = bootsItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.ring) {
            if (ringItem != null && equippedItems.Contains(ringItem)) {
                equippedItems.Remove(ringItem);
            }

            ringItem = item;
            ringItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            ringItemMaxDurability = ringItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.necklace) {
            if (necklaceItem != null && equippedItems.Contains(necklaceItem)) {
                equippedItems.Remove(necklaceItem);
            }

            necklaceItem = item;
            necklaceItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            necklaceItemMaxDurability= necklaceItemDurability;
        }

        equippedItems.Add(item);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealth(float health) {
        this.health = health;
    }

    public void Heal() {
        if(!healing) {
            healing = true;
            OnHealingStarted?.Invoke(this, EventArgs.Empty);
        }
        health += Time.deltaTime * humanoidNeeds.GetHealRate() / 10;
    }

    public void StopHealing() {
        OnHealingStopped?.Invoke(this, EventArgs.Empty);
        healing = false;
    }

    #endregion

    #region GET PARAMETERS

    public HumanoidSO GetHumanoidSO() {
        return humanoidSO;
    }

    public float GetHealth() {
        return health;
    }

    public float GetHealthNormalized() {
        return health /  maxHealth;
    }

    public float GetIntelligence() {
        float totalIntelligence = intelligence;

        foreach(Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalIntelligence += ItemAssets.Instance.GetItemSO(item.itemType).intelligenceBonusValue;
        }

        return totalIntelligence;
    }

    public float GetAgility() {
        float totalAgility = agility;

        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalAgility += ItemAssets.Instance.GetItemSO(item.itemType).agilityBonusValue;
        }

        return totalAgility;
    }

    public float GetMoveSpeed() {
        float totalMoveSpeed = moveSpeed;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalMoveSpeed += ItemAssets.Instance.GetItemSO(item.itemType).moveSpeedBonusValue;
        }

        return totalMoveSpeed;
    }

    public float GetMaxHealth() {
        float totalHealth = maxHealth;
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

    public bool GetAutoAssignBestEquipment() {
        return autoAssignBestEquipment;
    }

    public Item GetEquipmentItem(Item.ItemEquipmentCategory category) {
        Item equippedItem = null;

        if(category == Item.ItemEquipmentCategory.main && mainHandItem != null && mainHandItem.itemType != Item.ItemType.Wood) {
            equippedItem = mainHandItem;
        }
        if (category == Item.ItemEquipmentCategory.secondary && secondaryHandItem != null && secondaryHandItem.itemType != Item.ItemType.Wood) {
            equippedItem = secondaryHandItem;
        }
        if (category == Item.ItemEquipmentCategory.head && helmetItem != null && helmetItem.itemType != Item.ItemType.Wood) {
            equippedItem = helmetItem;
        }
        if (category == Item.ItemEquipmentCategory.boots && bootsItem != null && bootsItem.itemType != Item.ItemType.Wood) {
            equippedItem = bootsItem;
        }
        if (category == Item.ItemEquipmentCategory.ring && ringItem != null && ringItem.itemType != Item.ItemType.Wood) {
            equippedItem = ringItem;
        }
        if (category == Item.ItemEquipmentCategory.necklace && necklaceItem != null && necklaceItem.itemType != Item.ItemType.Wood) {
            equippedItem = necklaceItem;
        }

        return equippedItem;
    }

    public float GetEquipmentItemDurabilityNormalized(Item.ItemEquipmentCategory category) {
        if (category == Item.ItemEquipmentCategory.main) {
            return mainHandItemDurability / mainHandItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.secondary) {
            return secondaryHandItemDurability / secondaryHandItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.head) {
            return helmetItemDurability / helmetItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.boots) {
            return bootsItemDurability / bootsItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.ring) {
            return ringItemDurability / ringItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.necklace) {
            return necklaceItemDurability / necklaceItemMaxDurability;
        }
        return 0;
    }

    public Item GetMainHandItem() {
        if (mainHandItem != null && mainHandItem.itemType == Item.ItemType.Wood) return null;
        return mainHandItem;
    }

    public Item GetSecondaryHandItem() {
        if (secondaryHandItem != null && secondaryHandItem.itemType == Item.ItemType.Wood) return null;
        return secondaryHandItem;
    }
    public Item GetHeadItem() {
        if (helmetItem != null && helmetItem.itemType == Item.ItemType.Wood) return null;
        return helmetItem;
    }
    public Item GetBootsItem() {
        if (bootsItem != null && bootsItem.itemType == Item.ItemType.Wood) return null;
        return bootsItem;
    }
    public Item GetNecklaceItem() {
        if (necklaceItem != null && necklaceItem.itemType == Item.ItemType.Wood) return null;
        return necklaceItem;
    }
    public Item GetRingItem() {
        if (ringItem != null && ringItem.itemType == Item.ItemType.Wood) return null;
        return ringItem;
    }

    #endregion
    public void StopTask() {
        if (jobAssigned == Job.Worker) {
            humanoidWork.StopWorking();
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

        if (agility == 0) {
            agility = humanoidSO.agility;
        }

        if (strength == 0) {
            strength = humanoidSO.strength;
        }

        if (maxHealth == 0) {
            maxHealth = humanoidSO.maxHealth;
            health = maxHealth;
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
