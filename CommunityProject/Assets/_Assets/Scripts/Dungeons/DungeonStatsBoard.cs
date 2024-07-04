using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonStatsBoard : MonoBehaviour, IInteractable {

    private DungeonEntrance dungeonEntrance;

    [SerializeField] private List<Item> recordedDungeonLoot;
    [SerializeField] private float recordedDungeonTime;
    [SerializeField] private int recordedhumanoidsSaved;

    [SerializeField] private List<Item> previousRunDungeonLoot;
    [SerializeField] private float previousRunDungeonTime;

    [SerializeField] private DungeonStatsBoardUI recordedStatsBoardUI;
    [SerializeField] private DungeonStatsBoardUI previousRunStatsBoardUI;
    [SerializeField] private Transform recordedStatsBoardUIInitialPosition;
    [SerializeField] private Transform recordedStatsBoardUIPositionToReplaceStats;

    [SerializeField] private GameObject statsBoardHoveredVisual;
    [SerializeField] private GameObject statsBoardUIGameObject;
    [SerializeField] private DungeonStatsBoardWorldUI dungeonStatsBoardWorldUI;

    [SerializeField] private Collider2D solidStatsBoardCollider;

    private bool playerInTriggerArea;
    private bool statsBoardOpen;

    private void Awake() {
        dungeonEntrance = GetComponentInParent<DungeonEntrance>();
    }

    private void Start() {
        if(previousRunDungeonLoot.Count == 0) {
            recordedStatsBoardUI.transform.position = recordedStatsBoardUIInitialPosition.position;
            previousRunStatsBoardUI.gameObject.SetActive(false);
        }

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

        statsBoardHoveredVisual.SetActive(false);
        statsBoardUIGameObject.gameObject.SetActive(false);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (playerInTriggerArea) {
            if (statsBoardOpen) {
                statsBoardUIGameObject.gameObject.SetActive(false);
                statsBoardOpen = false;
            }
            else {
                statsBoardUIGameObject.gameObject.SetActive(true);
                statsBoardOpen = true;
            }
        }
    }

    public void RecordDungeon(List<Item> loot, float time, int humanoidsSaved) {
        List<Item> lootCopy = new List<Item>();

        foreach(Item item in loot) {
            Item itemToAdd = new Item {itemType = item.itemType, amount = item.amount};
            lootCopy.Add(itemToAdd);
        }
        recordedDungeonLoot = lootCopy;
        recordedDungeonTime = time;
        recordedhumanoidsSaved = humanoidsSaved;

        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);

        foreach(Item item in loot) {
            Debug.Log(item.itemType + " " + item.amount);
        }
    }

    public void RecordLastRun(List<Item> loot, float time, int newHumanoidsSaved) {
        Debug.Log("recording last run " + time);
        previousRunStatsBoardUI.gameObject.SetActive(true);

        recordedhumanoidsSaved += newHumanoidsSaved;

        previousRunDungeonLoot = loot;
        previousRunDungeonTime = time;

        previousRunStatsBoardUI.SetDungeonLootUI(previousRunDungeonLoot);
        previousRunStatsBoardUI.SetDungeonTimeUI(previousRunDungeonTime);

        recordedStatsBoardUI.transform.position = recordedStatsBoardUIPositionToReplaceStats.position;
    }

    public void ReplaceRecordedStats() {
        recordedDungeonLoot = previousRunDungeonLoot;
        recordedDungeonTime = previousRunDungeonTime;

        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);

        previousRunStatsBoardUI.gameObject.SetActive(false);
        recordedStatsBoardUI.transform.position = recordedStatsBoardUIInitialPosition.position;

        dungeonEntrance.SaveDungeon();
    }

    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        this.playerInTriggerArea = playerInTriggerArea;
    }

    public bool GetPlayerInTriggerArea() {
        return playerInTriggerArea;
    }

    public void SetHovered(bool hovered) {
        statsBoardHoveredVisual.SetActive(hovered);
    }

    public float GetDungeonTime() {
        return recordedDungeonTime;
    }

    public List<Item> GetDungeonLoot() {
        return recordedDungeonLoot;
    }

    public void ClosePanel() {
        statsBoardHoveredVisual.SetActive(false);
        statsBoardUIGameObject.gameObject.SetActive(false);
        statsBoardOpen = false;
    }

    public void OpenPanel() {
        statsBoardHoveredVisual.SetActive(true);
        statsBoardUIGameObject.gameObject.SetActive(true);
        statsBoardOpen = true;
    }

    public DungeonEntrance GetDungeonEntrance() {
        return dungeonEntrance;
    }

    public DungeonStatsBoardWorldUI GetDungeonStatsBoardWorldUI() {
        return dungeonStatsBoardWorldUI;
    }

    public Collider2D GetSolidCollider() {
        return solidStatsBoardCollider;
    }

    public int GetRecordedHumanoidsNumberSaved() {
        return recordedhumanoidsSaved;
    }

    public float GetRecordedDungeonTime() {
        return recordedDungeonTime;
    }

    public void LoadStatsBoardUI() {

        foreach(Item item in recordedDungeonLoot) {
            Debug.Log(item.itemType + " " + item.amount);
        }

        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);
    }
}
