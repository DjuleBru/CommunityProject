using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment Instance;
    public event EventHandler OnEquipmentChanged;

    [SerializeField] private float playerBaseHealthMax;
    [SerializeField] private float playerBaseStrength;
    [SerializeField] private float playerBaseArmor;
    [SerializeField] private float playerBaseMoveSpeed;
    [SerializeField] private float playerBaseDamage;
    [SerializeField] private float playerBaseAttackSpeed;
    [SerializeField] private int playerBaseMaxItemHold;

    private Item mainHandItem;
    private Item secondaryHandItem;
    private Item helmetItem;
    private Item bootsItem;
    private Item necklaceItem;
    private Item ringItem;

    private float mainHandItemDurability;
    private float secondaryHandItemDurability;
    private float helmetItemDurability;
    private float bootsItemDurability;
    private float necklaceItemDurability;
    private float ringItemDurability;

    private float mainHandItemMaxDurability;
    private float secondaryHandItemMaxDurability;
    private float helmetItemMaxDurability;
    private float bootsItemMaxDurability;
    private float necklaceItemMaxDurability;
    private float ringItemMaxDurability;

    private List<Item> equippedItems = new List<Item>();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LoadPlayerEquipment();
    }

    private void Update() {
        // Scene is not a dungeon
        if (BuildingsManager.Instance != null) return;

        HandleEquipmentDurability();
    }

    private void LoadPlayerEquipment() {
        equippedItems = ES3.Load("playerEquipment", equippedItems);

        foreach (Item item in equippedItems) {
            Item.ItemEquipmentCategory category = ItemAssets.Instance.GetItemSO(item.itemType).itemEquipmentCategory;

            if (category == Item.ItemEquipmentCategory.main) {
                mainHandItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
                mainHandItemMaxDurability = mainHandItemDurability;
                mainHandItem = item;
                PlayerAttack.Instance.ChangeActiveWeaponSO(ItemAssets.Instance.GetItemSO(item.itemType).weaponSO);
            }

            if (category == Item.ItemEquipmentCategory.secondary) {
                secondaryHandItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
                secondaryHandItemMaxDurability = secondaryHandItemDurability;
                secondaryHandItem = item;
            }

            if (category == Item.ItemEquipmentCategory.head) {
                helmetItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
                helmetItemMaxDurability = helmetItemDurability;
                helmetItem = item;
            }

            if (category == Item.ItemEquipmentCategory.boots) {
                bootsItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
                bootsItemMaxDurability = bootsItemDurability;
                bootsItem = item;
            }

            if (category == Item.ItemEquipmentCategory.ring) {
                ringItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
                ringItemMaxDurability = ringItemDurability;
                ringItem = item;
            }

            if (category == Item.ItemEquipmentCategory.necklace) {
                necklaceItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
                necklaceItemMaxDurability = necklaceItemDurability;
                necklaceItem = item;
            }
        }
    }

    public void SetEquipmentType(Item item) {
        Item.ItemEquipmentCategory category = ItemAssets.Instance.GetItemSO(item.itemType).itemEquipmentCategory;

        if (category == Item.ItemEquipmentCategory.main) {

            mainHandItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            mainHandItemMaxDurability = mainHandItemDurability;

            if (mainHandItem != null && equippedItems.Contains(mainHandItem)) {
                if(mainHandItem.itemType == item.itemType) {
                    mainHandItem.amount += item.amount;
                } else {
                    equippedItems.Remove(mainHandItem);
                    mainHandItem = item;
                }
            } else {
                mainHandItem = item;
            }

            PlayerAttack.Instance.ChangeActiveWeaponSO(ItemAssets.Instance.GetItemSO(item.itemType).weaponSO);
        }

        if (category == Item.ItemEquipmentCategory.secondary) {
            if (secondaryHandItem != null && equippedItems.Contains(secondaryHandItem)) {
                equippedItems.Remove(secondaryHandItem);
            }

            secondaryHandItem = item;
            secondaryHandItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            secondaryHandItemMaxDurability = secondaryHandItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.head) {
            if (helmetItem != null && equippedItems.Contains(helmetItem)) {
                equippedItems.Remove(helmetItem);
            }

            helmetItem = item;
            helmetItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            helmetItemMaxDurability = helmetItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.boots) {
            if (bootsItem != null && equippedItems.Contains(bootsItem)) {
                equippedItems.Remove(bootsItem);
            }

            bootsItem = item;
            bootsItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            bootsItemMaxDurability = bootsItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.ring) {
            if (ringItem != null && equippedItems.Contains(ringItem)) {
                equippedItems.Remove(ringItem);
            }

            ringItem = item;
            ringItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            ringItemMaxDurability = ringItemDurability;
        }

        if (category == Item.ItemEquipmentCategory.necklace) {
            if (necklaceItem != null && equippedItems.Contains(necklaceItem)) {
                equippedItems.Remove(necklaceItem);
            }

            necklaceItem = item;
            necklaceItemDurability = ItemAssets.Instance.GetItemSO(item.itemType).equipmentDurability;
            necklaceItemMaxDurability = necklaceItemDurability;
        }

        equippedItems.Add(item);
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public Item GetEquipmentItem(Item.ItemEquipmentCategory category) {
        Item equippedItem = null;

        if (category == Item.ItemEquipmentCategory.main && mainHandItem != null && mainHandItem.itemType != Item.ItemType.Wood) {
            equippedItem = mainHandItem;
        }
        if (category == Item.ItemEquipmentCategory.secondary && secondaryHandItem != null && secondaryHandItem.itemType != Item.ItemType.Wood) {
            equippedItem = secondaryHandItem;
        }
        if (category == Item.ItemEquipmentCategory.head && helmetItem != null && helmetItem.itemType != Item.ItemType.Wood) {
            equippedItem = helmetItem;
        }
        if (category == Item.ItemEquipmentCategory.boots && bootsItem != null && bootsItem.itemType != Item.ItemType.Wood) {
            equippedItem = bootsItem;
        }
        if (category == Item.ItemEquipmentCategory.ring && ringItem != null && ringItem.itemType != Item.ItemType.Wood) {
            equippedItem = ringItem;
        }
        if (category == Item.ItemEquipmentCategory.necklace && necklaceItem != null && necklaceItem.itemType != Item.ItemType.Wood) {
            equippedItem = necklaceItem;
        }

        return equippedItem;
    }
    public float GetEquipmentItemDurabilityNormalized(Item.ItemEquipmentCategory category) {
        if (category == Item.ItemEquipmentCategory.main) {
            return mainHandItemDurability / mainHandItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.secondary) {
            return secondaryHandItemDurability / secondaryHandItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.head) {
            return helmetItemDurability / helmetItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.boots) {
            return bootsItemDurability / bootsItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.ring) {
            return ringItemDurability / ringItemMaxDurability;
        }
        if (category == Item.ItemEquipmentCategory.necklace) {
            return necklaceItemDurability / necklaceItemMaxDurability;
        }
        return 0;
    }

    private void HandleEquipmentDurability() {

        if (mainHandItem != null && mainHandItem.amount > 0) {
            mainHandItemDurability -= Time.deltaTime;

            if (mainHandItemDurability < 0) {
                mainHandItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (mainHandItem.amount == 0) {
                    mainHandItemDurability = 0;
                }
                else {
                    mainHandItemDurability = mainHandItemMaxDurability;
                }
            }
        }

        if (secondaryHandItem != null && secondaryHandItem.amount > 0) {
            secondaryHandItemDurability -= Time.deltaTime;

            if (secondaryHandItemDurability < 0) {
                secondaryHandItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);

                if (secondaryHandItem.amount == 0) {
                    secondaryHandItemDurability = 0;
                }
                else {
                    secondaryHandItemDurability = secondaryHandItemMaxDurability;
                }
            }
        }

        if (helmetItem != null && helmetItem.amount > 0) {
            helmetItemDurability -= Time.deltaTime;

            if (helmetItemDurability < 0) {
                helmetItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (helmetItem.amount == 0) {
                    helmetItemDurability = 0;
                }
                else {
                    helmetItemDurability = helmetItemMaxDurability;
                }
            }
        }

        if (bootsItem != null && bootsItem.amount > 0) {
            bootsItemDurability -= Time.deltaTime;

            if (bootsItemDurability < 0) {
                bootsItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (bootsItem.amount == 0) {
                    bootsItemDurability = 0;
                }
                else {
                    bootsItemDurability = bootsItemMaxDurability;
                }
            }
        }

        if (necklaceItem != null && necklaceItem.amount > 0) {
            necklaceItemDurability -= Time.deltaTime;

            if (necklaceItemDurability < 0) {
                necklaceItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
                if (necklaceItem.amount == 0) {
                    necklaceItemDurability = 0;
                }
                else {
                    necklaceItemDurability = necklaceItemMaxDurability;
                }
            }
        }

        if (ringItem != null && ringItem.amount > 0) {
            ringItemDurability -= Time.deltaTime;

            if (ringItemDurability < 0) {
                ringItem.amount--;
                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);

                if (ringItem.amount == 0) {
                    ringItemDurability = 0;
                }
                else {
                    ringItemDurability = ringItemMaxDurability;
                }
            }
        }

    }

    public int GetAmountToEquip(ItemSO itemSO) {
        int amountToAddToEquipment = GetPlayerMaxItemHold();

        if (itemSO.itemEquipmentCategory == Item.ItemEquipmentCategory.main && mainHandItem != null && mainHandItem.itemType != Item.ItemType.Wood) {
            if(itemSO.itemType != mainHandItem.itemType) return amountToAddToEquipment;
            return GetPlayerMaxItemHold() - mainHandItem.amount;
        }

        if (itemSO.itemEquipmentCategory == Item.ItemEquipmentCategory.secondary && secondaryHandItem != null && secondaryHandItem.itemType != Item.ItemType.Wood) {
            if (itemSO.itemType != secondaryHandItem.itemType) return amountToAddToEquipment;
            return GetPlayerMaxItemHold() - secondaryHandItem.amount;
        }

        if (itemSO.itemEquipmentCategory == Item.ItemEquipmentCategory.head && helmetItem != null && helmetItem.itemType != Item.ItemType.Wood) {
            if (itemSO.itemType != helmetItem.itemType) return amountToAddToEquipment;
            return GetPlayerMaxItemHold() - helmetItem.amount;
        }

        if (itemSO.itemEquipmentCategory == Item.ItemEquipmentCategory.boots && bootsItem != null && bootsItem.itemType != Item.ItemType.Wood) {
            if (itemSO.itemType != bootsItem.itemType) return amountToAddToEquipment;
            return GetPlayerMaxItemHold() - bootsItem.amount;
        }

        if (itemSO.itemEquipmentCategory == Item.ItemEquipmentCategory.ring && ringItem != null && ringItem.itemType != Item.ItemType.Wood) {
            if (itemSO.itemType != ringItem.itemType) return amountToAddToEquipment;
            return GetPlayerMaxItemHold() - ringItem.amount;
        }

        if (itemSO.itemEquipmentCategory == Item.ItemEquipmentCategory.necklace && necklaceItem != null && necklaceItem.itemType != Item.ItemType.Wood) {
            if (itemSO.itemType != necklaceItem.itemType) return amountToAddToEquipment;
            return GetPlayerMaxItemHold() - necklaceItem.amount;
        }

        return amountToAddToEquipment;
    }

    public int GetPlayerMaxItemHold() {

        int totalItemHold = playerBaseMaxItemHold;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalItemHold += ItemAssets.Instance.GetItemSO(item.itemType).strengthBonusValue;
        }

        return totalItemHold;

    }

    public float GetStrength() {

        float totalStrength = playerBaseStrength;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalStrength += ItemAssets.Instance.GetItemSO(item.itemType).strengthBonusValue;
        }

        return totalStrength;
    }

    public float GetMoveSpeed() {

        float totalMoveSpeed = playerBaseMoveSpeed;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalMoveSpeed += ItemAssets.Instance.GetItemSO(item.itemType).moveSpeedBonusValue;
        }

        return totalMoveSpeed;
    }

    public float GetMaxHealth() {

        float maxHealth = playerBaseHealthMax;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            maxHealth += ItemAssets.Instance.GetItemSO(item.itemType).healthBonusValue;
        }

        return maxHealth;
    }

    public float GetDamage() {

        float damage = playerBaseDamage;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            damage += ItemAssets.Instance.GetItemSO(item.itemType).damageBonusValue;
        }

        return damage;
    }

    public float GetAttackSpeed() {

        float attackSpeed = playerBaseAttackSpeed;

        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            if (ItemAssets.Instance.GetItemSO(item.itemType).attackSpeedMultiplier == 0) continue;
            attackSpeed /= ItemAssets.Instance.GetItemSO(item.itemType).attackSpeedMultiplier;
        }

        return attackSpeed;
    }

    public float GetArmor() {

        float totalArmor = playerBaseArmor;
        foreach (Item item in equippedItems) {
            if (item.amount == 0) continue;
            totalArmor += ItemAssets.Instance.GetItemSO(item.itemType).armorBonusValue;
        }

        return totalArmor;
    }

    public List<Item> GetEquippmentItems() {
        return equippedItems;
    }
}
