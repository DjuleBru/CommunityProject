using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBar : HPBar
{
    protected override void Start() {
        iDamageable = Player.Instance;
        base.Start();
    }
}
