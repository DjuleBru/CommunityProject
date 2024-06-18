using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class HumanoidSO : ScriptableObject
{

    public GameObject humanoidPrefab;

    public enum HumanoidType {
        Human,
        Elf,
        Dwarf,
        Goblin,
        Orc,
        Halfling
    }

    public HumanoidType humanoidType;
    public Sprite humanoidSprite;
    public AnimatorOverrideController animatorController;

    public float moveSpeed;
    public float intelligence;
    public float strength;
    public float health;
    public float damage;
    public float armor;

    public int carryCapacity;

}
