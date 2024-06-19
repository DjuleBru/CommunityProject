using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidDungeonCrawl : MonoBehaviour
{
    private DungeonEntrance dungeonEntranceAssigned;
    private HumanoidCarry humanoidCarry;
    private Humanoid humanoid;
    [SerializeField] private float crawlTimer;

    [SerializeField] private bool crawling;

    public event EventHandler OnCrawlStarted;
    public event EventHandler OnCrawlSuccess;
    private float health;

    private void Awake() {
        humanoidCarry = GetComponent<HumanoidCarry>();
        humanoid = GetComponent<Humanoid>();
    }

    private void Update() {
        if(crawling) {
            crawlTimer -= Time.deltaTime;

            float healthLoss = Time.deltaTime / 50 * dungeonEntranceAssigned.GetDungeonSO().recommendedHealth;
            float newHealth = humanoid.GetHealth() - healthLoss;
            humanoid.SetHealth(newHealth);

            if(crawlTimer < 0 || newHealth <= 0) {
                EndCrawl(crawlTimer);
            }
        }
    }

    private void EndCrawl(float crawlTimer) {
        crawling = false;
        OnCrawlSuccess?.Invoke(this, EventArgs.Empty);

        foreach (Item item in dungeonEntranceAssigned.GetDungeonLootRecorded()) {

            int itemAmount = item.amount;
            float itemAmountBuffDueToDamageStat = itemAmount * ((humanoid.GetDamage() - dungeonEntranceAssigned.GetDungeonSO().recommendedDamage) / 10);
            float itemAmountDeBuffDueToUnfinishedDungeon = 0;

            if (crawlTimer > 0) {
                itemAmountDeBuffDueToUnfinishedDungeon = itemAmount * crawlTimer/ dungeonEntranceAssigned.GetDungeonTimerRecorded();
            } 

            Debug.Log("item debuff due to damage " + itemAmountBuffDueToDamageStat);
            Debug.Log("item debuff due to unfinished dungeon " + itemAmountDeBuffDueToUnfinishedDungeon);

            float itemAmountLooted = itemAmount + itemAmountBuffDueToDamageStat - itemAmountDeBuffDueToUnfinishedDungeon;

            Item itemLooted = new Item { itemType = item.itemType, amount = (int)itemAmountLooted };
            humanoidCarry.AddItemCarrying(itemLooted); 
        }

    }

    public void AssignDungeonEntrance(DungeonEntrance dungeonEtrance) {
        dungeonEntranceAssigned = dungeonEtrance;
        if(dungeonEntranceAssigned != null) {
            humanoid.AssignBuilding(dungeonEtrance.GetDungeonChest());
        }
    }

    public DungeonEntrance GetDungeonEntranceAssigned() { 
        return dungeonEntranceAssigned;
    }

    public DungeonEntrance FindBestDungeonEntrance() {

        foreach(DungeonEntrance dungeonEntrance in DungeonEntranceManager.Instance.GetAllDungeonEntrances()) {
            if(dungeonEntrance.GetDungeonComplete()) { 
                return dungeonEntrance; 
            }
        }
        return null;
    }

    public void StartCrawling() {
        crawling = true;
        crawlTimer = dungeonEntranceAssigned.GetDungeonTimerRecorded();
        OnCrawlStarted?.Invoke(this, EventArgs.Empty);
    }

    public bool IsCrawling() {
        return crawling;
    }

    public void StopCrawling() {
        crawling = false;
        OnCrawlSuccess?.Invoke(this, EventArgs.Empty);
        humanoidCarry.ClearItemsCarryingList();

        if(dungeonEntranceAssigned != null) {
            dungeonEntranceAssigned.DeAssignHumanoid(humanoid);
            dungeonEntranceAssigned = null;
        }

        if(humanoid.GetAssignedBuilding() != null) {
            humanoid.RemoveAssignedBuilding();
        }
    }

    public void LoadHumanoidDungeonCrawl() {
        string humanoidID = humanoid.GetInstanceID().ToString();
        dungeonEntranceAssigned = ES3.Load(humanoidID + "dungeonEntranceAssigned", dungeonEntranceAssigned);
        crawling = ES3.Load(humanoidID + "crawling", crawling);
        crawlTimer = ES3.Load(humanoidID + "crawlTimer", crawlTimer);
    }

    public void SaveHumanoidDungeonCrawl() {
        string humanoidID = humanoid.GetInstanceID().ToString();
        ES3.Save(humanoidID + "dungeonEntranceAssigned", dungeonEntranceAssigned);
        ES3.Save(humanoidID + "crawling", crawling);
        ES3.Save(humanoidID + "crawlTimer", crawlTimer);
    }

}
