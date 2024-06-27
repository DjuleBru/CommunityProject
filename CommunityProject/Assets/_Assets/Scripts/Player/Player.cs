using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private CinemachineVirtualCamera battleCamera;

    private Inventory playerInventory;
    [SerializeField] private InventoryUI playerInventoryUI;
    [SerializeField] private GameObject playerVisual;
    public static Player Instance { get; private set; }

    public event EventHandler OnPlayerDamaged;
    public event EventHandler<IDamageable.OnIDamageableHealthChangedEventArgs> OnIDamageableHealthChanged;

    private int playerBaseHP = 100;
    private int playerHP;
    private int playerBaseDamage = 5;

    [SerializeField] private float workingSpeed;
    private bool working;

    private List<IInteractable> interactablesInTriggerArea = new List<IInteractable>();
    private IInteractable closestInteractable = null;
    private float closestInteractableDistance = Mathf.Infinity;

    private void Awake() {
        Instance = this;
        playerHP = playerBaseHP;
    }

    private void Start() {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

        playerInventory = ES3.Load("playerInventory", new Inventory(true, 3, 3, false, null));
        playerInventoryUI.SetInventory(playerInventory);
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        // Check if there is a humanoid in trigger area

        bool humanoidsInTriggerArea = false;
        if (interactablesInTriggerArea.Count > 0) {
            foreach (IInteractable interactable1 in interactablesInTriggerArea) {
                if ((interactable1 is HumanoidInteraction | interactable1 is Humanoid)) {
                    //Interactable is not a humanoid
                    humanoidsInTriggerArea = true;
                }
            }
        }

        if(!humanoidsInTriggerArea) {
            HumanoidUI.Instance.ClosePanel();
        }
    }

    private void Update() {

        if (interactablesInTriggerArea.Count > 1) {
            //There are interactables in trigger area
            HandleClosestInteractableChange();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        HandleItemTriggerEnter(collider);
        HandleInteractableTriggerEnter(collider);
        HandleResourceNodeTriggerEnter(collider);
    }
    private void OnTriggerExit2D(Collider2D collider) {
        HandleInteractableTriggerExit(collider);
        HandleResourceNodeTriggerExit(collider);
    }

    public void SetBattleCameraAsPriority() {
        battleCamera.m_Priority = 12;
    }

    public void ResetBattleCameraPriority() {
        battleCamera.m_Priority = 9;
    }

    public void TakeDamage(int damage) {
        playerHP -= damage;
        OnIDamageableHealthChanged?.Invoke(this, new IDamageable.OnIDamageableHealthChangedEventArgs {
            previousHealth = playerHP + damage,
            newHealth = playerHP
        });
    }

    public int GetPlayerBaseAttackDamage() {
        return playerBaseDamage;
    }

    public void DisablePlayerActions() {
        PlayerMovement.Instance.DisableMovement();
        PlayerAttack.Instance.DisableAttacks();
    }

    public void EnablePlayerActions() {
        PlayerMovement.Instance.EnableMovement();
        PlayerAttack.Instance.EnableAttacks();
    }

    private void HandleItemTriggerEnter(Collider2D collider) {
        if (working) return;

        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();

        if (itemWorld != null) {

            Inventory inventoryToAddItem = null;

            if (SavingSystem.Instance.GetSceneIsOverworld()) {
                inventoryToAddItem = playerInventory;
            }
            if (SavingSystem.Instance.GetSceneIsDungeon()) {
                inventoryToAddItem = DungeonManager.Instance.GetDungeonInventory();
            }

            int itemAmountPlayerInventoryCanAdd = inventoryToAddItem.AmountInventoryCanReceiveOfType(itemWorld.GetItem());
            Item itemToAdd = null;

            if (itemAmountPlayerInventoryCanAdd > 0) {
                // Player has enough space in inventory to add item

                if(itemAmountPlayerInventoryCanAdd >= itemWorld.GetItem().amount) {
                    //Player can accept all items in stack
                    itemToAdd = itemWorld.GetItem();

                } else {
                    //Player can only accept limited amount of items
                    int amountToDrop = itemWorld.GetItem().amount - itemAmountPlayerInventoryCanAdd;
                    int amountToAdd = itemWorld.GetItem().amount - amountToDrop;

                    itemToAdd = new Item { itemType = itemWorld.GetItem().itemType, amount = amountToAdd };
                    Item itemToDrop = new Item { itemType = itemWorld.GetItem().itemType, amount = amountToDrop };

                    ItemWorld.DropItem(transform.position, itemToDrop, 5f, true);
                }
            } else {
                // Player has no space in inventory to add item
                ItemWorld.DropItem(transform.position, itemWorld.GetItem(), 5f, true);
            }


            inventoryToAddItem.AddItem(itemToAdd);
            itemWorld.DestroySelf();
        }
    }

    private void HandleInteractableTriggerEnter(Collider2D collider) {
        IInteractable interactable = collider.GetComponent<IInteractable>();

        if (interactable != null) {
            // Add worker only if there are only workers in interactable range

            bool onlyHumanoidsSurrounding = true;
            if(interactablesInTriggerArea.Count > 0) {
                foreach(IInteractable interactable1 in interactablesInTriggerArea) {
                    if(!(interactable1 is HumanoidInteraction | interactable1 is Humanoid)) {
                        //Interactable is not a humanoid
                        onlyHumanoidsSurrounding = false;
                    }
                }
            }

            if(interactable is HumanoidInteraction | interactable is Humanoid) {
                if(!onlyHumanoidsSurrounding) {
                    return;
                }
            }

            interactablesInTriggerArea.Add(interactable);
            HandleClosestInteractableChange();
        }
    }

    private void HandleInteractableTriggerExit(Collider2D collider) {
        IInteractable interactable = collider.GetComponent<IInteractable>();

        if (interactable != null) {
            interactablesInTriggerArea.Remove(interactable);
            RemoveInteractionWithInteractable(interactable);
        }
        HandleClosestInteractableChange();
    }
    private void HandleResourceNodeTriggerEnter(Collider2D collider) {
        IInteractable interactable = collider.GetComponent<IInteractable>();
        if (interactable != null) {

            if (interactable is HumanoidCageInteraction) return;

            if (interactable is ResourceNodeInteraction) {
                ResourceNodeInteraction resourceNodeInteraction = (ResourceNodeInteraction)interactable;
                PlayerAttack.Instance.ChangeToolWeaponSO(resourceNodeInteraction.GetComponentInParent<ResourceNode>().GetHarvestingWeaponSO());
            }
        }
    }

    private void HandleResourceNodeTriggerExit(Collider2D collider) {
        IInteractable interactable = collider.GetComponent<IInteractable>();

        if (interactable != null) {

            if (interactable is HumanoidCageInteraction) return;
            if (interactable is ResourceNodeInteraction) {
                ResourceNodeInteraction resourceNodeInteraction = (ResourceNodeInteraction)interactable;
                PlayerAttack.Instance.RemoveToolWeaponSO();
            }
        }
    }

    private void HandleClosestInteractableChange() {

        if (interactablesInTriggerArea.Count == 0) {
            closestInteractable = null;
            closestInteractableDistance = Mathf.Infinity;
            return;
        }

        if(interactablesInTriggerArea.Count == 1) {
            closestInteractable = interactablesInTriggerArea[0];
            closestInteractable.SetPlayerInTriggerArea(true);
            closestInteractable.SetHovered(true);
            return;
        }

        // Check if the interactables in area are only humanoids
        bool onlyHumanoidsSurrounding = true;
        if (interactablesInTriggerArea.Count > 0) {
            foreach (IInteractable interactable1 in interactablesInTriggerArea) {
                if (!(interactable1 is HumanoidInteraction | interactable1 is Humanoid)) {
                    //Interactable is not a humanoid
                    onlyHumanoidsSurrounding = false;
                }
            }
        }

        // Find closest interactable
        foreach (IInteractable interactable in interactablesInTriggerArea) {

            //Skip humanoid interaction if there are other types of interactables in trigger area
            if ((interactable is HumanoidInteraction | interactable is Humanoid) && !onlyHumanoidsSurrounding) continue;

            Collider2D interactableCollider = interactable.GetSolidCollider();

            ColliderDistance2D colliderDistance2DToInteractableSolidCollider = interactableCollider.Distance(GetComponent<Collider2D>());
            float distanceToInteractableCollider = colliderDistance2DToInteractableSolidCollider.distance;

            if (distanceToInteractableCollider < 0.1f) {
                distanceToInteractableCollider = 0.1f;
            }

            if (distanceToInteractableCollider < closestInteractableDistance) {
                if (closestInteractable != null) {
                    RemoveInteractionWithInteractable(closestInteractable);
                }

                closestInteractable = interactable;
                closestInteractable.SetPlayerInTriggerArea(true);
                closestInteractable.SetHovered(true);
                closestInteractableDistance = distanceToInteractableCollider;
            }
        }

    }

    private void RemoveInteractionWithInteractable(IInteractable interactable) {
        interactable.SetPlayerInTriggerArea(false);
        interactable.SetHovered(false);

        if(interactable is ProductionBuildingVisual) {
            int productionBuildingsInTriggerArea = 0;

            foreach(IInteractable interactable1 in interactablesInTriggerArea) {
                if(interactable1 is ProductionBuildingVisual) {
                    productionBuildingsInTriggerArea++;
                }
            }

            if(productionBuildingsInTriggerArea == 0) {
                interactable.ClosePanel();
            } else {
                ProductionBuildingUI.Instance.RefreshProductionBuildingUI();
            }
        } else {
            interactable.ClosePanel();
        }
    }

    public void SavePlayer() {
        ES3.Save("playerInventory", playerInventory);
    }

    #region SET PARAMETERS

    public void SetDead() {

    }
    public void SetPlayerWorking(bool working) {
        this.working = working;
        playerVisual.SetActive(!working);
    }

    #endregion

    #region GET PARAMETERS

    public int GetHP() {
        return playerHP;
    }

    public int GetMaxHP() {
        return playerBaseHP;
    }
    public Inventory GetInventory() {
        return playerInventory;
    }

    public bool GetPlayerWorking() {
        return working;
    }

    public float GetPlayerWorkingSpeed() {
        return workingSpeed;
    }

    public List<IInteractable> GetInteractablesInTriggerArea() {
        return interactablesInTriggerArea;
    }

    public IInteractable GetClosestInteractable() {
        return closestInteractable;
    }

    public InventoryUI GetPlayerInventoryUI() {
        return playerInventoryUI;
    }

    #endregion
}
