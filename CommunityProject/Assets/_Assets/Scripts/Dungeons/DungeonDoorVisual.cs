using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonDoorVisual : MonoBehaviour
{
    private Animator animator;
    private TilemapCollider2D tilemapCollider;

    private bool doorOpening;
    private bool doorClosing;

    [SerializeField] private bool isShadow;

    private void Awake() {
        animator = GetComponent<Animator>();
        tilemapCollider = GetComponent<TilemapCollider2D>();   
    }

    private void Update() {
        if(doorOpening) {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .95) {
                doorOpening = false;
                if (!isShadow) {
                    tilemapCollider.enabled = false;
                }
            };
        }
        if(doorClosing) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoor") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .95) {
                doorClosing = false;
                if (!isShadow) {
                    tilemapCollider.enabled = true;
                }
            };
        }
    }

    public void OpenDoor() {
        animator.SetTrigger("OpenDoor");
        doorOpening = true;
    }

    public void CloseDoor() {
        animator.SetTrigger("CloseDoor");
        doorClosing = true;
    }
}
