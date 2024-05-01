using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidTemplateUI : MonoBehaviour
{

    private Humanoid humanoid;

    [SerializeField] private Image humanoidIcon;
    [SerializeField] private TextMeshProUGUI humanoidJobText;
    [SerializeField] private TextMeshProUGUI humanoidNameText;

    [SerializeField] private Image humanoidJobBackgroundSpriteRenderer;
    [SerializeField] private Image assignmentIcon;
    [SerializeField] private TextMeshProUGUI assignmentText;
    [SerializeField] private TextMeshProUGUI actionDescriptionText;

    [SerializeField] private Sprite unassignedSprite;

    [SerializeField] private HumanoidAutoAssignButton autoAssignButton;
    
    public void SetHumanoidTemplateUI(Humanoid humanoid) {
        this.humanoid = humanoid;
        humanoidIcon.sprite = humanoid.GetHumanoidSO().humanoidSprite;
        humanoidJobText.text = humanoid.GetJob().ToString();
        humanoidJobBackgroundSpriteRenderer.sprite = HumanoidsMenuUI.Instance.GetHumanoidWorkerBackgroundSprite(humanoid.GetJob());
        humanoidNameText.text = humanoid.GetHumanoidName().ToString();

        if(humanoid.GetJob() == Humanoid.Job.Haulier) {
            if (humanoid.GetAssignedBuilding() != null) {
                assignmentIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
                assignmentText.text = humanoid.GetAssignedBuilding().GetBuildingSO().buildingType.ToString();
            }
            else {
                assignmentIcon.sprite = unassignedSprite;
                assignmentText.text = "unassigned";
            }
        }
        
        if(humanoid.GetJob() == Humanoid.Job.Worker) {
            if(humanoid.GetAssignedBuilding() != null) {
                assignmentIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
                assignmentText.text = humanoid.GetAssignedBuilding().GetBuildingSO().buildingType.ToString();
            } else {
                assignmentIcon.sprite = unassignedSprite;
                assignmentText.text = "unassigned";
            }
        }

        actionDescriptionText.text = humanoid.GetHumanoidActionDescription();
        autoAssignButton.SetAutoAssign(humanoid.GetAutoAssign());
    }

    public void RefreshHumanoidTemplateUI() {
        humanoidIcon.sprite = humanoid.GetHumanoidSO().humanoidSprite;
        humanoidJobText.text = humanoid.GetJob().ToString();

        humanoidJobBackgroundSpriteRenderer.sprite = HumanoidsMenuUI.Instance.GetHumanoidWorkerBackgroundSprite(humanoid.GetJob());
        humanoidNameText.text = humanoid.GetHumanoidName().ToString();

        if (humanoid.GetJob() == Humanoid.Job.Haulier) {
            if (humanoid.GetAssignedBuilding() != null) {
                assignmentIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
                assignmentText.text = humanoid.GetAssignedBuilding().GetBuildingSO().buildingType.ToString();
            }
            else {
                assignmentIcon.sprite = unassignedSprite;
                assignmentText.text = "unassigned";
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Worker) {
            if (humanoid.GetAssignedBuilding() != null) {
                assignmentIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
                assignmentText.text = humanoid.GetAssignedBuilding().GetBuildingSO().buildingType.ToString();
            }
            else {
                assignmentIcon.sprite = unassignedSprite;
                assignmentText.text = "unassigned";
            }
        }

        actionDescriptionText.text = humanoid.GetHumanoidActionDescription();
        autoAssignButton.SetAutoAssign(humanoid.GetAutoAssign());
    }

    public void ToggleAutoAssign(bool autoAssign) {
        humanoid.SetAutoAssign(autoAssign);
    }

    public void SetJob(Humanoid.Job job) {
        humanoid.SetJob(job);
        RefreshHumanoidTemplateUI();
    }

    public Humanoid GetHumanoid() {
        return humanoid;
    }
}
