using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidUI : MonoBehaviour
{

    public static HumanoidUI Instance { get; private set; }

    [SerializeField] private GameObject humanoidUIPanel;
    private bool panelOpen;

    private Humanoid humanoid;
    private HumanoidNeeds humanoidNeeds;

    [SerializeField] private Image hungerBarFillImage;
    [SerializeField] private Image energyBarFillImage;
    [SerializeField] private Image humanoidIcon;
    [SerializeField] private Image jobBackgroundImage;

    [SerializeField] private TextMeshProUGUI humanoidNameText;
    [SerializeField] private TextMeshProUGUI humanoidJobText;
    [SerializeField] private TextMeshProUGUI humanoidDescriptionText;

    private void Awake() {
        Instance = this;
        humanoidUIPanel.SetActive(false);
    }

    public bool GetPanelOpen() {
        return panelOpen;
    }

    public void ClosePanel() {
        panelOpen = false;
        humanoidUIPanel.SetActive(false);
    }

    public void OpenPanel() {
        panelOpen = true;
        humanoidUIPanel.SetActive(true);
    }

    private void Update() {
        if (panelOpen) {
            hungerBarFillImage.fillAmount = humanoidNeeds.GetHunger() / 100;
            energyBarFillImage.fillAmount = humanoidNeeds.GetEnergy() / 100;
        }
    }

    public void SetHumanoid(Humanoid humanoid) {
        this.humanoid = humanoid;
        humanoidNeeds = humanoid.GetComponent<HumanoidNeeds>();

        humanoidIcon.sprite = humanoid.GetHumanoidSO().humanoidSprite;
        humanoidNameText.text = humanoid.GetHumanoidName();
        humanoidJobText.text = humanoid.GetJob().ToString();
        humanoidDescriptionText.text = humanoid.GetHumanoidActionDescription();
        jobBackgroundImage.sprite = HumanoidsMenuUI.Instance.GetHumanoidWorkerBackgroundSprite(humanoid.GetJob());
    }

}
