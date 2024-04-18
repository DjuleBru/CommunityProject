using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVisual : MonoBehaviour
{
    private Humanoid humanoid;
    private HumanoidCarry humanoidCarry;
    [SerializeField] private GameObject questionMarkGameObject;
    [SerializeField] private SpriteRenderer carryingItemSprite;

    private void Awake() {
        humanoid = GetComponentInParent<Humanoid>();
        humanoidCarry = GetComponentInParent<HumanoidCarry>();

        questionMarkGameObject.SetActive(false);
    }

    private void Start() {
        humanoidCarry.OnCarryStarted += HumanoidCarry_OnCarryStarted;
        humanoidCarry.OnCarryCompleted += HumanoidCarry_OnCarryCompleted;
    }

    private void HumanoidCarry_OnCarryCompleted(object sender, System.EventArgs e) {
        carryingItemSprite.sprite = null;
    }

    private void HumanoidCarry_OnCarryStarted(object sender, System.EventArgs e) {
        SetCarryingItemSprite(humanoidCarry.GetItemCarrying());
    }

    public void SetQuestionMarkActive(bool active) {
        questionMarkGameObject.SetActive(active);
    }

    public void SetCarryingItemSprite(Item item) {
        carryingItemSprite.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
    }

}
