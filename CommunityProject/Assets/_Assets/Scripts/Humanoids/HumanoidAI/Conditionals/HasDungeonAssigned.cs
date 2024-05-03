using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasDungeonAssigned : Conditional {

    public HumanoidDungeonCrawl humanoidDungeonCrawl;

    public override void OnAwake() {
        humanoidDungeonCrawl = GetComponent<HumanoidDungeonCrawl>();
    }

    public override TaskStatus OnUpdate() {
        if (humanoidDungeonCrawl.GetDungeonEntranceAssigned() != null) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
    
}
