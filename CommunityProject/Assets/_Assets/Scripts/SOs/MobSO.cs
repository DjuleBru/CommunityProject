using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MobSO : ScriptableObject
{
    public int mobDifficultyValue;
    public Transform mobToSpawnPrefab;
    public List<DungeonManager.DungeonType> foundInDungeonTypes;

    public int mobHP;
    public int mobMoveSpeed;

    public MobAttack.AttackType attackType;
    public int attackDmg;
    public float mobAttackRate;
    public float mobAttackDelay;
    public float mobAttackRange;

    public float mobAggroRange;
}
