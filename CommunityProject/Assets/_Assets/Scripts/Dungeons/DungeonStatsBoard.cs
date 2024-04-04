using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonStatsBoard : MonoBehaviour, IInteractable {

    private DungeonEntrance dungeonEntrance;

    [SerializeField] private List<Item> recordedDungeonLoot = new List<Item>();
    [SerializeField] private float recordedDungeonTime;

    [SerializeField] private List<Item> previousRunDungeonLoot = new List<Item>();
    [SerializeField] private float previousRunDungeonTime;

    [SerializeField] private DungeonStatsBoardUI recordedStatsBoardUI;
    [SerializeField] private DungeonStatsBoardUI previousRunStatsBoardUI;
    [SerializeField] private Transform recordedStatsBoardUIInitialPosition;
    [SerializeField] private Transform recordedStatsBoardUIPositionToReplaceStats;

    [SerializeField] private GameObject statsBoardHoveredVisual;
    [SerializeField] private GameObject statsBoardUIGameObject;

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

        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);

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
        recordedDungeonLoot = loot;
        recordedDungeonTime = time;

        recordedStatsBoardUI.SetDungeonLootUI(recordedDungeonLoot);
        recordedStatsBoardUI.SetDungeonTimeUI(recordedDungeonTime);
    }

    public void RecordLastRun(List<Item> loot, float time) {
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

        dungeonEntrance.SaveDungeon();
    }

    public void SetPlayerInTriggerArea(bool playerInTriggerArea) {
        this.playerInTriggerArea = playerInTriggerArea;
    }

    public void SetHovered(bool hovered) {
        statsBoardHoveredVisual.SetActive(hovered);
    }

    public void ClosePanel() {
        statsBoardHoveredVisual.SetActive(false);
        statsBoardUIGameObject.gameObject.SetActive(false);
        statsBoardOpen = false;
    }

    public Collider2D GetSolidCollider() {
        return solidStatsBoardCollider;
    }
}
