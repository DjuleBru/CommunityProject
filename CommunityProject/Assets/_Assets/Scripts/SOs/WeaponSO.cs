using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public enum WeaponBodyAnimationType {
        Thrust,
        Slash,
        CutTree,
    }

    public Sprite weaponSprite;
    public Sprite weaponSpriteUI;
    public int weaponDamage;
    public float weaponAttackRate;
    public bool weaponCanPierce;
    public float weaponKnockback;
    public float weaponDazeTime;
    public WeaponBodyAnimationType bodyAnimationType;
    public AnimatorOverrideController weaponAnimatorController;
}
