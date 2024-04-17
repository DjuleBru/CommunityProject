using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidNames
{
    public static List<string> Names = new List<string> {
        "Duralf",
        "Bricketeer",
        "FartyFive",
        "PeanutCream",
    };

    public static string GetRandomName() {
        return Names[Random.Range(0, Names.Count)];
    }
}
