using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAssignTaskButton : MonoBehaviour
{
    [SerializeField] private HumanoidAutoAssignButton autoAssignButton;
    [SerializeField] private bool isDestionationBuilding;
    [SerializeField] private bool ishousing;

    public void AssignTask() {

        Humanoid humanoid = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid();

        if (humanoid.GetAutoAssign()) {
            autoAssignButton.ToggleAutoAssign();
        }

        if (humanoid.GetJob() == Humanoid.Job.Worker) {
            HumanoidManualAssignManager.Instance.SetAssigningTaskToHumanoid(true, humanoid, false, ishousing);
        }

        if (humanoid.GetJob() == Humanoid.Job.Haulier) {
            HumanoidManualAssignManager.Instance.SetAssigningTaskToHumanoid(true, humanoid, isDestionationBuilding, ishousing);
        }

        if (humanoid.GetJob() == Humanoid.Job.Dungeoneer) {
            HumanoidManualAssignManager.Instance.SetAssigningTaskToHumanoid(true, humanoid, isDestionationBuilding, ishousing);
        }
    }

    public void UnassignTask() {
        Humanoid humanoid = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid();

        if (ishousing) {
            House house = humanoid.GetComponent<HumanoidNeeds>().GetAssignedHousing() as House;
            house.UnassignHumanoidHousing(humanoid);
            humanoid.GetComponent<HumanoidNeeds>().UnAssignHousing();
        }
        GetComponentInParent<HumanoidTemplateUI>().ToggleAutoAssign(false);
        GetComponentInParent<HumanoidTemplateUI>().RefreshHumanoidTemplateUI();
    }

}
