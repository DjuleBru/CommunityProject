using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class CrawlInDungeon : Action
{
    public HumanoidDungeonCrawl humanoidCrawl;
    bool crawlSuccess;

    public override void OnAwake() {
        humanoidCrawl = GetComponent<HumanoidDungeonCrawl>();
    }

    public override void OnStart() {
        humanoidCrawl.OnCrawlSuccess += HumanoidCrawl_OnCrawlSuccess;

    }

    private void HumanoidCrawl_OnCrawlSuccess(object sender, System.EventArgs e) {
        crawlSuccess = true;
    }

    public override TaskStatus OnUpdate() {

        if(crawlSuccess) {
            crawlSuccess = false;
            return TaskStatus.Success;
        }

        if(humanoidCrawl.IsCrawling()) {
            crawlSuccess = false;
            return TaskStatus.Running;
        } else {
            return TaskStatus.Failure;
        }
    }
}
