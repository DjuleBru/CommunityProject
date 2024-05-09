using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonRecapUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dungeonNameText;
    [SerializeField] private TextMeshProUGUI dungeonDifficultyValueText;

    [SerializeField] private GameObject mobTemplatePrefab;
    [SerializeField] private GameObject resourcesTemplatePrefab;
    [SerializeField] private GameObject humanoidsTemplatePrefab;

    [SerializeField] private Transform mobTemplateContainer;
    [SerializeField] private Transform resourcesTemplateContainer;
    [SerializeField] private Transform humanoidsTemplateContainer;


    private DungeonEntrance dungeonEntrance;
    private DungeonSO dungeonSO;

    private void Awake() {
        dungeonEntrance = GetComponentInParent<DungeonEntrance>();

        dungeonSO = dungeonEntrance.GetDungeonSO();
        dungeonNameText.text = dungeonSO.name;
        dungeonDifficultyValueText.text = dungeonSO.dungeonDifficulty.ToString();
    }

    private void Start() {
        InstantiateMobTemplates();
        InstantiateResourceTemplates();
        InstantiateHumanoidsTemplates();
    }

    private void InstantiateMobTemplates() {
        foreach(MobSO mobSO in dungeonSO.mobsFoundInDungeon) {
            GameObject mobPrefab = Instantiate(mobTemplatePrefab, mobTemplateContainer);
            mobPrefab.transform.Find("MobSprite").GetComponent<Image>().sprite = mobSO.mobIconSprite;
        }

        mobTemplatePrefab.SetActive(false);
    }

    private void InstantiateResourceTemplates() {
        foreach (ItemSO itemSO in dungeonSO.itemsFoundInDungeon) {
            GameObject itemPrefab = Instantiate(resourcesTemplatePrefab, resourcesTemplateContainer);
            itemPrefab.transform.Find("ResourceSprite").GetComponent<Image>().sprite = itemSO.itemSprite;
        }

        resourcesTemplatePrefab.SetActive(false);
    }

    private void InstantiateHumanoidsTemplates() {

        Debug.Log(dungeonSO.humanoidsToSaveFromDungeon + " " + dungeonEntrance.GetDungeonStatsBoard().GetRecordedHumanoidsNumberSaved());
        int humanoidsRemainingInDungeon = dungeonSO.humanoidsToSaveFromDungeon - dungeonEntrance.GetDungeonStatsBoard().GetRecordedHumanoidsNumberSaved();

        for (int i = 0; i < humanoidsRemainingInDungeon; i++) {
            GameObject itemPrefab = Instantiate(humanoidsTemplatePrefab, humanoidsTemplateContainer);
            itemPrefab.transform.Find("HumanoidSprite").GetComponent<Image>().sprite = dungeonSO.humanoidTypeFoundInDungeon.humanoidSprite;
        }

        humanoidsTemplatePrefab.SetActive(false);
    }


}
