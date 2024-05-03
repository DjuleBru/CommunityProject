using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonStatsBoardWorldUI : MonoBehaviour
{

    [SerializeField] private DungeonEntrance dungeonEntrance;

    [SerializeField] private Transform assignedDungeoneersTemplate;
    [SerializeField] private Transform assignedDungeoneersContainer;
    [SerializeField] private Image outputArrowImage;


    private void Awake() {
        assignedDungeoneersTemplate.gameObject.SetActive(false);
        ShowAssignedDungeoneers(false);
    }

    public void ShowAssignedDungeoneers(bool active) {
        assignedDungeoneersContainer.gameObject.SetActive(active);
        outputArrowImage.gameObject.SetActive(active);

        RefreshAssignedDungeoneers();
    }

    public void RefreshAssignedDungeoneers() {

        foreach (Transform child in assignedDungeoneersContainer.transform) {
            if (child.transform == assignedDungeoneersTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Humanoid humanoid in dungeonEntrance.GetHumanoidsAssigned()) {
            Transform assignedHaulerIcon = Instantiate(assignedDungeoneersTemplate, assignedDungeoneersContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HaulerImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;
        }
    }

    public void RefreshAssignedDungeoneers(Humanoid humanoidTargeted) {

        foreach (Transform child in assignedDungeoneersContainer.transform) {
            if (child.transform == assignedDungeoneersTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Humanoid humanoid in dungeonEntrance.GetHumanoidsAssigned()) {
            Transform assignedHaulerIcon = Instantiate(assignedDungeoneersTemplate, assignedDungeoneersContainer);
            assignedHaulerIcon.gameObject.SetActive(true);
            assignedHaulerIcon.GetComponent<CanvasGroup>().alpha = 1f;
            assignedHaulerIcon.Find("HaulerImage").GetComponent<Image>().sprite = humanoid.GetHumanoidSO().humanoidSprite;

            if (humanoid == humanoidTargeted) {
                assignedHaulerIcon.Find("Outline").GetComponent<Image>().color = Color.green;
            }
        }

    }

    public void ShowPotentialDungeoneersAssigned(Humanoid humanoidPotentiallyAssigned) {
        RefreshAssignedDungeoneers(humanoidPotentiallyAssigned);

        assignedDungeoneersContainer.gameObject.SetActive(true);
        outputArrowImage.gameObject.SetActive(true);

        if (!dungeonEntrance.GetHumanoidsAssigned().Contains(humanoidPotentiallyAssigned)) {
            Transform potentiallyAssignedHauler = Instantiate(assignedDungeoneersTemplate, assignedDungeoneersContainer);
            potentiallyAssignedHauler.gameObject.SetActive(true);
            potentiallyAssignedHauler.Find("HaulerImage").GetComponent<Image>().sprite = humanoidPotentiallyAssigned.GetHumanoidSO().humanoidSprite;
            potentiallyAssignedHauler.GetComponent<CanvasGroup>().alpha = .6f;
        }

    }

}
