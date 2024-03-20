using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static Vector3 Randomize2DPoint(Vector3 initialPoint, float randomizer) {
        return initialPoint + new Vector3(Random.Range(0,randomizer), Random.Range(0,randomizer), 0);
    }
}
