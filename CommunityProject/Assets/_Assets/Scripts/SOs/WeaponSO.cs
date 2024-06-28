using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public enum WeaponBodyAnimationType {
        Thrust,
        Slash,
        TwoHanded,
        CutTree,
    }

    public Sprite weaponSprite;
    public Sprite weaponSpriteUI;
    public bool weaponCanPierce;
    public float weaponKnockback;
    public float weaponDazeTime;
    public WeaponBodyAnimationType bodyAnimationType;
    public AnimatorOverrideController weaponAnimatorController;
}
