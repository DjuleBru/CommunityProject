using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeSlotTemplate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RecipeSO recipeSO;

    public void OnPointerEnter(PointerEventData eventData) {
        RecipeTooltipUI.Instance.SetRecipeSO(recipeSO);
    }

    public void OnPointerExit(PointerEventData eventData) {
        RecipeTooltipUI.Instance.ResetTooltip();
    }

    public void SetRecipe(RecipeSO recipeSO) {
        this.recipeSO = recipeSO;
    }
}
