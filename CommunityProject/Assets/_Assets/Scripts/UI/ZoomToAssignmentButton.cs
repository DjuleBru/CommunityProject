using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToAssignmentButton : MonoBehaviour
{

    [SerializeField] private bool isInputBuilding;

    public void ZoomToAssignment() {
        Humanoid humanoid = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid();

        if(humanoid.GetJob() == Humanoid.Job.Worker) {
            if (humanoid.GetAssignedBuilding() != null) {
                FreeCameraViewManager.Instance.SetFreeCamera(true);

                FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetAssignedBuilding().transform.position);
            }
        }

        if (humanoid.GetJob() == Humanoid.Job.Haulier) {
            if(isInputBuilding) {
                if (humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding() != null) {
                    FreeCameraViewManager.Instance.SetFreeCamera(true);
                    FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetComponent<HumanoidHaul>().GetSourceBuilding().transform.position);
                } 
            } else {
                if (humanoid.GetComponent<HumanoidHaul>().GetDestinationBuilding() != null) {
                    FreeCameraViewManager.Instance.SetFreeCamera(true);
                    FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetComponent<HumanoidHaul>().GetDestinationBuilding().transform.position);
                }
            }
        }

        if(humanoid.GetJob() == Humanoid.Job.Dungeoneer) {
            if(humanoid.GetComponent<HumanoidDungeonCrawl>().GetDungeonEntranceAssigned() != null) {
                FreeCameraViewManager.Instance.SetFreeCamera(true);
                FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetComponent<HumanoidDungeonCrawl>().GetDungeonEntranceAssigned().transform.position);
            }
        }

    }
}
