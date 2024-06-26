using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResearchMenuUI : MonoBehaviour
{

    public class ResearchProgress {
        public RecipeSO recipeSO;
        public BuildingSO buildingSO;
        public List<Item> remainingItemList;
    }

    public event EventHandler OnSelectedResearchChanged;
    public event EventHandler OnSelectedResearchFinished;

    public static ResearchMenuUI Instance;
    [SerializeField] private GameObject researchTreePanel;
    [SerializeField] private GameObject researchDescriptionPanel;

    [SerializeField] private TextMeshProUGUI unlockableName;
    [SerializeField] private Image unlockableIcon;

    [SerializeField] private Transform researchItemsToUnlockContainer;
    [SerializeField] private Transform researchItemToUnlockTemplate;

    [SerializeField] private Transform buildingRecipeContainer;
    [SerializeField] private Transform buildingRecipeTemplate;

    [SerializeField] private GameObject buildingResearchButtonsHolder;
    [SerializeField] private GameObject recipeResearchButtonsHolder;
    private ResearchButtonUI researchButtonSelected;


    [SerializeField] private Image researchProgressImage;

    private List<ResearchProgress> researchProgressList;
    private ResearchProgress researchSelected;

    private bool researchTreePanelOpen;

    private void Awake() {
        Instance = this;
        researchTreePanel.gameObject.SetActive(false);
        researchDescriptionPanel.gameObject.SetActive(false);
    }

    private void Start() {
        InitializeResearchProgressLists();
        LoadResearchProgress();
    }

    private void InitializeResearchProgressLists() {
        if (researchProgressList == null) {
            researchProgressList = new List<ResearchProgress>();

            foreach (RecipeSO recipeSO in ItemAssets.Instance.GetLockedRecipeSOList()) {

                List<Item> remainingItemList = new List<Item>();
                foreach(Item item in recipeSO.itemsToUnlockList) {
                    Item itemToAdd = new Item { itemType = item.itemType, amount = item.amount };
                    remainingItemList.Add(itemToAdd);
                }

                ResearchProgress recipeResearchProgress = new ResearchProgress {
                    recipeSO = recipeSO,
                    remainingItemList = remainingItemList,
                };
                researchProgressList.Add(recipeResearchProgress);
            }

            foreach (BuildingSO buildingSO in BuildingsManager.Instance.GetLockedBuildingSOList()) {

                List<Item> remainingItemList = new List<Item>();
                foreach (Item item in buildingSO.itemsToUnlockList) {
                    Item itemToAdd = new Item { itemType = item.itemType, amount = item.amount };
                    remainingItemList.Add(itemToAdd);
                }

                ResearchProgress recipeResearchProgress = new ResearchProgress {
                    buildingSO = buildingSO,
                    remainingItemList = remainingItemList,
                };
                researchProgressList.Add(recipeResearchProgress);
            }
        }
    }

    public void OpenCloseResearchTreePanel() {
        researchTreePanelOpen = !researchTreePanelOpen;
        researchTreePanel.gameObject.SetActive(researchTreePanelOpen);

        if(researchSelected != null) {
            researchDescriptionPanel.gameObject.SetActive(researchTreePanelOpen);
        } else {
            researchDescriptionPanel.gameObject.SetActive(false);
        }
    }

    public void OpenResearchDescriptionPanel(bool open) {
        researchDescriptionPanel.gameObject.SetActive(open);
    }

    public void RefreshResearchDescriptionPanel(BuildingSO buildingSO) {
        unlockableName.text = buildingSO.buildingName;
        unlockableIcon.sprite = buildingSO.buildingIconSprite;

        foreach(Transform child in buildingRecipeContainer) {
            if (child == buildingRecipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Transform child in researchItemsToUnlockContainer) {
            if (child == researchItemToUnlockTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in buildingSO.buildingRecipes) {
            Transform recipeTransform = Instantiate(buildingRecipeTemplate, buildingRecipeContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<RecipeSlotTemplate>().SetRecipe(recipeSO);
        }

        foreach(Item item in GetResearchProgress(buildingSO).remainingItemList) {
            Transform itemTransform = Instantiate(researchItemToUnlockTemplate, researchItemsToUnlockContainer);
            itemTransform.gameObject.SetActive(true);
            itemTransform.GetComponent<ItemSlot>().SetItem(item);
        }

        if(researchSelected == null) {
            researchProgressImage.fillAmount = 0;
        } else {
            researchProgressImage.fillAmount = GetResearchProgressNormalized(GetResearchProgress(buildingSO));
        }
    }

    public void RefreshResearchDescriptionPanel(RecipeSO recipeSO) {
        unlockableName.text = recipeSO.recipeName;
        unlockableIcon.sprite = ItemAssets.Instance.GetItemSO(recipeSO.outputItems[0].itemType).itemSprite;

        foreach (Transform child in buildingRecipeContainer) {
            if (child == buildingRecipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Transform child in researchItemsToUnlockContainer) {
            if (child == researchItemToUnlockTemplate) continue;
            Destroy(child.gameObject);
        }

        Transform recipeTransform = Instantiate(buildingRecipeTemplate, buildingRecipeContainer);
        recipeTransform.GetComponent<RecipeSlotTemplate>().SetRecipe(recipeSO);
        recipeTransform.gameObject.SetActive(true);

        foreach (Item item in GetResearchProgress(recipeSO).remainingItemList) {
            Transform itemTransform = Instantiate(researchItemToUnlockTemplate, researchItemsToUnlockContainer);
            itemTransform.GetComponent<ItemSlot>().SetItem(item);
            itemTransform.gameObject.SetActive(true);
        }

        researchProgressImage.fillAmount = GetResearchProgressNormalized(GetResearchProgress(recipeSO));
    }

    public void RefreshResearchDescriptionPanel(ResearchProgress researchProgress) {
        if(researchProgress.recipeSO != null) {
            RefreshResearchDescriptionPanel(researchProgress.recipeSO);
        } else {
            RefreshResearchDescriptionPanel(researchProgress.buildingSO);
        }

    }

    public void ResetResearchDescriptionPanel() {
        if(researchSelected != null) {
            RefreshResearchDescriptionPanel(researchSelected);
        } else {
            OpenResearchDescriptionPanel(false);
        }
    }

    public void OpenResearchDescriptionPanell(bool open) {
        researchDescriptionPanel.gameObject.SetActive(open);
    }

    public void SetResearchButtonSelected(ResearchButtonUI researchButton) {
        researchButtonSelected = researchButton;
    }
    public void SetResearchSelected(RecipeSO recipeSO) {
        researchSelected = null;

        foreach (ResearchProgress recipeResearch in researchProgressList) {
            if(recipeResearch.recipeSO == recipeSO) {
                researchSelected = recipeResearch;
            }
        }
    }

    public void SetResearchSelected(BuildingSO buildingSO) {
        researchSelected = null;

        foreach (ResearchProgress recipeResearch in researchProgressList) {
            if (recipeResearch.buildingSO == buildingSO) {
                researchSelected = recipeResearch;
            }
        }

        OnSelectedResearchChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ProgressInSelectedResearch(Item item) {
        if (researchSelected != null) {
            List<Item> itemsToRemoveFromList = new List<Item>();

            foreach(Item inputItem in researchSelected.remainingItemList) {
                if(inputItem.itemType == item.itemType && inputItem.amount > 0) {
                    inputItem.amount -= 1;

                    if(inputItem.amount == 0) {
                        itemsToRemoveFromList.Add(inputItem);
                    }
                }
            }

            foreach(Item itemToRemove in itemsToRemoveFromList) {
                researchSelected.remainingItemList.Remove(itemToRemove);
            }
        }

        RefreshResearchDescriptionPanel(researchSelected);

        //Check if research is finished
        bool researchIsFinished = true;
        foreach(Item inputItem in researchSelected.remainingItemList) {
            if (inputItem.amount != 0) {
                researchIsFinished = false;
            }
        }

        if(researchIsFinished) {
            FinishSelectedResearch();
            researchSelected = null;
            OnSelectedResearchFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    private void FinishSelectedResearch() {
        researchButtonSelected.SetResearchUnlocked();

        if(researchSelected.recipeSO != null) {

        }

        if(researchSelected.buildingSO != null) {
            BuildingsManager.Instance.UnlockBuilding(researchSelected.buildingSO);
        }
    }

    public ResearchProgress GetCurrentResearch() {
        return researchSelected;
    }

    public Sprite GetCurrentResearchSprite() {
        if(researchSelected.buildingSO != null) {
            return researchSelected.buildingSO.buildingIconSprite;
        } else {
            return ItemAssets.Instance.GetItemSO(researchSelected.recipeSO.outputItems[0].itemType).itemSprite;
        }
    }

    public string GetCurrentResearchName() {
        if (researchSelected.buildingSO != null) {
            return researchSelected.buildingSO.buildingName;
        }
        else {
            return researchSelected.recipeSO.recipeName;
        }
    }

    public ResearchProgress GetResearchProgress(BuildingSO buildingSO) {

        foreach(ResearchProgress researchProgress in researchProgressList) {
            if (researchProgress.buildingSO == buildingSO) {
                return researchProgress;
            }
        }

        return null;
    }

    public ResearchProgress GetResearchProgress(RecipeSO recipeSO) {
        foreach (ResearchProgress researchProgress in researchProgressList) {
            if (researchProgress.recipeSO == recipeSO) {
                return researchProgress;
            }
        }
        return null;
    }

    public float GetResearchProgressNormalized(ResearchProgress researchProgress) {
        int totalItemAmount = 0;
        int remainingItemAmount = 0;

        if(researchProgress.recipeSO != null) {
            foreach (Item item in researchProgress.recipeSO.itemsToUnlockList) {
                totalItemAmount += item.amount;
            }
        } else {
            foreach (Item item in researchProgress.buildingSO.itemsToUnlockList) {
                totalItemAmount += item.amount;
            }
        }

        foreach(Item item in researchProgress.remainingItemList) {
            remainingItemAmount += item.amount;
        }

        return ((float)totalItemAmount - (float)remainingItemAmount)/ (float)totalItemAmount;

    }

    public void SaveResearchProgress() {
        ES3.Save("researchProgressList", researchProgressList);
        ES3.Save("researchSelected", researchSelected);

    }

    private void LoadResearchProgress() {
        researchProgressList = ES3.Load("researchProgressList", researchProgressList);
        researchSelected = ES3.Load("researchSelected", researchProgressList[0]);
        OnSelectedResearchChanged?.Invoke(this, EventArgs.Empty);
    }

}
