using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class MobSO : ScriptableObject
{
    public int mobDifficultyValue;
    public Transform mobToSpawnPrefab;
    public Sprite mobIconSprite;

    public int mobHP;
    public int mobMoveSpeed;

    public MobAttack.AttackType attackType;
    public int attackDmg;
    public float mobAttackRate;
    public float mobAttackDelay;
    public float mobMeleeAttackRange;
    public float mobRangedAttackRange;
    public float projectileSpeed;
    public int projectileDamage;
    public Sprite projectileSprite;
    public float rangedAttackAnimationDelay;
    public float mobAggroRange;

    public float meleeAttackKnockback;
    public float rangedAttackKnockback;

    public float dropProbability;
    public int maxItemDrops;
    public List<ItemDropRate> itemDropRateList;
}
