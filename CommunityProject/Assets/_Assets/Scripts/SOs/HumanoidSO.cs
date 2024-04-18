using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class HumanoidSO : ScriptableObject
{
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
    public float moveSpeed;
    public float workingSpeed;

}