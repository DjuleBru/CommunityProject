using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    private List<Humanoid> humanoidsAssigned = new List<Humanoid>();
    private List<bool> humanoidsSleeping = new List<bool>();

    [SerializeField] private ParticleSystem sleepingPS;

    public void AssignHumanoidHousing(Humanoid humanoid) {
        humanoidsAssigned.Add(humanoid);
        humanoidsSleeping.Add(false);
    } 

    public int GetHousedHumanoidsNumber() {
        return humanoidsAssigned.Count;
    }

    public void SetSleeping(Humanoid humanoid, bool sleeping) {
        int humanoidIndex = humanoidsAssigned.IndexOf(humanoid);

        humanoidsSleeping[humanoidIndex] = sleeping;

        bool atLeastOneHumanoidSleeping = false;
        foreach(bool sleepingHuman in humanoidsSleeping) {
            if(sleepingHuman) {
                atLeastOneHumanoidSleeping = true;
            }
        }

        if(atLeastOneHumanoidSleeping) {
            sleepingPS.Play();
        } else {
            sleepingPS.Stop();
        }

    }
}
