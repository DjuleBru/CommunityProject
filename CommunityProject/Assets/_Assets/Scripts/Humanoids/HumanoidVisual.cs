using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVisual : MonoBehaviour
{
    private Humanoid humanoid;
    private HumanoidCarry humanoidCarry;
    private HumanoidDungeonCrawl humanoidDungeonCrawl;

    [SerializeField] private GameObject questionMarkGameObject;
    [SerializeField] private SpriteRenderer carryingItemSprite;

    [SerializeField] private GameObject bodyGameObject;
    [SerializeField] private GameObject shadowGameObject;

    private void Awake() {
        humanoid = GetComponentInParent<Humanoid>();
        humanoidCarry = GetComponentInParent<HumanoidCarry>();
        humanoidDungeonCrawl = GetComponentInParent<HumanoidDungeonCrawl>();

        humanoidDungeonCrawl.OnCrawlSuccess += HumanoidDungeonCrawl_OnCrawlSuccess;
        humanoidDungeonCrawl.OnCrawlStarted += HumanoidDungeonCrawl_OnCrawlStarted;

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
        SetCarryingItemSprite(humanoidCarry.GetItemCarryingForVisual());
    }

    private void HumanoidDungeonCrawl_OnCrawlStarted(object sender, System.EventArgs e) {
        HideVisual();
    }

    private void HumanoidDungeonCrawl_OnCrawlSuccess(object sender, System.EventArgs e) {
        ShowVisual();
    }

    public void HideVisual() {
        bodyGameObject.SetActive(false);
        shadowGameObject.SetActive(false);
    }

    public void ShowVisual() {
        bodyGameObject.SetActive(true);
        shadowGameObject.SetActive(true);
    }

    public void SetQuestionMarkActive(bool active) {
        questionMarkGameObject.SetActive(active);
    }

    public void SetCarryingItemSprite(Item item) {
        carryingItemSprite.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
    }

}
