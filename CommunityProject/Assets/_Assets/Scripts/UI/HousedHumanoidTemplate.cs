using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousedHumanoidTemplate : MonoBehaviour
{
    private Humanoid humanoid;
    [SerializeField] private GameObject deAssignButton;

    private void Awake() {
        deAssignButton.GetComponent<Button>().onClick.AddListener(() => {
            RemoveHumanoidFromHouse();
        });

        deAssignButton.SetActive(false);
    }

    public void SetHumanoid(Humanoid humanoid) {
        this.humanoid = humanoid;
        deAssignButton.SetActive(true);
    }

    public void RemoveHumanoidFromHouse() {
        GetComponentInParent<House>().UnassignHumanoidHousing(humanoid);
        GetComponentInParent<HousingBuildingUIWorld>().RefreshAssignedHoused();
        humanoid.SetAutoAssign(false);
    }
}
