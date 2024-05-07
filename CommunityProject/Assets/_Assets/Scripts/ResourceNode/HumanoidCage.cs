using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCage : ResourceNode
{
    [SerializeField] private GameObject humanoid;
    [SerializeField] private Transform humanoidSpawnPoint;

    public override void HitResourceNode() {
        FreeHumanoid();
    }

    public void SetHumanoidGameObject(GameObject humanoid) {
        this.humanoid = humanoid;
    }

    private void FreeHumanoid() {
        Instantiate(humanoid, humanoidSpawnPoint.transform.position, Quaternion.identity);
        InvokeResourceNodeDepleted();
    }
}
