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
                if (humanoid.GetComponent<HumanoidCarry>().GetSourceBuilding() != null) {
                    FreeCameraViewManager.Instance.SetFreeCamera(true);
                    FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetComponent<HumanoidCarry>().GetSourceBuilding().transform.position);
                } else {
                    FreeCameraViewManager.Instance.SetFreeCamera(true);
                    FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetComponent<HumanoidCarry>().GetDestinationBuilding().transform.position);
                }
            }
            
        }

    }
}
