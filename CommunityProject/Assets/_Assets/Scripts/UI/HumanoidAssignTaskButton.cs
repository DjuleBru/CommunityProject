using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAssignTaskButton : MonoBehaviour
{
    public void AssignTask() {
        Humanoid humanoid = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid();
        HumanoidManualAssignManager.Instance.SetAssigningBuildingToHumanoid(true, humanoid);
    }

}
