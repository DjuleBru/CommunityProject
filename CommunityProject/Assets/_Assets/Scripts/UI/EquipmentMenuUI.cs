using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentMenuUI : MonoBehaviour { 
    public static EquipmentMenuUI Instance;

    [SerializeField] private GameObject equipmentMenuUIGameObject;
    [SerializeField] private InventoryUI equipmentInventoryUI;

    [SerializeField] private Sprite defaultWeaponSprite;
    [SerializeField] private Image weaponImage;
    [SerializeField] private Color equippedWeaponColor;
    [SerializeField] private Color unequippedWeaponColor;

    private PlayerEquipmentButton lastEquipmentButtonPressed;

    [SerializeField] private PlayerEquipmentButton mainHandSlot;
    [SerializeField] private PlayerEquipmentButton secondaryHandSlot;
    [SerializeField] private PlayerEquipmentButton headSlot;
    [SerializeField] private PlayerEquipmentButton bootsSlot;
    [SerializeField] private PlayerEquipmentButton trinket1Slot;
    [SerializeField] private PlayerEquipmentButton trinket2Slot;

    [SerializeField] private TextMeshProUGUI healthStatText;
    [SerializeField] private TextMeshProUGUI strengthStatText;
    [SerializeField] private TextMeshProUGUI armorStatText;
    [SerializeField] private TextMeshProUGUI moveSpeedStatText;
    [SerializeField] private TextMeshProUGUI damageStatText;
    [SerializeField] private TextMeshProUGUI attackSpeedStatText;

    [SerializeField] private TextMeshProUGUI weaponKnockbackStatText;
    [SerializeField] private GameObject weaponKnockbackGameObject;
    [SerializeField] private GameObject weaponPierceGameObject;

    private bool menuOpen;

    private void Awake() {
        Instance = this;
        EnableMenu(false);
    }

    private void Start() {
        PlayerEquipment.Instance.OnEquipmentChanged += PlayerEquipment_OnEquipmentChanged;
        RefreshPlayerEquipmentUI();
        RefreshPlayertats();
    }

    private void PlayerEquipment_OnEquipmentChanged(object sender, System.EventArgs e) {
        RefreshPlayerEquipmentUI();
        RefreshPlayertats();
    }

    public void EnableMenu(bool enabled) {
        menuOpen = enabled;
        equipmentMenuUIGameObject.SetActive(enabled);
    }

    public void OpenCloseMenu() {
        menuOpen = !menuOpen;
        EnableMenu(menuOpen);
    }

    public void OpenInventoryUI(Item.ItemEquipmentCategory category, PlayerEquipmentButton playerEquipmentButton) {
        equipmentInventoryUI.gameObject.SetActive(true);

        Inventory equipmentInventory = new Inventory(false, 0, 0, 100000);

        foreach (Item item in BuildingsManager.Instance.GetAllEquipmentItemsOfCategory(category)) {
            Item itemToAdd = new Item { amount = item.amount, itemType = item.itemType };
            equipmentInventory.AddItem(itemToAdd);
        }

        equipmentInventoryUI.SetInventory(equipmentInventory);
        lastEquipmentButtonPressed = playerEquipmentButton;
    }

    public void SetItemToEquip(Item item) {
        ItemSO itemSO = ItemAssets.Instance.GetItemSO(item.itemType);

        int amountToEquip = PlayerEquipment.Instance.GetAmountToEquip(itemSO);
        if(amountToEquip > BuildingsManager.Instance.GetEquipmentItemAmountOfItemType(itemSO.itemType)) {
            amountToEquip = BuildingsManager.Instance.GetEquipmentItemAmountOfItemType(itemSO.itemType);
        }

        Item itemToEquip = new Item { itemType = item.itemType, amount = amountToEquip };

        lastEquipmentButtonPressed.SetItem(itemToEquip);
        PlayerEquipment.Instance.SetEquipmentType(itemToEquip);
        BuildingsManager.Instance.RemoveEquipmentFromAnyChest(itemToEquip);

        OpenInventoryUI(itemSO.itemEquipmentCategory, lastEquipmentButtonPressed);
    }

    public void CloseInventoryUI() {
        equipmentInventoryUI.gameObject.SetActive(false);
    }

    private void RefreshPlayerEquipmentUI() {
        mainHandSlot.SetItem(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.main));
        secondaryHandSlot.SetItem(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.secondary));
        headSlot.SetItem(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.head));
        bootsSlot.SetItem(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.boots));
        trinket1Slot.SetItem(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.necklace));
        trinket2Slot.SetItem(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.ring));

        Item mainEquipmentItem = PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.main);
        if (mainEquipmentItem != null) {
            ItemSO itemSO = ItemAssets.Instance.GetItemSO(PlayerEquipment.Instance.GetEquipmentItem(Item.ItemEquipmentCategory.main).itemType);
            weaponImage.sprite = itemSO.itemSprite;
            weaponImage.color = equippedWeaponColor;

            if(itemSO.weaponSO.weaponCanPierce) {
                weaponPierceGameObject.SetActive(true);
            } else {
                weaponPierceGameObject.SetActive(false);
            }

            if (itemSO.weaponSO.weaponKnockback != 0) {
                weaponKnockbackGameObject.SetActive(true);
                weaponKnockbackStatText.text = (itemSO.weaponSO.weaponKnockback / 100).ToString();
            }
            else {
                weaponKnockbackGameObject.SetActive(false);
            }


        } else {
            weaponImage.sprite = defaultWeaponSprite;
            weaponImage.color = unequippedWeaponColor;
            weaponPierceGameObject.SetActive(false);
            weaponKnockbackGameObject.SetActive(false);
        }
    }

    private void RefreshPlayertats() {
        int strength = (int)PlayerEquipment.Instance.GetStrength();
        int moveSpeed = (int)PlayerEquipment.Instance.GetMoveSpeed();
        int health = (int)PlayerEquipment.Instance.GetMaxHealth();
        int damage = (int)PlayerEquipment.Instance.GetDamage();

        float attackSpeed = PlayerEquipment.Instance.GetAttackSpeed();
        attackSpeed = Mathf.Round(attackSpeed * 10.0f) * 0.1f;
        int armor = (int)PlayerEquipment.Instance.GetArmor();

        strengthStatText.text = (strength).ToString();
        moveSpeedStatText.text = ((int)(moveSpeed / 50f)).ToString();
        healthStatText.text = (health).ToString();
        damageStatText.text = (damage).ToString();
        armorStatText.text = (armor).ToString();
        attackSpeedStatText.text = (attackSpeed).ToString();
    }

    public void OnEnable() {
        equipmentInventoryUI.gameObject.SetActive(false);
    }
}
