using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentTooltipUI : MonoBehaviour
{
    public static EquipmentTooltipUI Instance;

    private Canvas parentCanvas;
    [SerializeField] private GameObject tooltipGameObject;

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemStat1Text;
    [SerializeField] private TextMeshProUGUI itemStat2Text;
    [SerializeField] private TextMeshProUGUI itemDurabilityText;

    [SerializeField] private Image stat1Image;
    [SerializeField] private Image stat2Image;
    [SerializeField] private Image itemIconImage;

    [SerializeField] private Sprite strengthSprite;
    [SerializeField] private Sprite intelligenceSprite;
    [SerializeField] private Sprite moveSpeedSprite;
    [SerializeField] private Sprite healthSprite;
    [SerializeField] private Sprite damageSprite;
    [SerializeField] private Sprite armorSprite;
    [SerializeField] private Sprite agilitySprite;

    private Color semiTransparentColor;

    private void Awake() {
        Instance = this;
        semiTransparentColor = stat1Image.color;
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

        tooltipGameObject.transform.position = parentCanvas.transform.TransformPoint(movePos) - Vector3.one*10;
    }

    public void EnableToolTip(bool enabled) {
        tooltipGameObject.SetActive(enabled);
    }

    public void SetToolTip(Item item) {
        ItemSO itemSO = ItemAssets.Instance.GetItemSO(item.itemType);
        itemNameText.text = itemSO.name;
        itemDescriptionText.text = itemSO.itemDescription;
        itemIconImage.sprite = itemSO.itemSprite;

        itemDurabilityText.text = itemSO.equipmentDurability.ToString() + "s";

        stat1Image.sprite = null;
        stat2Image.sprite = null;
        stat2Image.color = semiTransparentColor;
        stat2Image.color = semiTransparentColor;


        if (itemSO.strengthBonusValue != 0) {
            stat1Image.sprite = strengthSprite;
            itemStat1Text.text = "+" + itemSO.strengthBonusValue.ToString();
        }

        if (itemSO.intelligenceBonusValue != 0) {
            if(stat1Image.sprite == null) {
                stat1Image.sprite = intelligenceSprite;
                itemStat1Text.text = "+" + itemSO.intelligenceBonusValue.ToString();
            } else {
                stat2Image.sprite = intelligenceSprite;
                itemStat2Text.text = "+" + itemSO.intelligenceBonusValue.ToString();
            }
        }

        if (itemSO.moveSpeedBonusValue != 0) {
            if (stat1Image.sprite == null) {
                stat1Image.sprite = moveSpeedSprite;
                itemStat1Text.text = "+" + itemSO.moveSpeedBonusValue.ToString();
            }
            else {
                stat2Image.sprite = moveSpeedSprite;
                itemStat2Text.text = "+" + itemSO.moveSpeedBonusValue.ToString();
            }
        }

        if (itemSO.healthBonusValue != 0) {
            if (stat1Image.sprite == null) {
                stat1Image.sprite = healthSprite;
                itemStat1Text.text = "+" + itemSO.healthBonusValue.ToString();
            }
            else {
                stat2Image.sprite = healthSprite;
                itemStat2Text.text = "+" + itemSO.healthBonusValue.ToString();
            }
        }

        if (itemSO.damageBonusValue != 0) {
            if (stat1Image.sprite == null) {
                stat1Image.sprite = damageSprite;
                itemStat1Text.text = "+" + itemSO.damageBonusValue.ToString();
            }
            else {
                stat2Image.sprite = damageSprite;
                itemStat2Text.text = "+" + itemSO.damageBonusValue.ToString();
            }
        }

        if (itemSO.armorBonusValue != 0) {
            if (stat1Image.sprite == null) {
                stat1Image.sprite = armorSprite;
                itemStat1Text.text = "+" + itemSO.armorBonusValue.ToString();
            }
            else {
                stat2Image.sprite = armorSprite;
                itemStat2Text.text = "+" + itemSO.armorBonusValue.ToString();
            }
        }

        if (itemSO.agilityBonusValue != 0) {
            if (stat1Image.sprite == null) {
                stat1Image.sprite = agilitySprite;
                itemStat1Text.text = "+" + itemSO.agilityBonusValue.ToString();
            }
            else {
                stat2Image.sprite = agilitySprite;
                itemStat2Text.text = "+" + itemSO.agilityBonusValue.ToString();
            }
        }

        if (stat2Image.sprite == null) {
            stat2Image.color = Color.clear;
            itemStat2Text.text = "";
        }

    }
}
