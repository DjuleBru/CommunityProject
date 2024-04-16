using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
    [SerializeField] private HumanoidSO humanoidSO;

    public HumanoidSO GetHumanoidSO() {
        return humanoidSO;
    }
}
