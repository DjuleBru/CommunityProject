using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class HumanoidNeeds : MonoBehaviour {

    private Building bestFoodSourceBuilding;
    private Building assignedHousing;

    private Humanoid humanoid;
    private HumanoidVisual humanoidVisual;
    private HumanoidAnimatorManager humanoidAnimatorManager;

    private Item itemEating;

    private float hunger;
    private float eatingRate = 1f;
    private float energy;

    private bool hungry;
    private bool fullBelly;
    private bool sleeping;
    
    private bool exhausted;
    private bool fullEnergy;

    [SerializeField] private float hungerDepletionRate;
    [SerializeField] private float energyDepletionRate;
    [SerializeField] private float baseHealRate;

    private float energyFillRate;
    private float housingAssignmentAttemptRate = .5f;
    private float housingAssignmentAttemptTimer;

    private void Awake() {
        humanoid = GetComponent<Humanoid>();
        humanoidVisual = GetComponentInChildren<HumanoidVisual>();
        humanoidAnimatorManager = GetComponentInChildren<HumanoidAnimatorManager>();
    }

    private void Start() {
        InitializeHumanoidNeedStats();
    }

    private void Update() {

        TryAssignHousing();

        RefreshHungerStatuses();
        RefreshEnergyStatuses();

        if(hunger < 10) {
            humanoid.StopTask();
        }
        if(energy < 10) {
            humanoid.StopTask();
        }

    }

    private void TryAssignHousing() {
        if (assignedHousing != null) return;
        if (!humanoid.GetAutoAssign()) return;

        housingAssignmentAttemptTimer += Time.deltaTime;
        if(housingAssignmentAttemptTimer >= housingAssignmentAttemptRate) {
            housingAssignmentAttemptTimer = 0;
            AssignHousing();
        }
    }

    #region HUNGER

    private void RefreshHungerStatuses() {
        if(hunger > 0) {
            hunger -= hungerDepletionRate * Time.deltaTime;
        }

        if (hunger < 10) {
            if (!hungry) {
                fullBelly = false;
                hungry = true;
                humanoidVisual.SetHungryStatusActive(true);
            }
        }
        else {
            if (hungry) {
                hungry = false;
                humanoidVisual.SetHungryStatusActive(false);
            }
        }

        if (hunger > 90) {
            if (!fullBelly) {
                StopEating();
                fullBelly = true;
            }
        }
    }

    public Building IdentifyBestFoodBuilding() {

        List<Building> sourceBuildingsList = BuildingsManager.Instance.GetFoodDistributionBuildingsList();

        float bestBuildingScore = 0f;
        Building bestFoodBuilding = null;

        foreach (Building building in sourceBuildingsList) {
            float score = 1 / Vector3.Distance(transform.position, building.transform.position);

            if (score > bestBuildingScore) {
                bestBuildingScore = score;
                bestFoodBuilding = building;
            }
        }

        bestFoodSourceBuilding = bestFoodBuilding;
        return bestFoodBuilding;
    }

    public bool FetchFoodInBuilding() {
        List<Item> foodItems = ItemAssets.Instance.GetItemListOfCategory(Item.ItemCategory.Food);
        humanoid.SetHumanoidActionDescription("Going to eat.");

        if (bestFoodSourceBuilding is Chest) {

            Chest chest = bestFoodSourceBuilding as Chest;

            foreach (Item item in foodItems) {

                if (chest.GetChestInventory().HasItem(item)) {

                    itemEating = new Item { itemType = item.itemType, amount = 1 };
                    chest.GetChestInventory().RemoveItemAmount(itemEating);
                    humanoidVisual.SetEating(itemEating);

                    return true;
                }

            }
        }
        return false;
    }

    public Building GetBestFoodSourceBuilding() {
        return bestFoodSourceBuilding;
    }

    public void Eat() {
        Feed(ItemAssets.Instance.GetItemSO(itemEating.itemType).foodValue);
    }

    public void Feed(float hungerAddition) {
        hunger += hungerAddition;
    }

    public void StopEating() {
        humanoidVisual.StopEating();
    }

    public void FillEnergy() {
        energy = 100;
    }

    public float GetHunger() {
        return hunger;
    }

    public bool GetFullBelly() {
        return fullBelly;
    }

    #endregion

    #region ENERGY
    private void RefreshEnergyStatuses() {
        if(!sleeping && energy > 0) {
            energy -= energyDepletionRate * Time.deltaTime;
        } else {
            energy += energyDepletionRate * Time.deltaTime;
        }

        if (energy < 10) {
            if (!exhausted) {
                exhausted = true;
                humanoidVisual.SetExhaustedStatusActive(true);
            }
        }

        if (energy >= 100) {
            if (exhausted) {
                exhausted = false;
                humanoidVisual.SetExhaustedStatusActive(false);
                SetSleeping(false);
            }
        }
    }

    public bool AssignHousing() {

        List<Building> housingBuildingsList = BuildingsManager.Instance.GetHousingBuildingsList();

        foreach (Building building in housingBuildingsList) {
            House house = building as House;

            if(house.GetHousedHumanoidsNumber() < house.GetBuildingSO().housingCapacity) {
                house.AssignHumanoidHousing(humanoid);
                assignedHousing = house;
                energyFillRate *= house.GetBuildingSO().energyFillRateMultiplier;
                return true;
            }
        }
        return false;
    }

    public void AssignHousingManual(House house) {
        assignedHousing = house;
    }

    public void UnAssignHousing() {
        assignedHousing = null;
        energyFillRate = energyDepletionRate * 2f;
    }
    public Building GetAssignedHousing() {
        return assignedHousing;
    }


    public void SetSleeping(bool sleeping) {
        this.sleeping = sleeping;

        humanoidVisual.SetSleeping(sleeping);
        humanoidAnimatorManager.PauseAnimator(sleeping);

        if (assignedHousing != null) {
            House house = assignedHousing as House;

            if (sleeping) {
                humanoidVisual.HideVisual();
                house.SetSleeping(humanoid, true);
            }
            else {
                humanoidVisual.ShowVisual();
                house.SetSleeping(humanoid, false);
            }
        } else {
            if (sleeping) {
                humanoidVisual.SetSleepingPS(true);
            }
            else {
                humanoidVisual.SetSleepingPS(false);
            }
        }
    }

    public bool GetSleeping() {  return sleeping; }

    public float GetEnergy() {
        return energy;
    }

    public bool GetExhausted() { return exhausted; }    

    #endregion

    public float GetHealRate() {
        if(assignedHousing != null) {
            return baseHealRate * assignedHousing.GetBuildingSO().healRate;
        } else {
            return baseHealRate;
        }
    }
    public void InitializeHumanoidNeedStats() {
        // hunger and happiness set to <0 if already initialized and empty
        if (hunger == 0) {
            hunger = 100;
        }

        if (energy == 0) {
            energy = 100;
        }

        if(energyFillRate == 0) {
            energyFillRate = energyDepletionRate * 2;
        }
    }

    public float GetEatingRate() {
        return eatingRate;
    }
}
