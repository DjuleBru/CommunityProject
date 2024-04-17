using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector3 Randomize2DPoint(Vector3 initialPoint, float randomizer) {
        return initialPoint + new Vector3(Random.Range(-randomizer, randomizer), Random.Range(-randomizer, randomizer), 0);
    }
}
