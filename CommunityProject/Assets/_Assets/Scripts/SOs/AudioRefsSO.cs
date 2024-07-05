using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioRefsSO : ScriptableObject
{

    [BoxGroup("UI")]
    public AudioClip itemHover;
    [BoxGroup("UI")]
    public AudioClip itemDrop;
    [BoxGroup("UI")]
    public AudioClip itemTransfer;
    [BoxGroup("UI")]
    public AudioClip itemSplit;
    [BoxGroup("UI")]
    public AudioClip itemFailedError;
    [BoxGroup("UI")]
    public AudioClip itemEquip;
    [BoxGroup("UI")]
    public AudioClip itemUnequip;
    [BoxGroup("UI")]
    public AudioClip equipmentHover;
    [BoxGroup("UI")]
    public AudioClip equipmentPress;
    [BoxGroup("UI")]
    public AudioClip buttonPress;
    [BoxGroup("UI")]
    public AudioClip pauseMenuOpen;
    [BoxGroup("UI")]
    public AudioClip pauseMenuClose;
    [BoxGroup("UI")]
    public AudioClip recipeSlotSelected;
    [BoxGroup("UI")]
    public AudioClip recipeSlotHovered;

    [BoxGroup("Player")]
    public AudioClip playerDamaged;

    [BoxGroup("Buildings")]
    public AudioClip buildingDestroyed;
    [BoxGroup("Buildings")]
    public AudioClip buildingPlaced;
    [BoxGroup("Buildings")]
    public AudioClip chestOpen;
    [BoxGroup("Buildings")]
    public AudioClip chestClosed;

    [BoxGroup("Humanoids")]
    public AudioClip[] eating;
    public AudioClip[] sleeping;


    [BoxGroup("Dungeon")]
    public AudioClip[] mobAppear;
    public AudioClip[] doorClose;
    public AudioClip[] dungeonRoomCleared;
}
