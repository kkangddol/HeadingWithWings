using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.damage += GameManager.playerInfo.damage * (changeMultiplier / 100f);
    }
}
