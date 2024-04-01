using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class DungeonSO : ScriptableObject
{
    public string dungeonName;
    public DungeonManager.DungeonType dungeonType;

    public int dungeonDifficulty;
    public List<ItemSO> itemsFoundInDungeon;
    public List<MobSO> mobsFoundInDungeon;
}
