using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAmountChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.healAmount += GameManager.playerInfo.healAmount * (changeMultiplier / 100f);
    }
}
