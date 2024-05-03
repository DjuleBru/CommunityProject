using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemUI : MonoBehaviour
{
    public static SelectItemUI Instance { get; private set; }

    [SerializeField] private GameObject selectItemUIPanel;

    [SerializeField] private Transform itemContainer;
    [SerializeField] private Transform itemTemplate;

    public event EventHandler<OnItemSelectedEventArgs> OnItemSelected;
    public class OnItemSelectedEventArgs {
        public Item itemSelected;
    }

    private void Awake() {
        Instance = this;
        selectItemUIPanel.SetActive(false);
        itemTemplate.gameObject.SetActive(false);
    }


    private void RefreshItemsUI() {
        foreach(Transform child in itemContainer) {
            if (child == itemTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(ItemSO itemSO in ItemAssets.Instance.GetItemSOList()) {
            Transform itemTemplateTransform = Instantiate(itemTemplate, itemContainer);
            Item item = new Item { itemType = itemSO.itemType, amount = 0 };

            itemTemplateTransform.gameObject.SetActive(true);
            itemTemplateTransform.GetComponent<SelectItemButton>().SetItem(item);
        }
    }

    public void SelectItem(Item item) {
        OnItemSelected?.Invoke(this, new OnItemSelectedEventArgs {
            itemSelected = item
        });
    }

    public void OpenPanel() {
        selectItemUIPanel.SetActive(true);
        RefreshItemsUI();
    }

    public void ClosePanel() {
        selectItemUIPanel.SetActive(false);
    }
}
