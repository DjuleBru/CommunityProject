using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAssignJobButton : MonoBehaviour
{
    private List<Humanoid.Job> jobList = new List<Humanoid.Job> { Humanoid.Job.Worker, Humanoid.Job.Haulier, Humanoid.Job.Dungeoneer, Humanoid.Job.Shipper };

    public void ChangeAssignedJob() {
        Humanoid.Job job = GetComponentInParent<HumanoidTemplateUI>().GetHumanoid().GetJob();

        int position = jobList.IndexOf(job);
        int newPosition = position+1;

        if(newPosition == jobList.Count) {
            newPosition = 0;
        }

        GetComponentInParent<HumanoidTemplateUI>().SetJob(jobList[newPosition]);
    }
}
