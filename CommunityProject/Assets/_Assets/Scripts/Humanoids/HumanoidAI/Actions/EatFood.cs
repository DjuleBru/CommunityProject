using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class EatFood : Action {

    public HumanoidNeeds humanoidNeeds;

    private float eatTimer;
    private float eatRate;

    public override void OnAwake() {
        humanoidNeeds = GetComponent<HumanoidNeeds>();
        eatRate = humanoidNeeds.GetEatingRate();
    }

    public override TaskStatus OnUpdate() {

        eatTimer += Time.deltaTime;

        if (eatTimer >= eatRate) {
            eatTimer = 0;
            humanoidNeeds.Eat();
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

}
