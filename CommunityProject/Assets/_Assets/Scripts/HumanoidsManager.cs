using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidsManager : MonoBehaviour
{
    public static HumanoidsManager Instance { get; private set; }

    private List<Humanoid> humanoidsSaved;

    private void Awake() {
        Instance = this;
        humanoidsSaved = new List<Humanoid>();
    }

    public void AddHumanoidSaved(Humanoid humanoid) {
        if(humanoidsSaved.Contains(humanoid)) return;
        humanoidsSaved.Add(humanoid);
    }

    public List<Humanoid> GetHumanoids() {
        return humanoidsSaved;
    }

}
