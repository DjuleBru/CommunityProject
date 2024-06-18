using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHaulersUI_World : MonoBehaviour
{
    [SerializeField] private GameObject outputInventoryFull;

    [SerializeField] private Building building;

    [SerializeField] private Transform assignedInputHaulersTemplate;
    [SerializeField] private Transform assignedInputHaulersContainer;
    [SerializeField] private Transform assignedOutputHaulersTemplate;
    [SerializeField] private Transform assignedOutputHaulersContainer;
    [SerializeField] private Image inputArrowImage;
    [SerializeField] private Image outputArrowImage;

    private void Awake() {
        outputInventoryFull.SetActive(false);

        assignedInputHaulersTemplate.gameObject.SetActive(false);
        assignedOutputHaulersTemplate.gameObject.SetActive(false);
        ShowAssignedHaulers(false);
    }

    public void SetOutputInventoryFull(bool full) {
        outputInventoryFull.SetActive(full);
    }

    public void ShowAssignedHaulers(bool active) {
        assignedOutputHaulersContainer.gameObject.SetActive(active);
        assignedInputHaulersContainer.gameObject.SetActive(active);
        inputArrowImage.gameObject.SetActive(active);
        outputArrowImage.gameObject.SetActive(active);

        RefreshAssignedHaulers();
    }

    public void RefreshAssignedHaulers() {

        foreach (Transform child in assignedInputHaulersContainer.transform) {
            if (child.transform == assignedInputHaulersTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (Transform child in assignedOutputHaulersContainer.transform) {
            if (child.transform == assignedOutputHaulersTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Humanoid humanoid in building.GetAssignedInputHauliersList()) {
            Transform assignedHaulerIcon = Instantiate(assignedInputHaulersTemplate, assignedInputHaulersContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HaulerImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;
        }

        foreach (Humanoid humanoid in building.GetAssignedOutputHauliersList()) {
            Transform assignedHaulerIcon = Instantiate(assignedOutputHaulersTemplate, assignedOutputHaulersContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HaulerImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;
        }
    }
    public void RefreshAssignedHaulers(Humanoid humanoidTargeted) {

        foreach (Transform child in assignedInputHaulersContainer.transform) {
            if (child.transform == assignedInputHaulersTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (Transform child in assignedOutputHaulersContainer.transform) {
            if (child.transform == assignedOutputHaulersTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Humanoid humanoid in building.GetAssignedInputHauliersList()) {
            Transform assignedHaulerIcon = Instantiate(assignedInputHaulersTemplate, assignedInputHaulersContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HaulerImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;

            if(humanoid == humanoidTargeted) {
                assignedHaulerIcon.Find("Outline").GetComponent<Image>().color = Color.green;
            }
        }

        foreach (Humanoid humanoid in building.GetAssignedOutputHauliersList()) {
            Transform assignedHaulerIcon = Instantiate(assignedOutputHaulersTemplate, assignedOutputHaulersContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HaulerImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;

            if (humanoid == humanoidTargeted) {
                assignedHaulerIcon.Find("Outline").GetComponent<Image>().color = Color.green;
            }
        }
    }
    public void ShowPotentialInputHaulerAssign(Humanoid humanoidPotentiallyAssigned) {
        RefreshAssignedHaulers(humanoidPotentiallyAssigned);

        if (!building.GetAssignedInputHauliersList().Contains(humanoidPotentiallyAssigned)) {
            Transform potentiallyAssignedHauler = Instantiate(assignedInputHaulersTemplate, assignedInputHaulersContainer);
            potentiallyAssignedHauler.gameObject.SetActive(true);
            potentiallyAssignedHauler.Find("HaulerImage").GetComponent<Image>().sprite = humanoidPotentiallyAssigned.GetHumanoidSO().humanoidSprite;
            potentiallyAssignedHauler.GetComponent<CanvasGroup>().alpha = .6f;
        }
    }

    public void ShowPotentialOutputHaulerAssign(Humanoid humanoidPotentiallyAssigned) {
        RefreshAssignedHaulers(humanoidPotentiallyAssigned);

        if(!building.GetAssignedOutputHauliersList().Contains(humanoidPotentiallyAssigned)) {
            Transform potentiallyAssignedHauler = Instantiate(assignedOutputHaulersTemplate, assignedOutputHaulersContainer);
            potentiallyAssignedHauler.gameObject.SetActive(true);
            potentiallyAssignedHauler.Find("HaulerImage").GetComponent<Image>().sprite = humanoidPotentiallyAssigned.GetHumanoidSO().humanoidSprite;
            potentiallyAssignedHauler.GetComponent<CanvasGroup>().alpha = .6f;
        }
    }
}
