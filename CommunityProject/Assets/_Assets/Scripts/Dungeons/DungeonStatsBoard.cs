using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStatsBoard : MonoBehaviour, IInteractable {

    private DungeonEntrance dungeonEntrance;

    [SerializeField] private List<Item> recordedDungeonLoot;
    [SerializeField] private float recordedDungeonTime;

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

    public void RecordDungeon(List<Item> loot, float time) {
        Debug.Log("recording dungeon loot " + time);
        recordedDungeonLoot = loot;
        recordedDungeonTime = time;

        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);
    }

    public void RecordLastRun(List<Item> loot, float time) {
        Debug.Log("recording last run " + time);
        previousRunStatsBoardUI.gameObject.SetActive(true);

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

        //dungeonEntrance.SaveDungeon();
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

    public void LoadStatsBoardUI() {
        Debug.Log("load stats board ui " + recordedDungeonLoot.Count);
        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);
    }
}
