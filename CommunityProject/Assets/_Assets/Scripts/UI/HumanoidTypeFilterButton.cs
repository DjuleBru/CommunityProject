using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidTypeFilterButton : MonoBehaviour
{
    [SerializeField] private HumanoidSO.HumanoidType humanoidType;
    [SerializeField] private Image humanoidIcon;

    [SerializeField] private Color disabledColor;

    private bool filterActive = true;

    public void ToggleFilter() {
        filterActive = !filterActive;

        if(!filterActive) {
            humanoidIcon.color = disabledColor;
        } else {
            humanoidIcon.color = Color.white;
        }

        HumanoidsMenuUI.Instance.ChangeHumanoidTypeFilter(humanoidType, filterActive);
    }
}
