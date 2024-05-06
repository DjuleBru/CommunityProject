using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCage : ResourceNode
{
    private Humanoid humanoid;

    public virtual void HitResourceNode() {
        FreeHumanoid();
    }

    public void SetHumanoid(Humanoid humanoid) {
        this.humanoid = humanoid;
    }

    private void FreeHumanoid() {
    }
}
