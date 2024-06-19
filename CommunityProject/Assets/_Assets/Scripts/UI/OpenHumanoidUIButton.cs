using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenHumanoidUIButton : MonoBehaviour
{
    private Humanoid humanoid;
    private Button button;
    private bool humanoidPanelOpen;

    public static OpenHumanoidUIButton lastButtonPressed;
    private bool inventoryOpen;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {

            if (lastButtonPressed != null && lastButtonPressed == this) {
                inventoryOpen = !inventoryOpen;
                if (inventoryOpen) {
                    HumanoidUI.Instance.OpenPanel();
                    HumanoidUI.Instance.SetHumanoid(humanoid);
                }
                else {
                    HumanoidUI.Instance.ClosePanel();
                }
            }
            else {
                lastButtonPressed = this; 
                HumanoidUI.Instance.OpenPanel();
                HumanoidUI.Instance.SetHumanoid(humanoid);
                inventoryOpen = true;
            }

        });
    }

    public void SetHumanoid(Humanoid humanoid) {
        this.humanoid = humanoid;
    }

    public void EnableButton(bool enable) {
        button.interactable = enable;
    }
}
