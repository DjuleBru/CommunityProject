using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidVisual : MonoBehaviour
{
    private Humanoid humanoid;
    private HumanoidCarry humanoidCarry;
    private HumanoidDungeonCrawl humanoidDungeonCrawl;

    [SerializeField] private GameObject questionMarkGameObject;
    [SerializeField] private GameObject hungryStatusGameObject;
    [SerializeField] private GameObject exhaustedStatusGameObject;
    [SerializeField] private SpriteRenderer carryingItemSprite;

    [SerializeField] private SpriteRenderer eatingItemSprite;
    [SerializeField] private Animator eatingItemAnimator;

    [SerializeField] private ParticleSystem sleepingPS;

    [SerializeField] private GameObject bodyGameObject;
    [SerializeField] private GameObject shadowGameObject;

    [SerializeField] private GameObject statusesContainer;



    private void Awake() {
        questionMarkGameObject.SetActive(false);
        hungryStatusGameObject.SetActive(false);
        exhaustedStatusGameObject.SetActive(false);
        eatingItemSprite.gameObject.SetActive(false);


        humanoid = GetComponentInParent<Humanoid>();
        humanoidCarry = GetComponentInParent<HumanoidCarry>();
        humanoidDungeonCrawl = GetComponentInParent<HumanoidDungeonCrawl>();
    }

    private void Start() {
        humanoidDungeonCrawl.OnCrawlSuccess += HumanoidDungeonCrawl_OnCrawlSuccess;
        humanoidDungeonCrawl.OnCrawlStarted += HumanoidDungeonCrawl_OnCrawlStarted;

        humanoidCarry.OnCarryStarted += HumanoidCarry_OnCarryStarted;
        humanoidCarry.OnCarryCompleted += HumanoidCarry_OnCarryCompleted;
    }

    public void SetEating(Item itemEating) {
        eatingItemSprite.gameObject.SetActive(true);
        eatingItemSprite.sprite = ItemAssets.Instance.GetItemSO(itemEating.itemType).itemSprite;
        eatingItemAnimator.SetTrigger("Eat");
    }

    public void StopEating() {
        eatingItemSprite.gameObject.SetActive(false);
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

    public void ShowStatuses() {
        statusesContainer.SetActive(true);
    }

    public void HideStatuses() {
        statusesContainer.SetActive(false);
    }


    public void SetQuestionMarkActive(bool active) {
        questionMarkGameObject.SetActive(active);
    }

    public void SetHungryStatusActive(bool active) {
        hungryStatusGameObject.SetActive(active);
    }

    public void SetExhaustedStatusActive(bool active) {
        exhaustedStatusGameObject.SetActive(active);
    }

    public void SetSleeping(bool sleeping) {
        if (sleeping) {
            bodyGameObject.transform.eulerAngles = new Vector3(0, 0, 44f);
            shadowGameObject.SetActive(false);
            HideStatuses();

        } else {
            bodyGameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            shadowGameObject.SetActive(true);
            ShowStatuses();
        }
    }

    public void SetSleepingPS(bool sleeping) {
        if (sleeping) {
            sleepingPS.Play();

        }
        else {
            sleepingPS.Stop();
        }
    }

    public void SetCarryingItemSprite(Item item) {
        carryingItemSprite.sprite = ItemAssets.Instance.GetItemSO(item.itemType).itemSprite;
    }

}
