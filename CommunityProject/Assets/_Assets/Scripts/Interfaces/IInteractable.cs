using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    public void SetPlayerInTriggerArea(bool playerInTriggerArea);

    public bool GetPlayerInTriggerArea();

    public void SetHovered(bool hovered);

    public void ClosePanel();

    public Collider2D GetSolidCollider();
}
