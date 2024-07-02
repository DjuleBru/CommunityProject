using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DungeonSO : ScriptableObject
{
    public string dungeonName;
    public DungeonManager.DungeonType dungeonType;
    public List<DungeonRoom> dungeonRoomPoolList;

    public int dungeonDifficulty;
    public int totalRoomNumber;

    public List<ItemSO> itemsFoundInDungeon;
    public List<GameObject> resourceNodesInDungeon;
    public int resourceNodesNumberInDungeon;
    public List<MobSO> commonMobsFoundInDungeon;
    public List<MobSO> rareMobsFoundInDungeon;
    public List<MobSO> epicMobsFoundInDungeon;
    public HumanoidSO humanoidTypeFoundInDungeon;
    public int humanoidsToSaveFromDungeon;
    public Sprite dungeonSprite;

    public HumanoidSO.HumanoidType proficiencyHumanoidType;
    public int recommendedHealth;
    public int recommendedDamage;
}
