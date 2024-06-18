using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    private List<Humanoid> humanoidsAssigned = new List<Humanoid>();
    private List<bool> humanoidsSleeping = new List<bool>();

    [SerializeField] private ParticleSystem sleepingPS;
    [SerializeField] private HousingBuildingUIWorld housingBuildingUIWorld;

    public event EventHandler OnAssignedHumanoidChanged;

    public void AssignHumanoidHousing(Humanoid humanoid) {
        Debug.Log("Assigned " +  humanoid + " to " + this);
        humanoidsAssigned.Add(humanoid);
        humanoidsSleeping.Add(false);
        OnAssignedHumanoidChanged?.Invoke(this, EventArgs.Empty);
    } 

    public void UnassignHumanoidHousing(Humanoid humanoid) {
        humanoid.GetComponent<HumanoidNeeds>().UnAssignHousing();
        int humanoidIndex = humanoidsAssigned.IndexOf(humanoid);
        humanoidsSleeping.Remove(humanoidsSleeping[humanoidIndex]);

        humanoidsAssigned.Remove(humanoid);
        OnAssignedHumanoidChanged?.Invoke(this, EventArgs.Empty);
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

    public List<Humanoid> GetAssignedHumanoids() {
        return humanoidsAssigned;
    }

    public HousingBuildingUIWorld GetHousingBuildingUIWorld() {
        return housingBuildingUIWorld;
    }

    public override void OpenBuildingUI() {
        housingBuildingUIWorld.ShowAssignedHoused(true);
    }


    public override void ClosePanel() {
        housingBuildingUIWorld.ShowAssignedHoused(false);
    }
}
