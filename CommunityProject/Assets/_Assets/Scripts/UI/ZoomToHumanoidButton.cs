using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToHumanoidButton : MonoBehaviour
{
    public void ZoomToHumanoid() {
        Humanoid humanoid = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid();

        FreeCameraViewManager.Instance.SetFreeCamera(true);
        FreeCameraViewManager.Instance.FollowHumanoid(humanoid);
    }
}
