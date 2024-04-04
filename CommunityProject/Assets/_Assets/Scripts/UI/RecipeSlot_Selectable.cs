using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeSlot_Selectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    [SerializeField] private GameObject selectedGameObject;

    private RecipeSO recipeSO;
    private ProductionBuilding linkedProductionBuilding;

    private void Awake() {
        selectedGameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData) {
        linkedProductionBuilding.SetSelectedRecipeSO(recipeSO);
        ProductionBuildingUI.Instance.RefreshRecipeList();
        ProductionBuildingUI.Instance.RefreshRecipePanel();
    }

    public void OnPointerEnter(PointerEventData eventData) {

    }

    public void OnPointerExit(PointerEventData eventData) {

    }

    public void SetRecipeSlot(RecipeSO recipeSO, ProductionBuilding linkedProductionBuilding, bool selected) {
        this.recipeSO = recipeSO;
        this.linkedProductionBuilding = linkedProductionBuilding;
        selectedGameObject.SetActive(selected);
    }
}
