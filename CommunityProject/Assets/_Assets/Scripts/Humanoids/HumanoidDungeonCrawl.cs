using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidDungeonCrawl : MonoBehaviour
{
    private DungeonEntrance dungeonEntranceAssigned;
    private HumanoidCarry humanoidCarry;
    private Humanoid humanoid;
    private float crawlTimer;

    private bool crawling;

    public event EventHandler OnCrawlStarted;
    public event EventHandler OnCrawlSuccess;

    private void Awake() {
        humanoidCarry = GetComponent<HumanoidCarry>();
        humanoid = GetComponent<Humanoid>();
    }

    private void Update() {
        if(crawling) {
            crawlTimer -= Time.deltaTime;

            if(crawlTimer < 0) {
                EndCrawl();
            }
        }
    }

    private void EndCrawl() {
        crawling = false;
        OnCrawlSuccess?.Invoke(this, EventArgs.Empty);
        foreach (Item item in dungeonEntranceAssigned.GetDungeonLootRecorded()) { 
            humanoidCarry.AddItemCarrying(item); 
        }
    }

    public void AssignDungeonEntrance(DungeonEntrance dungeonEtrance) {
        dungeonEntranceAssigned = dungeonEtrance;
        humanoid.AssignBuilding(dungeonEtrance.GetDungeonChest());
    }

    public DungeonEntrance GetDungeonEntranceAssigned() { 
        return dungeonEntranceAssigned;
    }

    public DungeonEntrance FindBestDungeonEntrance() {
        return DungeonEntranceManager.Instance.GetAllDungeonEntrances()[0];
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
    }

}
