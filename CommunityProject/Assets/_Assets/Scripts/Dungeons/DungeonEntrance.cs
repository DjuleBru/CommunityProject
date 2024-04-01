using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    bool playerIsInEntranceArea;
    [SerializeField] private GameObject dungeonEntranceUI;
    [SerializeField] private GameObject enterUI;
    [SerializeField] private DungeonSO dungeonSO;

    [SerializeField] private Transform exitDungeonSpawnPoint;
    [SerializeField] private Transform chestSpawnPoint;

    private void Start() {
        dungeonEntranceUI.SetActive(false);
        enterUI.SetActive(false);
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        playerIsInEntranceArea = true;
        dungeonEntranceUI.SetActive(true);
        enterUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        playerIsInEntranceArea = false;
        dungeonEntranceUI.SetActive(false);
        enterUI.SetActive(false);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (playerIsInEntranceArea) {
            SavingSystem.Instance.SetLastDungeonEntrance(this);
            SceneLoader.Load(SceneLoader.Scene.Dungeon);
        }
    }

    public DungeonSO GetDungeonSO() {
        return dungeonSO;
    }

    public Transform GetExitDungeonSpawnPoint() {
        return exitDungeonSpawnPoint;
    }

}
