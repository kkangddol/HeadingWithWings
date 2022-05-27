using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAmountChange : AbilityChange
{
    public override void ApplyChange()
    {
        playerInfo.healAmount += playerInfo.healAmount * (changeMultiplier / 100f);
    }
}
