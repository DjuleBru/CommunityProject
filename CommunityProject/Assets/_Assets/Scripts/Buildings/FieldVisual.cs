using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldVisual : ProductionBuildingVisual
{
    [SerializeField] private List<SpriteRenderer> cropSpriteRenderers;

    private ProductionBuilding productionBuilding;

    private bool seed;
    private bool growth1;
    private bool growth2;
    private bool growth3;

    private void Awake() {
        productionBuilding = building as ProductionBuilding;
    }

    private void Update() {

        if (!productionBuilding.GetWorking()) return;

        if(productionBuilding.GetProductionTimerNormalized() < .25f) {
            if(!seed) {
                seed = true;
                growth1 = false;
                growth2 = false;
                growth3 = false;

                
                SetCropSprites(ItemAssets.Instance.GetItemSO(productionBuilding.GetSelectedRecipeSO().outputItems[0].itemType).seedSprite);
            }
        }

        if (productionBuilding.GetProductionTimerNormalized() > .25f && productionBuilding.GetProductionTimerNormalized() < .5f) {
            if (!growth1) {
                growth1 = true;
                SetCropSprites(ItemAssets.Instance.GetItemSO(productionBuilding.GetSelectedRecipeSO().outputItems[0].itemType).growth1Sprite);
            }
        }

        if (productionBuilding.GetProductionTimerNormalized() > .5f && productionBuilding.GetProductionTimerNormalized() < .75f) {
            if (!growth2) {
                growth2 = true;
                SetCropSprites(ItemAssets.Instance.GetItemSO(productionBuilding.GetSelectedRecipeSO().outputItems[0].itemType).growth2Sprite);
            }
        }

        if (productionBuilding.GetProductionTimerNormalized() > .75f) {
            if (!growth3) {
                growth3 = true;
                seed = false;
                SetCropSprites(ItemAssets.Instance.GetItemSO(productionBuilding.GetSelectedRecipeSO().outputItems[0].itemType).growth3Sprite);
            }
        }
    }

    private void SetCropSprites(Sprite sprite) {
        foreach(SpriteRenderer renderer in cropSpriteRenderers) {
            float randomTime = UnityEngine.Random.Range(0, 3f);
            StartCoroutine(SetCropSprite(sprite, renderer, randomTime));
        }
    }

    private IEnumerator SetCropSprite(Sprite sprite, SpriteRenderer spriteRenderer, float delay) {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = sprite;
    }

}
