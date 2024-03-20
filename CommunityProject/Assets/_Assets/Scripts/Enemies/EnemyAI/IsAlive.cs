using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class IsAlive : Conditional {
    public Mob mob;

    public override void OnAwake() {
        mob = GetComponent<Mob>();
    }

    public override TaskStatus OnUpdate() {
        if (!mob.GetDead()) {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
