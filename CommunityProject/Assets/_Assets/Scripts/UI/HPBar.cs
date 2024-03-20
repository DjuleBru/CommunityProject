using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    protected IDamageable iDamageable;

    [SerializeField] protected GameObject unitHPBarGameObject;
    [SerializeField] protected Image HPBarImage;
    [SerializeField] protected Image HPBarDamageImage;

    [SerializeField] protected bool HPBarGetHidden;

    [SerializeField] protected float damageBarUpdateRate = 2f;
    [SerializeField] protected float delayToUpdateDamageBar = .2f;
    [SerializeField] protected float updateHPBarDuration = 1f;
    [SerializeField] protected float hideHPBarDuration = 1f;
    protected float damageBarUpdateTimer;
    protected bool updateHPBarFinished;
    protected bool unitHPBarIsActive;
    protected float hideHPBarTimer;

    protected virtual void Awake() {
        iDamageable = GetComponentInParent<IDamageable>();

        hideHPBarTimer = hideHPBarDuration;

        if(HPBarGetHidden) {
            unitHPBarGameObject.SetActive(false);
        }
    }

    protected virtual void Start() {
        iDamageable.OnIDamageableHealthChanged += IDamageable_OnIDamageableHealthChanged;
    }

    protected void IDamageable_OnIDamageableHealthChanged(object sender, IDamageable.OnIDamageableHealthChangedEventArgs e) {
        UpdateHPBar(e.previousHealth, e.newHealth);
    }

    protected void Update() {
        if (!updateHPBarFinished) {
            damageBarUpdateTimer -= Time.deltaTime;

            if (damageBarUpdateTimer < 0) {
                if(HPBarDamageImage.fillAmount <= HPBarImage.fillAmount) {
                    updateHPBarFinished = true;
                    return;
                }

                if (HPBarDamageImage.fillAmount > (float)iDamageable.GetHP() / (float)iDamageable.GetMaxHP()) {
                    HPBarDamageImage.fillAmount = HPBarDamageImage.fillAmount - damageBarUpdateRate * Time.deltaTime;
                }
            }

        }
        else {
            if (!unitHPBarIsActive) return;

            hideHPBarTimer -= Time.deltaTime;
            if (hideHPBarTimer < 0) {
                if (HPBarGetHidden) {
                    unitHPBarGameObject.SetActive(false);
                }
                unitHPBarIsActive = false;
            }
        }
    }

    protected void UpdateHPBar(float initialUnitHP, float newUnitHP) {
        if (newUnitHP<=0) {
            if (HPBarGetHidden) {
                unitHPBarGameObject.SetActive(false);
            }
        return;
        }

        unitHPBarGameObject.SetActive(true);
        unitHPBarIsActive = true;
        updateHPBarFinished = false;
        hideHPBarTimer = hideHPBarDuration;

        damageBarUpdateTimer = delayToUpdateDamageBar;

        unitHPBarGameObject.SetActive(true);
        unitHPBarIsActive = true;
        HPBarImage.fillAmount = newUnitHP / iDamageable.GetMaxHP();

    }
}
