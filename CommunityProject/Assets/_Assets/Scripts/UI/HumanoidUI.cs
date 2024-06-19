using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanoidUI : MonoBehaviour
{

    public static HumanoidUI Instance { get; private set; }

    [SerializeField] private GameObject humanoidUIPanel;
    [SerializeField] private InventoryUI equipmentInventoryUI;
    [SerializeField] private InventoryUI humanoidInventoryUI;

    private bool panelOpen;

    private Humanoid humanoid;
    private HumanoidNeeds humanoidNeeds;
    private HumanoidEquipmentButton lastEquipmentButtonPressed;

    [SerializeField] private Transform proficiencyContainer;
    [SerializeField] private Transform proficiencyTemplate;

    [SerializeField] private Image hungerBarFillImage;
    [SerializeField] private Image energyBarFillImage;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image humanoidIcon;
    [SerializeField] private Image jobBackgroundImage;

    [SerializeField] private HumanoidEquipmentButton mainHandSlot;
    [SerializeField] private HumanoidEquipmentButton secondaryHandSlot;
    [SerializeField] private HumanoidEquipmentButton headSlot;
    [SerializeField] private HumanoidEquipmentButton bootsSlot;
    [SerializeField] private HumanoidEquipmentButton trinket1Slot;
    [SerializeField] private HumanoidEquipmentButton trinket2Slot;

    [SerializeField] private Image autoAssignBestEquipmentImage;

    [SerializeField] private TextMeshProUGUI humanoidNameText;
    [SerializeField] private TextMeshProUGUI humanoidJobText;
    [SerializeField] private TextMeshProUGUI humanoidDescriptionText;

    [SerializeField] private Button housingCameraButton;
    [SerializeField] private Image housingIcon;
    [SerializeField] private Button mainBuildingButton;
    [SerializeField] private Image mainBuildingIcon;

    [SerializeField] private TextMeshProUGUI strenthStatText;
    [SerializeField] private TextMeshProUGUI strenthStatBonusText;
    [SerializeField] private TextMeshProUGUI intelligenceStatText;
    [SerializeField] private TextMeshProUGUI intelligenceBonusStatText;
    [SerializeField] private TextMeshProUGUI moveSpeedStatText;
    [SerializeField] private TextMeshProUGUI moveSpeedBonusStatText;
    [SerializeField] private TextMeshProUGUI agilityStatText;
    [SerializeField] private TextMeshProUGUI agilityBonusStatText;
    [SerializeField] private TextMeshProUGUI totalOverworldProductivity;
    [SerializeField] private TextMeshProUGUI healthStatText;
    [SerializeField] private TextMeshProUGUI healthStatBonusText;
    [SerializeField] private TextMeshProUGUI damageStatText;
    [SerializeField] private TextMeshProUGUI damageStatBonusText;
    [SerializeField] private TextMeshProUGUI armorStatText;
    [SerializeField] private TextMeshProUGUI armorStatBonusText;
    [SerializeField] private TextMeshProUGUI totalDungeonProductivity;

    [SerializeField] private Color semiTransparentColor;

    private void Awake() {
        Instance = this;
        humanoidUIPanel.SetActive(false);
        equipmentInventoryUI.gameObject.SetActive(false);
        proficiencyTemplate.gameObject.SetActive(false);
    }

    public bool GetPanelOpen() {
        return panelOpen;
    }

    public void ClosePanel() {
        panelOpen = false;
        humanoidUIPanel.SetActive(false);
        equipmentInventoryUI.gameObject.SetActive(false);
    }

    public void OpenPanel() {
        panelOpen = true;
        humanoidUIPanel.SetActive(true);
    }

    private void Start() {
        mainBuildingButton.onClick.AddListener(() => {
            FreeCameraViewManager.Instance.SetFreeCamera(true);
            FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetAssignedBuilding().transform.position);
        });

        housingCameraButton.onClick.AddListener(() => {
            FreeCameraViewManager.Instance.SetFreeCamera(true);
            FreeCameraViewManager.Instance.ZoomToLocation(humanoid.GetComponent<HumanoidNeeds>().GetAssignedHousing().transform.position);
        });
    }

    private void Update() {
        if (panelOpen) {
            hungerBarFillImage.fillAmount = humanoidNeeds.GetHunger() / 100;
            energyBarFillImage.fillAmount = humanoidNeeds.GetEnergy() / 100;
            healthBarFillImage.fillAmount = humanoid.GetHealthNormalized();
        }
    }

    public void SetHumanoid(Humanoid humanoid) {
        if(this.humanoid != null) {
            this.humanoid.OnEquipmentChanged -= Humanoid_OnEquipmentChanged;
        }

        this.humanoid = humanoid;
        humanoid.OnEquipmentChanged += Humanoid_OnEquipmentChanged;
        humanoidNeeds = humanoid.GetComponent<HumanoidNeeds>();
        humanoidInventoryUI.ReplaceInventory(humanoid.GetComponent<HumanoidCarry>().GetHumanoidCarryInventory());

        humanoidIcon.sprite = humanoid.GetHumanoidSO().humanoidSprite;
        humanoidNameText.text = humanoid.GetHumanoidName();
        humanoidJobText.text = humanoid.GetJob().ToString();
        humanoidDescriptionText.text = humanoid.GetHumanoidActionDescription();
        jobBackgroundImage.sprite = HumanoidsMenuUI.Instance.GetHumanoidWorkerBackgroundSprite(humanoid.GetJob());

        RefreshHumanoidEquipment();
        RefreshHumanoidAssignments();
        RefreshHumanoidStats();
        RefreshHumanoidProficiencies();
    }

    private void RefreshHumanoidProficiencies() {

        foreach(Transform child in proficiencyContainer) {
            if (child == proficiencyTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(Building.BuildingWorksCategory category in humanoid.GetHumanoidSO().humanoidProficiencies) {
            Transform proficiency = Instantiate(proficiencyTemplate, proficiencyContainer);
            proficiency.GetComponent<Image>().sprite = BuildingsManager.Instance.GetWorkingCategorySprite(category);
            proficiency.gameObject.SetActive(true);
        }
    }

    private void Humanoid_OnEquipmentChanged(object sender, System.EventArgs e) {
        RefreshHumanoidEquipment();
        RefreshHumanoidStats();
    }

    private void RefreshHumanoidEquipment() {
        mainHandSlot.SetItem(humanoid.GetMainHandItem());
        secondaryHandSlot.SetItem(humanoid.GetSecondaryHandItem());
        headSlot.SetItem(humanoid.GetHeadItem());
        bootsSlot.SetItem(humanoid.GetBootsItem());
        trinket1Slot.SetItem(humanoid.GetNecklaceItem());
        trinket2Slot.SetItem(humanoid.GetRingItem());

        bool autoAssignBestEquipment = humanoid.GetAutoAssignBestEquipment();
        if(autoAssignBestEquipment) {
            autoAssignBestEquipmentImage.color = Color.white;
        } else {
            autoAssignBestEquipmentImage.color = semiTransparentColor;
        }
    }

    private void RefreshHumanoidAssignments() {
        if(humanoid.GetAssignedBuilding() != null) {
            mainBuildingIcon.color = Color.white;
            mainBuildingIcon.sprite = humanoid.GetAssignedBuilding().GetBuildingSO().buildingIconSprite;
            mainBuildingButton.gameObject.SetActive(true);
        } else {
            mainBuildingIcon.color = Color.clear;
            mainBuildingButton.gameObject.SetActive(false);
        }

        if (humanoid.GetComponent<HumanoidNeeds>().GetAssignedHousing() != null) {
            housingIcon.color = Color.white;
            housingIcon.sprite = humanoid.GetComponent<HumanoidNeeds>().GetAssignedHousing().GetBuildingSO().buildingIconSprite;
            housingCameraButton.gameObject.SetActive(true);
        } else {
            housingIcon.color = Color.clear;
            housingCameraButton.gameObject.SetActive(false);
        }

    }

    private void RefreshHumanoidStats() {
        int strength = (int)humanoid.GetStrength();
        int intelligence = (int)humanoid.GetIntelligence();
        int agility = (int)humanoid.GetAgility();
        int moveSpeed = (int)humanoid.GetMoveSpeed();
        int health = (int)humanoid.GetMaxHealth();
        int damage = (int)humanoid.GetDamage();
        int armor = (int)humanoid.GetArmor();

        int bonusStrength = strength - (int)humanoid.GetHumanoidSO().strength;
        int bonusintelligence = intelligence - (int)humanoid.GetHumanoidSO().intelligence;
        int bonusMoveSpeed = moveSpeed - (int)humanoid.GetHumanoidSO().moveSpeed;
        int bonusAgility = agility - (int)humanoid.GetHumanoidSO().agility;
        int bonusHealth = health - (int)humanoid.GetHumanoidSO().maxHealth;
        int bonusDamage = damage - (int)humanoid.GetHumanoidSO().damage;
        int bonusArmor = armor - (int)humanoid.GetHumanoidSO().armor;

        strenthStatText.text = (strength).ToString();
        intelligenceStatText.text = (intelligence).ToString();
        moveSpeedStatText.text = ((int)(moveSpeed /50f)).ToString();
        healthStatText.text = (health).ToString();
        damageStatText.text = (damage).ToString();
        armorStatText.text = (armor).ToString();
        agilityStatText.text = (armor).ToString();

        if (bonusStrength != 0) {
            strenthStatBonusText.text = "(+" + (bonusStrength).ToString() + ")";
        }
        else {
            strenthStatBonusText.text = "";
        }

        if (bonusintelligence != 0) {
            intelligenceBonusStatText.text = "(+" + (bonusintelligence).ToString() + ")";
        }
        else {
            intelligenceBonusStatText.text = "";
        }

        if (bonusAgility != 0) {
            agilityBonusStatText.text = "(+" + (bonusAgility).ToString() + ")";
        }
        else {
            agilityBonusStatText.text = "";
        }

        if (bonusMoveSpeed != 0) {
            moveSpeedBonusStatText.text = "(+" + (bonusMoveSpeed).ToString() + ")";
        }
        else {
            moveSpeedBonusStatText.text = "";
        }

        if (bonusHealth != 0) {
            healthStatBonusText.text = "(+" + (bonusHealth).ToString() + ")";
        }
        else {
            healthStatBonusText.text = "";
        }

        if (bonusDamage != 0) {
            damageStatBonusText.text = "(+" + (bonusDamage).ToString() + ")";
        }
        else {
            damageStatBonusText.text = "";
        }

        if (bonusArmor != 0) {
            armorStatBonusText.text = "(+" + (bonusArmor).ToString() + ")";
        }
        else {
            armorStatBonusText.text = "";
        }


    }

    public void OpenInventoryUI(Item.ItemEquipmentCategory category, HumanoidEquipmentButton humanoidEquipmentButton) {
        equipmentInventoryUI.gameObject.SetActive(true);

        Inventory equipmentInventory = new Inventory(false, 0, 0, 0);

        foreach(Item item in BuildingsManager.Instance.GetAllEquipmentItemsOfCategory(category)) {
            equipmentInventory.AddItem(item);
        }

        equipmentInventoryUI.SetInventory(equipmentInventory);
        lastEquipmentButtonPressed = humanoidEquipmentButton;
    }

    public void CloseInventoryUI() {
        equipmentInventoryUI.gameObject.SetActive(false);
    }

    public Humanoid GetHumanoid() {
        return humanoid;
    }

    public void SetItemToEquip(Item item) {
        ItemSO itemSO = ItemAssets.Instance.GetItemSO(item.itemType);

        lastEquipmentButtonPressed.SetItem(item);

        humanoid.SetEquipmentType(item);
    }

    public void ToggleAutoAssignBestEquipment() {
        bool autoAssignBestEquipment = humanoid.GetAutoAssignBestEquipment();

        humanoid.SetAutoAssignBestEquipment((!autoAssignBestEquipment));

        if (!autoAssignBestEquipment) {
            autoAssignBestEquipmentImage.color = Color.white;
        }
        else {
            autoAssignBestEquipmentImage.color = semiTransparentColor;
        }
    }

}
