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

    public HumanoidType HumanType;
    public float moveSpeed;

}
