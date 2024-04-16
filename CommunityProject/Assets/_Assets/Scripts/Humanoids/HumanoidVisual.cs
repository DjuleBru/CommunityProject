using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVisual : MonoBehaviour
{
    private Humanoid humanoid;

    private void Awake() {
        humanoid = GetComponentInParent<Humanoid>();
    }

}
