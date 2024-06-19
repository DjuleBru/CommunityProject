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
    public List<Building.BuildingWorksCategory> humanoidProficiencies;

    public Sprite humanoidSprite;
    public AnimatorOverrideController animatorController;

    public float moveSpeed;
    public float intelligence;
    public float strength;
    public float agility;
    public float maxHealth;
    public float damage;
    public float armor;

    public int carryCapacity;

}
