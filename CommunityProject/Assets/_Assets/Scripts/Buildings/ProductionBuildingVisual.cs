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

    public void SetWorking(bool working, HumanoidSO.HumanoidType humanoidType) {
        propVisual.SetActive(!working);

        buildingAnimator.gameObject.SetActive(working);
        characterAnimator.gameObject.SetActive(working);

        characterAnimator.SetBool("Human", false);
        characterAnimator.SetBool("Elf", false);
        characterAnimator.SetBool("Orc", false);
        characterAnimator.SetBool("Goblin", false);
        characterAnimator.SetBool("Halfling", false);
        characterAnimator.SetBool("Dwarf", false);

        characterAnimator.SetBool(humanoidType.ToString(), true);
    }

}
