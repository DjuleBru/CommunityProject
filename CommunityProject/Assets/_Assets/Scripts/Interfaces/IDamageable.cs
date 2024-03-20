using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    public event EventHandler<OnIDamageableHealthChangedEventArgs> OnIDamageableHealthChanged;
    public class OnIDamageableHealthChangedEventArgs {
        public float previousHealth;
        public float newHealth;
    }

    public void TakeDamage(int damage);

    public void SetDead();

    public int GetHP();

    public int GetMaxHP();
}
