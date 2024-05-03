using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAssignTaskButton : MonoBehaviour
{
    [SerializeField] private HumanoidAutoAssignButton autoAssignButton;
    [SerializeField] private bool isDestionationBuilding;

    public void AssignTask() {

        Humanoid humanoid = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid();

        if (humanoid.GetAutoAssign()) {
            autoAssignButton.ToggleAutoAssign();
        }

        if (humanoid.GetJob() == Humanoid.Job.Worker) {
            HumanoidManualAssignManager.Instance.SetAssigningBuildingToHumanoid(true, humanoid, false);
        }

        if (humanoid.GetJob() == Humanoid.Job.Haulier) {
            HumanoidManualAssignManager.Instance.SetAssigningBuildingToHumanoid(true, humanoid, isDestionationBuilding);
        }

    }

}
