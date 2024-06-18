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
    [SerializeField] private Image housingAssignmentIcon;
    [SerializeField] private Image humanoidItemCarryingSprite;

    [SerializeField] private TextMeshProUGUI mainAssignmentText;
    [SerializeField] private TextMeshProUGUI inputAssignmentText;
    [SerializeField] private TextMeshProUGUI outputAssignmentText;
    [SerializeField] private TextMeshProUGUI housingAssignmentText;

    [SerializeField] private TextMeshProUGUI actionDescriptionText;

    [SerializeField] private Color unassignedColor;

    [SerializeField] private HumanoidAutoAssignButton autoAssignButton;

    [SerializeField] private GameObject mainAssignmentSlot;
    [SerializeField] private GameObject inputAssignmentSlot;
    [SerializeField] private GameObject outputAssignmentSlot;
    [SerializeField] private GameObject housingAssignmentSlot;

    [SerializeField] private GameObject mainAssignmentSlotCameraButton;
    [SerializeField] private GameObject inputAssignmentSlotCameraButton;
    [SerializeField] private GameObject outputAssignmentSlotCameraButton;
    [SerializeField] private GameObject housingAssignmentSlotCameraButton;

    [SerializeField] private GameObject housingUnassignButton;
    [SerializeField] private GameObject housingAssignButton;


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
                outputAssignmentIcon.color = Color.white;
                outputAssignmentText.text = humanoid.GetComponent<HumanoidHaul>().GetDestinationBuilding().GetBuildingSO().buildingType.ToString();
                outputAssignmentSlotCameraButton.SetActive(true);
            } else {
                outputAssignmentIcon.color = unassignedColor;
                outputAssignmentText.text = "Unassigned";
                outputAssignmentSlotCameraButton.SetActive(false);
            }

            if (humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding() != null) {
                inputAssignmentIcon.sprite = humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding().GetBuildingSO().buildingIconSprite;
                inputAssignmentIcon.color = Color.white;
                inputAssignmentText.text = humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding().GetBuildingSO().buildingType.ToString();
                inputAssignmentSlotCameraButton.SetActive(true);
            }
            else {
                inputAssignmentIcon.color = unassignedColor;
                inputAssignmentText.text = "Unassigned";
                inputAssignmentSlotCameraButton.SetActive(true);
            }

            if(humanoid.GetComponent<HumanoidHaul>().GetItemToCarry() != null) {
                humanoidItemCarryingSprite.sprite = ItemAssets.Instance.GetItemSO(humanoid.GetComponent<HumanoidHaul>().GetItemToCarry().itemType).itemSprite;
            } else {
                humanoidItemCarryingSprite.color = unassignedColor;
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Worker) {

            mainAssignmentSlot.SetActive(true);
            inputAssignmentSlot.SetActive(false);
            outputAssignmentSlot.SetActive(false);

            if (humanoid.GetAssignedBuilding() != null) {
                mainAssignmentIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
                mainAssignmentIcon.color = Color.white;
                mainAssignmentText.text = humanoid.GetAssignedBuilding().GetBuildingSO().buildingType.ToString();
                mainAssignmentSlotCameraButton.SetActive(true);
            }
            else {
                mainAssignmentIcon.color = unassignedColor;
                mainAssignmentText.text = "Unassigned";
                mainAssignmentSlotCameraButton.SetActive(false);
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Dungeoneer) {

            mainAssignmentSlot.SetActive(true);
            inputAssignmentSlot.SetActive(false);
            outputAssignmentSlot.SetActive(false);

            DungeonEntrance dungeonEntranceAssigned = humanoid.GetComponent<HumanoidDungeonCrawl>().GetDungeonEntranceAssigned();

            if (dungeonEntranceAssigned != null) {
                mainAssignmentIcon.sprite = dungeonEntranceAssigned.GetDungeonSO().dungeonSprite;
                mainAssignmentIcon.color = Color.white;
                mainAssignmentText.text = dungeonEntranceAssigned.GetDungeonSO().dungeonName.ToString();
                mainAssignmentSlotCameraButton.SetActive(true);
            }
            else {
                mainAssignmentIcon.color = unassignedColor;
                mainAssignmentText.text = "Unassigned";
                mainAssignmentSlotCameraButton.SetActive(false);
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Unassigned) {

            mainAssignmentSlot.SetActive(false);
            inputAssignmentSlot.SetActive(false);
            outputAssignmentSlot.SetActive(false);
        }

        actionDescriptionText.text = humanoid.GetHumanoidActionDescription();
        autoAssignButton.SetAutoAssign(humanoid.GetAutoAssign());


        Building house = humanoid.GetComponent<HumanoidNeeds>().GetAssignedHousing();
        if(house == null) {
            housingAssignmentIcon.color = unassignedColor;
            housingAssignmentText.text = "Unassigned";
            housingAssignmentSlotCameraButton.SetActive(false);
            housingUnassignButton.SetActive(false);
            housingAssignButton.SetActive(true);
        } else {
            housingAssignmentIcon.sprite = house.GetBuildingSO().buildingIconSprite;
            housingAssignmentIcon.color = Color.white;
            housingAssignmentText.text = house.GetBuildingSO().buildingType.ToString();
            housingAssignmentSlotCameraButton.SetActive(true);
            housingUnassignButton.SetActive(true);
            housingAssignButton.SetActive(false);
        }
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
