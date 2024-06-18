using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousingBuildingUIWorld : MonoBehaviour {

    [SerializeField] private House house;
    [SerializeField] private Transform housedContainer;
    [SerializeField] private Transform housedTemplate;



    private void Awake() {
        ShowAssignedHoused(false);
    }

    private void Start() {
        house.OnAssignedHumanoidChanged += House_OnAssignedHumanoidChanged;
    }

    private void House_OnAssignedHumanoidChanged(object sender, System.EventArgs e) {
        RefreshAssignedHoused();
    }

    public void ShowAssignedHoused(bool active) {
        housedContainer.gameObject.SetActive(active);
        housedTemplate.gameObject.SetActive(active);

        RefreshAssignedHoused();
    }

    public void RefreshAssignedHoused() {
        housedTemplate.gameObject.SetActive(false);

        foreach (Transform child in housedContainer.transform) {
            if (child.transform == housedTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Humanoid humanoid in house.GetAssignedHumanoids()) {
            Transform assignedHaulerIcon = Instantiate(housedTemplate, housedContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HousedImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;
            assignedHaulerIcon.GetComponent<HousedHumanoidTemplate>().SetHumanoid(humanoid);

        }

        int remainingSpots = house.GetBuildingSO() .housingCapacity - house.GetAssignedHumanoids().Count;
        for(int i = 0; i < remainingSpots; i++) {
            Transform assignedHaulerIcon = Instantiate(housedTemplate, housedContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = .6f;

            assignedHaulerIcon.Find("HousedImage").GetComponent<Image>().color = Color.clear;
        }

    }

    public void RefreshAssignedHoused(Humanoid humanoidTargeted) {

        foreach (Transform child in housedContainer.transform) {
            if (child.transform == housedTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Humanoid humanoid in house.GetAssignedHumanoids()) {
            Transform assignedHaulerIcon = Instantiate(housedTemplate, housedContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HousedImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;

            if (humanoid == humanoidTargeted) {
                assignedHaulerIcon.Find("Outline").GetComponent<Image>().color = Color.green;
            } else {
                assignedHaulerIcon.GetComponent<HousedHumanoidTemplate>().SetHumanoid(humanoid);
            }
        }
    }

    public void ShowPotentialInputHousingAssign(Humanoid humanoidPotentiallyAssigned) {
        RefreshAssignedHoused(humanoidPotentiallyAssigned);

        if (!house.GetAssignedHumanoids().Contains(humanoidPotentiallyAssigned)) {
            Transform potentiallyAssignedHoused = Instantiate(housedTemplate, housedContainer);
            potentiallyAssignedHoused.gameObject.SetActive(true);
            potentiallyAssignedHoused.Find("HousedImage").GetComponent<Image>().sprite = humanoidPotentiallyAssigned.GetHumanoidSO().humanoidSprite;
            potentiallyAssignedHoused.GetComponent<CanvasGroup>().alpha = .6f;
        }
    }

}
