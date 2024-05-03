using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidsMenuUI : MonoBehaviour
{
    public static HumanoidsMenuUI Instance { get; private set; }

    private List<Humanoid> humanoidsSaved;
    private List<HumanoidSO.HumanoidType> humanoidTypeFilterList;
    private List<Humanoid.Job> humanoidJobFilterList;

    [SerializeField] private Transform humanoidsListContainer;
    [SerializeField] private HumanoidTemplateUI humanoidsTemplate;

    [SerializeField] private Sprite humanoidWorkerBackgroundSprite;
    [SerializeField] private Sprite humanoidHaulerBackgroundSprite;
    [SerializeField] private Sprite humanoidShipperBackgroundSprite;
    [SerializeField] private Sprite humanoidDungeoneerBackgroundSprite;
    [SerializeField] private Sprite humanoidUnassignedBackgroundSprite;

    private bool menuOpen;
    private float refreshRate = 2f;
    private float refreshTimer;
    [SerializeField] private GameObject humanoidMenuPanel;

    private void Awake() {
        Instance = this;
        humanoidMenuPanel.SetActive(false);
        humanoidsTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        InitializeFilterLists();
    }

    private void Update() {
        if (menuOpen) {
            refreshTimer += Time.deltaTime;

            if(refreshTimer > refreshRate) {
                refreshTimer = 0f;
                RefreshHumanoidsUIWhenOpen();
            }
        }
    }

    public void RefreshHumanoidsUI() {
        humanoidsSaved = HumanoidsManager.Instance.GetHumanoids();

        foreach(Transform child in humanoidsListContainer) {
            if (child == humanoidsTemplate.transform) {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach(Humanoid humanoid in humanoidsSaved) {
            if (!humanoidJobFilterList.Contains(humanoid.GetJob()) || !humanoidTypeFilterList.Contains(humanoid.GetHumanoidSO().humanoidType)) continue;

            Transform humanoidTemplate = Instantiate(humanoidsTemplate.transform, humanoidsListContainer);
            humanoidTemplate.gameObject.SetActive(true);
            HumanoidTemplateUI humanoidTemplateUI = humanoidTemplate.GetComponent<HumanoidTemplateUI>();
            humanoidTemplateUI.SetHumanoidTemplateUI(humanoid);
        }
    }

    private void RefreshHumanoidsUIWhenOpen() {
        foreach (Transform child in humanoidsListContainer) {
            if (child == humanoidsTemplate.transform) {
                continue;
            }

            HumanoidTemplateUI humanoidTemplateUI = child.GetComponent<HumanoidTemplateUI>();
            humanoidTemplateUI.RefreshHumanoidTemplateUI();
        }

    }

    private void InitializeFilterLists() {
        humanoidTypeFilterList = new List<HumanoidSO.HumanoidType>();
        humanoidJobFilterList = new List<Humanoid.Job>();

        humanoidTypeFilterList.Add(HumanoidSO.HumanoidType.Elf);
        humanoidTypeFilterList.Add(HumanoidSO.HumanoidType.Human);
        humanoidTypeFilterList.Add(HumanoidSO.HumanoidType.Orc);
        humanoidTypeFilterList.Add(HumanoidSO.HumanoidType.Dwarf);
        humanoidTypeFilterList.Add(HumanoidSO.HumanoidType.Goblin);
        humanoidTypeFilterList.Add(HumanoidSO.HumanoidType.Halfling);

        humanoidJobFilterList.Add(Humanoid.Job.Unassigned);
        humanoidJobFilterList.Add(Humanoid.Job.Worker);
        humanoidJobFilterList.Add(Humanoid.Job.Haulier);
        humanoidJobFilterList.Add(Humanoid.Job.Dungeoneer);
        humanoidJobFilterList.Add(Humanoid.Job.Shipper);
    }

    public Sprite GetHumanoidWorkerBackgroundSprite(Humanoid.Job job) {
        if(job == Humanoid.Job.Haulier) {
            return humanoidHaulerBackgroundSprite;
        }
        if(job == Humanoid.Job.Shipper) {
            return humanoidShipperBackgroundSprite;
        }
        if(job == Humanoid.Job.Dungeoneer) {
            return humanoidDungeoneerBackgroundSprite;
        }
        if(job == Humanoid.Job.Worker) {
            return humanoidWorkerBackgroundSprite;
        }
        if(job == Humanoid.Job.Unassigned) {
            return humanoidUnassignedBackgroundSprite;
        }

        return null;
    }

    public void ChangeHumanoidTypeFilter(HumanoidSO.HumanoidType humanoidType, bool filterActive) {
        if(filterActive) {
            humanoidTypeFilterList.Add(humanoidType);
        } else {
            humanoidTypeFilterList.Remove(humanoidType);
        }

        RefreshHumanoidsUI();
    }

    public void ChangeHumanoidJobFilter(Humanoid.Job humanoidJob, bool filterActive) {
        if (filterActive) {
            humanoidJobFilterList.Add(humanoidJob);
        }
        else {
            humanoidJobFilterList.Remove(humanoidJob);
        }

        RefreshHumanoidsUI();
    }

    public void OpenCloseHumanoidsMenu() {
        menuOpen = !menuOpen;
        humanoidMenuPanel.SetActive(menuOpen);
        if(menuOpen) {
            RefreshHumanoidsUI();

            BuildUI.Instance.CloseBuildHotbar();
            (Player.Instance.GetPlayerInventoryUI() as InventoryUI_Interactable).CloseInventoryPanel();
        }
    }

    public void CloseHumanoidsMenu() {
        menuOpen = false;
        humanoidMenuPanel.SetActive(menuOpen);
    }
}
