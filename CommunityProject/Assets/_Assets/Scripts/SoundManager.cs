using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioRefsSO audioRefs;
    [SerializeField] private AnimationCurve volumeDecreaseWithDistance;

    private float musicVolume = .5f;
    private float sfxVolume = .5f;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        musicVolume = ES3.Load("musicVolume", musicVolume);
        sfxVolume = ES3.Load("sfxVolume", sfxVolume);
        MusicManager.Instance.SetVolume(musicVolume);


        ButtonUI_SFX.OnAnyButtonUIClick += ButtonUI_SFX_OnAnyButtonUIClick;
        ButtonUI_SFX.OnAnyButtonUIHover += ButtonUI_SFX_OnAnyButtonUIHover;
        PauseMenuUI.OnPauseMenuClosed += PauseMenuUI_OnPauseMenuClosed;
        PauseMenuUI.OnPauseMenuOpen += PauseMenuUI_OnPauseMenuOpen;
        RecipeSlot_Selectable.OnAnyRecipeSlotSelected += RecipeSlot_Selectable_OnAnyRecipeSlotSelected;
        RecipeSlot_Selectable.OnAnyRecipeSlotHovered += RecipeSlot_Selectable_OnAnyRecipeSlotHovered;
        HumanoidEquipmentButton.OnAnyEquipmentButtonHovered += HumanoidEquipmentButton_OnAnyEquipmentButtonHovered;
        HumanoidEquipmentButton.OnAnyEquipmentButtonPressed += HumanoidEquipmentButton_OnAnyEquipmentButtonPressed;
        HumanoidEquipItemButton.OnAnyEquipItemHovered += HumanoidEquipItemButton_OnAnyEquipItemHovered;
        HumanoidEquipItemButton.OnAnyEquipItemPressed += HumanoidEquipItemButton_OnAnyEquipItemPressed;
        ItemSlot_Inventory.OnAnyItemHovered += ItemSlot_Inventory_OnAnyItemHovered;
        ItemSlot_Inventory.OnAnyItemDropped += ItemSlot_Inventory_OnAnyItemDropped;
        ItemSlot_Inventory.OnAnyItemFailedAction += ItemSlot_Inventory_OnAnyItemFailedAction;
        ItemSlot_Inventory.OnAnyItemSplitted += ItemSlot_Inventory_OnAnyItemSplitted;
        ItemSlot_Inventory.OnAnyItemTransfered += ItemSlot_Inventory_OnAnyItemTransfered;
        SelectItemButton.OnAnySelectItemButtonPressed += SelectItemButton_OnAnySelectItemButtonPressed;
        SelectItemButton.OnAnySelectItemButtonHovered += SelectItemButton_OnAnySelectItemButtonHovered;
        PlayerEquipItemButton.OnAnyPlayerEquipmentItemHovered += PlayerEquipItemButton_OnAnyPlayerEquipmentItemHovered;
        PlayerEquipItemButton.OnAnyPlayerEquipmentItemEquipped += PlayerEquipItemButton_OnAnyPlayerEquipmentItemEquipped;
        PlayerEquipmentButton.OnAnyEquipmentItemHovered += PlayerEquipmentButton_OnAnyEquipmentItemHovered;
        PlayerEquipmentButton.OnAnyEquipmentItemPressed += PlayerEquipmentButton_OnAnyEquipmentItemPressed;

        Chest.OnAnyChestOpened += Chest_OnAnyChestOpened;
        Chest.OnAnyChestClosed += Chest_OnAnyChestClosed;
        Building.OnAnyBuildingDestroyed += Building_OnAnyBuildingDestroyed;
        Building.OnAnyBuildingPlaced += Building_OnAnyBuildingPlaced;
        ProductionBuilding.OnAnyRecipeProduced += ProductionBuilding_OnAnyRecipeProduced;

        PlayerAttack.Instance.OnPlayerAttack += PlayerAttack_OnPlayerAttack;
        Mob.OnAnyMobDamaged += Mob_OnAnyMobDamaged;
        Mob.OnAnyMobDied += Mob_OnAnyMobDied;
        MobAttack.OnAnyMobAttack += MobAttack_OnAnyMobAttack;
        MobAttack.OnAnyMobRangedAttack += MobAttack_OnAnyMobRangedAttack;

        DungeonRoom.OnAnyMobAppear += DungeonRoom_OnAnyMobAppear;
        DungeonRoom.OnAnyDoorClosed += DungeonRoom_OnAnyDoorClosed;
    }

    private void DungeonRoom_OnAnyDoorClosed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.doorClose, Camera.main.transform.position, 2.5f);
    }

    private void DungeonRoom_OnAnyMobAppear(object sender, System.EventArgs e) {
        PlaySound(audioRefs.mobAppear, Camera.main.transform.position, .25f);
    }

    private void MobAttack_OnAnyMobAttack(object sender, System.EventArgs e) {
        MobAttack mobAttack = (MobAttack)sender;
        AudioClip[] audioClip = mobAttack.GetComponent<Mob>().GetMobSO().attackAudioClip;

        if(audioClip != null && audioClip.Length > 0) {
            PlaySound(audioClip, Camera.main.transform.position);
        }
    }
    private void MobAttack_OnAnyMobRangedAttack(object sender, System.EventArgs e) {
        MobAttack mobAttack = (MobAttack)sender;
        AudioClip[] audioClip = mobAttack.GetComponent<Mob>().GetMobSO().rangedAttackReleaseAudioClip;

        if (audioClip != null && audioClip.Length > 0) {
            PlaySound(audioClip, Camera.main.transform.position);
        }
    }


    private void Mob_OnAnyMobDied(object sender, System.EventArgs e) {
        Mob mob = (Mob)sender;
        AudioClip[] audioClip = mob.GetMobSO().DieAudioClip;

        if (audioClip != null && audioClip.Length > 0) {
            PlaySound(audioClip, Camera.main.transform.position);
        }
    }

    private void Mob_OnAnyMobDamaged(object sender, System.EventArgs e) {
        Mob mob = (Mob)sender;
        AudioClip[] audioClip = mob.GetMobSO().hitAudioClip;

        if (audioClip != null && audioClip.Length > 0) {
            PlaySound(audioClip, Camera.main.transform.position);
        }
    }

    private void PlayerAttack_OnPlayerAttack(object sender, System.EventArgs e) {
        PlaySound(PlayerAttack.Instance.GetActiveWeaponSO().attackAudioClips, Camera.main.transform.position, .5f);
    }

    private void ProductionBuilding_OnAnyRecipeProduced(object sender, System.EventArgs e) {
        ProductionBuilding productionBuilding  = (ProductionBuilding)sender;

        AudioClip[] productionAudioClipArray = productionBuilding.GetBuildingSO().buildingProductionClip;

        if (productionAudioClipArray == null || productionAudioClipArray.Length == 0) return;

        PlaySound(productionAudioClipArray, productionBuilding.transform.position);
    }

    private void Building_OnAnyBuildingPlaced(object sender, System.EventArgs e) {
        PlaySound(audioRefs.buildingPlaced, Camera.main.transform.position, .65f);
    }

    private void Building_OnAnyBuildingDestroyed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.buildingDestroyed, Camera.main.transform.position);
    }

    private void Chest_OnAnyChestClosed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.chestClosed, Camera.main.transform.position);
    }

    private void Chest_OnAnyChestOpened(object sender, System.EventArgs e) {
        PlaySound(audioRefs.chestOpen, Camera.main.transform.position);
    }

    #region UI

    private void PlayerEquipmentButton_OnAnyEquipmentItemPressed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.buttonPress, Camera.main.transform.position);
    }

    private void PlayerEquipmentButton_OnAnyEquipmentItemHovered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemHover, Camera.main.transform.position);
    }

    private void PlayerEquipItemButton_OnAnyPlayerEquipmentItemEquipped(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemEquip, Camera.main.transform.position);
    }

    private void PlayerEquipItemButton_OnAnyPlayerEquipmentItemHovered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemHover, Camera.main.transform.position);
    }

    private void SelectItemButton_OnAnySelectItemButtonHovered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemHover, Camera.main.transform.position);
    }

    private void SelectItemButton_OnAnySelectItemButtonPressed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemTransfer, Camera.main.transform.position);
    }

    private void ItemSlot_Inventory_OnAnyItemTransfered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemTransfer, Camera.main.transform.position);
    }

    private void ItemSlot_Inventory_OnAnyItemSplitted(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemSplit, Camera.main.transform.position);
    }

    private void ItemSlot_Inventory_OnAnyItemFailedAction(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemFailedError, Camera.main.transform.position);
    }

    private void ItemSlot_Inventory_OnAnyItemDropped(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemDrop, Camera.main.transform.position);
    }

    private void ItemSlot_Inventory_OnAnyItemHovered(object sender, System.EventArgs e) {
        //PlaySound(audioRefs.itemHover, Camera.main.transform.position, .4f);
    }

    private void HumanoidEquipItemButton_OnAnyEquipItemPressed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemEquip, Camera.main.transform.position);
    }

    private void HumanoidEquipItemButton_OnAnyEquipItemHovered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.equipmentHover, Camera.main.transform.position);
    }

    private void HumanoidEquipmentButton_OnAnyEquipmentButtonPressed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.buttonPress, Camera.main.transform.position);
    }

    private void HumanoidEquipmentButton_OnAnyEquipmentButtonHovered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.equipmentHover, Camera.main.transform.position);
    }

    private void RecipeSlot_Selectable_OnAnyRecipeSlotHovered(object sender, System.EventArgs e) {
        PlaySound(audioRefs.recipeSlotHovered, Camera.main.transform.position);
    }

    private void RecipeSlot_Selectable_OnAnyRecipeSlotSelected(object sender, System.EventArgs e) {
        PlaySound(audioRefs.recipeSlotSelected, Camera.main.transform.position);
    }

    private void PauseMenuUI_OnPauseMenuOpen(object sender, System.EventArgs e) {
        PlaySound(audioRefs.pauseMenuOpen, Camera.main.transform.position);
    }

    private void PauseMenuUI_OnPauseMenuClosed(object sender, System.EventArgs e) {
        PlaySound(audioRefs.pauseMenuClose, Camera.main.transform.position);
    }

    private void ButtonUI_SFX_OnAnyButtonUIHover(object sender, System.EventArgs e) {
        PlaySound(audioRefs.itemHover, Camera.main.transform.position);
    }

    private void ButtonUI_SFX_OnAnyButtonUIClick(object sender, System.EventArgs e) {
        PlaySound(audioRefs.buttonPress, Camera.main.transform.position);
    }
    #endregion
    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {

        Vector3 soundOrigin = position;
        soundOrigin.z = Camera.main.transform.position.z;

        float distanceToPlayer = Vector3.Distance(soundOrigin, Camera.main.transform.position);

        float maxDistanceToHearSound = 5f;
        float distanceToPlayerNormalized = 1;

        if (distanceToPlayer < maxDistanceToHearSound) {
            distanceToPlayerNormalized = distanceToPlayer/ maxDistanceToHearSound;
        }



        float volumeDistanceDecreaseMultiplier = volumeDecreaseWithDistance.Evaluate(distanceToPlayerNormalized);

        Debug.Log(audioClip + " volume " + volume * sfxVolume * volumeDistanceDecreaseMultiplier);
        AudioSource.PlayClipAtPoint(audioClip, soundOrigin, volume * sfxVolume * volumeDistanceDecreaseMultiplier);

    }

    public void SetSFXVolume(float volume) {
        sfxVolume = volume;
    }
    public void SetMusicVolume(float volume) {
        musicVolume = volume;
        MusicManager.Instance.SetVolume(musicVolume);
    }

    public float GetSFXVolume() {
        return sfxVolume;
    }


    private void OnApplicationQuit() {
        ES3.Save("musicVolume", musicVolume);
        ES3.Save("sfxVolume", sfxVolume);
    }

    public float GetMusicVolume() {
        return musicVolume;
    }
}
