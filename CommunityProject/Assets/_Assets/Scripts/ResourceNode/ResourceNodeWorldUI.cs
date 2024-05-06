using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNodeWorldUI : MonoBehaviour
{

    [SerializeField] private GameObject progressionBarGameObject;
    [SerializeField] private Image progressionBarFill;

    private ResourceNode resourceNode;

    private void Awake() {
        resourceNode = GetComponentInParent<ResourceNode>();
    }

    private void Start() {
        progressionBarGameObject.SetActive(false);
        resourceNode.OnResourceNodeHit += ResourceNode_OnResourceNodeHit;
        resourceNode.OnResourceNodeDepleted += ResourceNode_OnResourceNodeDepleted;
    }

    private void ResourceNode_OnResourceNodeDepleted(object sender, System.EventArgs e) {
        progressionBarGameObject.SetActive(false);
    }

    private void ResourceNode_OnResourceNodeHit(object sender, System.EventArgs e) {
        progressionBarGameObject.SetActive(true);
        progressionBarFill.fillAmount = resourceNode.GetHarvestingAmountNormalized();
    }
}
