using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTooltipUI : MonoBehaviour
{

    public static RecipeTooltipUI Instance;
    private Canvas parentCanvas;

    [SerializeField] private GameObject tooltipGameObject;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Transform usedInItemContainer;
    [SerializeField] private Transform usedInItemTemplate;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        Vector2 pos;
        parentCanvas = GetComponentInParent<Canvas>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);

        tooltipGameObject.SetActive(false);
    }

    private void Update() {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        tooltipGameObject.transform.position = parentCanvas.transform.TransformPoint(movePos);
    }

    public void SetRecipeSO(RecipeSO recipeSO) {
        tooltipGameObject.SetActive(true);
        itemIcon.sprite = ItemAssets.Instance.GetItemSO(recipeSO.outputItems[0].itemType).itemSprite;
        itemNameText.text = recipeSO.recipeName;

        foreach(Transform child in usedInItemContainer) {
            if (child == usedInItemTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO usedInRecipeSO in ItemAssets.Instance.GetUsedInRecipeSOList(recipeSO)) {
             Transform recipeIconTemplate = Instantiate(usedInItemTemplate, usedInItemContainer);
             recipeIconTemplate.gameObject.SetActive(true);
             recipeIconTemplate.Find("RecipeIcon").GetComponent<Image>().sprite = ItemAssets.Instance.GetItemSO(usedInRecipeSO.outputItems[0].itemType).itemSprite;
        }
    }

    public void ResetTooltip() {
        tooltipGameObject.SetActive(false);
    }


}
