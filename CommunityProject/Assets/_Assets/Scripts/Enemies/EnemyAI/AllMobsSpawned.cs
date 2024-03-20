using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AllMobsSpawned : Conditional {

    private Mob mob;

    public override void OnAwake() {
        mob = GetComponent<Mob>();
    }

    public override TaskStatus OnUpdate() {
        if (mob.GetAllMobsSpawned()) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
