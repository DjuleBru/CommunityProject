using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidCarryAssignItemButton : MonoBehaviour
{


    private bool selectingItem;

    private void Update() {
        if (selectingItem) {
            if (Input.GetMouseButtonDown(1)) {
                selectingItem = false;
                SelectItemUI.Instance.ClosePanel();
            }
        }
    }

    private void SelectItemUI_OnItemSelected(object sender, SelectItemUI.OnItemSelectedEventArgs e) {
        GetComponentInParent<HumanoidTemplateUI>().SetItemToCarry(e.itemSelected);
        SelectItemUI.Instance.ClosePanel();
        SelectItemUI.Instance.OnItemSelected -= SelectItemUI_OnItemSelected;
    }

    public void StartSettingItem() {
        selectingItem = true;

        GetComponentInParent<HumanoidTemplateUI>().ToggleAutoAssign(false);
        SelectItemUI.Instance.OpenPanel();
        SelectItemUI.Instance.OnItemSelected += SelectItemUI_OnItemSelected;
        GetComponentInParent<HumanoidTemplateUI>().RefreshHumanoidTemplateUI();
    }
}
