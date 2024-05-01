using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidJobFilterButton : MonoBehaviour
{
    [SerializeField] private Humanoid.Job humanoidJob;

    [SerializeField] private Image backGroundSprite;
    [SerializeField] private Sprite disabledSprite;
    private Sprite enabledSprite;

    private bool filterActive = true;

    private void Awake() {
        enabledSprite = backGroundSprite.sprite;
    }

    public void ToggleFilter() {
        filterActive = !filterActive;

        if (!filterActive) {
            backGroundSprite.sprite = disabledSprite;
        }
        else {
            backGroundSprite.sprite = enabledSprite;
        }

        HumanoidsMenuUI.Instance.ChangeHumanoidJobFilter(humanoidJob, filterActive);
    }
}
