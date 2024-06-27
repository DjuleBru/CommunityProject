using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResearchButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    [SerializeField] private BuildingSO buildingSO;
    [SerializeField] private RecipeSO recipeSO;

    [SerializeField] private bool researchIsUnlockedAtGameStart;
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private List<ResearchButtonUI> researchUnlockedByThis;

    private bool researchSelected;
    public static event EventHandler OnAnyResearchButtonPressed;

    public void OnPointerEnter(PointerEventData eventData) {
        ResearchMenuUI.Instance.OpenResearchDescriptionPanel(true);
        if (buildingSO != null) {
            ResearchMenuUI.Instance.RefreshResearchDescriptionPanel(buildingSO);
        }
        else {
            ResearchMenuUI.Instance.RefreshResearchDescriptionPanel(recipeSO);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        ResearchMenuUI.Instance.ResetResearchDescriptionPanel();
    }

    private void Awake() {
        button = GetComponent<Button>();

        button.onClick.AddListener(() => {
            ResearchMenuUI.Instance.SetResearchButtonSelected(this);
            if (buildingSO != null) {
                ResearchMenuUI.Instance.SetResearchSelected(buildingSO);
            }
            else {
                ResearchMenuUI.Instance.SetResearchSelected(recipeSO);
            }
            OnAnyResearchButtonPressed?.Invoke(this, EventArgs.Empty);
        });

        if(!researchIsUnlockedAtGameStart) {
            button.interactable = false;
        }
    }

    private void Start() {
        ResearchButtonUI.OnAnyResearchButtonPressed += ResearchButtonUI_OnAnyResearchButtonPressed;

        if (ResearchMenuUI.Instance.GetCurrentResearch() != null) {
            if (buildingSO != null) {
                selectedGameObject.SetActive(ResearchMenuUI.Instance.GetCurrentResearch().buildingSO == buildingSO);
            }
            else {
                selectedGameObject.SetActive(ResearchMenuUI.Instance.GetCurrentResearch().recipeSO == recipeSO);
            }
        } else {
            selectedGameObject.SetActive(false);
        }
       
    }

    private void ResearchButtonUI_OnAnyResearchButtonPressed(object sender, EventArgs e) {
        if((sender as ResearchButtonUI) == this) {
            SetButtonSelected(true);
        } else {
            SetButtonSelected(false);
        }
    }

    private void SetButtonSelected(bool selected) {
        selectedGameObject.SetActive(selected);
    }

    public void SetResearchSelectable() {
        button.interactable = true;
    }

    public void SetResearchUnlocked() {
        button.enabled = false;
        selectedGameObject.SetActive(false);
        foreach (ResearchButtonUI researchButtonUI in researchUnlockedByThis) {
            researchButtonUI.SetResearchSelectable();
        }
    }

}