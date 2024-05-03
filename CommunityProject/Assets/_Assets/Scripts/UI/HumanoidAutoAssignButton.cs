using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidAutoAssignButton : MonoBehaviour
{
    [SerializeField] private Image autoAssignSprite;
    [SerializeField] private Sprite manualAssignSprite;
    [SerializeField] private HumanoidTemplateUI humanoidTemplateUI;

    private Sprite enabledSprite;

    private bool autoAssignActive;

    private void Awake() {
        enabledSprite = autoAssignSprite.sprite;
    }

    public void SetAutoAssign(bool autoAssignActive) {
        this.autoAssignActive = autoAssignActive;

        if (!autoAssignActive) {
            autoAssignSprite.sprite = manualAssignSprite;
        }
        else {
            autoAssignSprite.sprite = enabledSprite;
        }
    }

    public void ToggleAutoAssign() {
        autoAssignActive = !autoAssignActive;

        if (!autoAssignActive) {
            autoAssignSprite.sprite = manualAssignSprite;
        }
        else {
            autoAssignSprite.sprite = enabledSprite;
        }

        humanoidTemplateUI.ToggleAutoAssign(autoAssignActive);
        GetComponentInParent<HumanoidTemplateUI>().RefreshHumanoidTemplateUI();
    }


}
