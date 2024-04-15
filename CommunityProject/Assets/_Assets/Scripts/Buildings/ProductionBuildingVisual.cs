using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionBuildingVisual : BuildingVisual
{
    [SerializeField] Animator buildingAnimator;
    [SerializeField] Animator characterAnimator;
    [SerializeField] GameObject propVisual;

    private void Awake() {
        propVisual.SetActive(true);
        buildingAnimator.gameObject.SetActive(false);
        characterAnimator.gameObject.SetActive(false);
    }

    public void SetWorking(bool working) {
        propVisual.SetActive(!working);
        buildingAnimator.gameObject.SetActive(working);
        characterAnimator.gameObject.SetActive(working);
    }

}
