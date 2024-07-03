using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] private GameObject obstacleUI;
    [SerializeField] private List<Item> itemsToRemoveGrid;
    [SerializeField] private Rigidbody2D gridRB;

    private Inventory obstacleInventory;
    [SerializeField] private InventoryUI obstacleInventoryUI;

    [SerializeField] private Transform itemsRequiredContainer;
    [SerializeField] private Transform itemsRequiredTemplate;

    private bool obstacleRemoved;

    private void Awake() {
        obstacleUI.SetActive(false);
    }

    private void Start() {

        if(obstacleInventory == null) {
            obstacleInventory = new Inventory(true, 2, 2, 10000);
        }

        if(obstacleRemoved) {
            gameObject.SetActive(false);
        }

        obstacleInventoryUI.SetInventory(obstacleInventory);
        obstacleInventory.OnItemListChanged += ObstacleInventory_OnItemListChanged;
        RefreshRequiredItemsUI();
    }

    private void ObstacleInventory_OnItemListChanged(object sender, System.EventArgs e) {

        bool playerHasPaidObstacle = true;
        foreach (Item item in itemsToRemoveGrid) {
            if (!obstacleInventory.HasItem(item)) {
                playerHasPaidObstacle = false;
            }
        }

        if (playerHasPaidObstacle) {
            RemoveObstacle();
        }
    }

    private void RefreshRequiredItemsUI() {

        foreach (Transform child in itemsRequiredContainer) {
            if (child == itemsRequiredTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (Item item in itemsToRemoveGrid) {
            ItemSlot_Inventory itemSlot = Instantiate(itemsRequiredTemplate, itemsRequiredContainer).GetComponent<ItemSlot_Inventory>();
            itemSlot.gameObject.SetActive(true);
            itemSlot.SetItem(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<Player>() != null) {
            obstacleUI.SetActive(true);
            obstacleInventoryUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>() != null) {
            obstacleUI.SetActive(false);
            obstacleInventoryUI.gameObject.SetActive(false);
        }
    }



    public void RemoveObstacle() {
        gameObject.SetActive(false);
    }
}
