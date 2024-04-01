using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Transferable : InventoryUI {


    [SerializeField] private TransferItemsUI transferItemsUI;

    private void Awake() {
        transferItemsUI.gameObject.SetActive(false);
    }

    public void OpenTransferItemsPanelGameObject() {
        transferItemsUI.gameObject.SetActive(true);
    }

    public void CloseTransferItemsPanelGameObject() {
        transferItemsUI.ResetItemToTransfer();
        transferItemsUI.gameObject.SetActive(false);
    }
}
