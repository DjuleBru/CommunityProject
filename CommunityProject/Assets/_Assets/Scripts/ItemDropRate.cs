using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;

[Serializable]
public class ItemDropRate
{
    public ItemSO itemSO;
    public float dropRate;
    public int maxDropAmount;
}
