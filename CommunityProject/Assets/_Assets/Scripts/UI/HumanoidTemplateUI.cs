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

    [SerializeField] private Image mainAssignmentIcon;
    [SerializeField] private Image inputAssignmentIcon;
    [SerializeField] private Image outputAssignmentIcon;
    [SerializeField] private Image humanoidItemCarryingSprite;

    [SerializeField] private TextMeshProUGUI mainAssignmentText;
    [SerializeField] private TextMeshProUGUI inputAssignmentText;
    [SerializeField] private TextMeshProUGUI outputAssignmentText;

    [SerializeField] private TextMeshProUGUI actionDescriptionText;

    [SerializeField] private Sprite unassignedSprite;

    [SerializeField] private HumanoidAutoAssignButton autoAssignButton;

    [SerializeField] private GameObject mainAssignmentSlot;
    [SerializeField] private GameObject inputAssignmentSlot;
    [SerializeField] private GameObject outputAssignmentSlot;


    public void SetHumanoidTemplateUI(Humanoid humanoid) {
        this.humanoid = humanoid;
        RefreshHumanoidTemplateUI();
    }

    public void RefreshHumanoidTemplateUI() {
        humanoidIcon.sprite = humanoid.GetHumanoidSO().humanoidSprite;
        humanoidJobText.text = humanoid.GetJob().ToString();

        humanoidJobBackgroundSpriteRenderer.sprite = HumanoidsMenuUI.Instance.GetHumanoidWorkerBackgroundSprite(humanoid.GetJob());
        humanoidNameText.text = humanoid.GetHumanoidName().ToString();

        if (humanoid.GetJob() == Humanoid.Job.Haulier) {

            mainAssignmentSlot.SetActive(false);
            inputAssignmentSlot.SetActive(true);
            outputAssignmentSlot.SetActive(true);

            if (humanoid.GetComponent<HumanoidHaul>().GetDestinationBuilding() != null) {
                outputAssignmentIcon.sprite = humanoid.GetComponent<HumanoidHaul>().GetDestinationBuilding().GetBuildingSO().buildingIconSprite;
                outputAssignmentText.text = humanoid.GetComponent<HumanoidHaul>().GetDestinationBuilding().GetBuildingSO().buildingType.ToString();
            } else {
                outputAssignmentIcon.sprite = unassignedSprite;
                outputAssignmentText.text = "Unassigned";
            }

            if (humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding() != null) {
                inputAssignmentIcon.sprite = humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding().GetBuildingSO().buildingIconSprite;
                inputAssignmentText.text = humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding().GetBuildingSO().buildingType.ToString();
            }
            else {
                inputAssignmentIcon.sprite = unassignedSprite;
                inputAssignmentText.text = "Unassigned";
            }

            if(humanoid.GetComponent<HumanoidHaul>().GetItemToCarry() != null) {
                humanoidItemCarryingSprite.sprite = ItemAssets.Instance.GetItemSO(humanoid.GetComponent<HumanoidHaul>().GetItemToCarry().itemType).itemSprite;
            } else {
                humanoidItemCarryingSprite.sprite = unassignedSprite;
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Worker) {

            mainAssignmentSlot.SetActive(true);
            inputAssignmentSlot.SetActive(false);
            outputAssignmentSlot.SetActive(false);

            if (humanoid.GetAssignedBuilding() != null) {
                mainAssignmentIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
                mainAssignmentText.text = humanoid.GetAssignedBuilding().GetBuildingSO().buildingType.ToString();
            }
            else {
                mainAssignmentIcon.sprite = unassignedSprite;
                mainAssignmentText.text = "Unassigned";
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Dungeoneer) {

            mainAssignmentSlot.SetActive(true);
            inputAssignmentSlot.SetActive(false);
            outputAssignmentSlot.SetActive(false);

            DungeonEntrance dungeonEntranceAssigned = humanoid.GetComponent<HumanoidDungeonCrawl>().GetDungeonEntranceAssigned();

            if (dungeonEntranceAssigned != null) {
                mainAssignmentIcon.sprite = dungeonEntranceAssigned.GetDungeonSO().dungeonSprite;
                mainAssignmentText.text = dungeonEntranceAssigned.GetDungeonSO().dungeonName.ToString();
            }
            else {
                mainAssignmentIcon.sprite = unassignedSprite;
                mainAssignmentText.text = "Unassigned";
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

    public void SetItemToCarry(Item item) {
        humanoid.GetComponent<HumanoidHaul>().SetItemToCarry(item);
        RefreshHumanoidTemplateUI();
    }

    public Humanoid GetHumanoid() {
        return humanoid;
    }
}
