using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public enum WeaponBodyAnimationType {
        Thrust,
        Slash,
    }

    public Sprite weaponSprite;
    public int weaponDamage;
    public float weaponAttackRate;
    public bool weaponCanPierce;
    public float weaponKnockback;
    public float weaponDazeTime;
    public WeaponBodyAnimationType bodyAnimationType;
    public AnimatorOverrideController weaponAnimatorController;
}
