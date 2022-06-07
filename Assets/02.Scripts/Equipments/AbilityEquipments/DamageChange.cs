using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageChange : AbilityChange
{
    public override void ApplyChange()
    {
        playerInfo.damage += playerInfo.damage * (changeMultiplier / 100f);
    }
}
