using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemDropRateChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.Instance.healItemDropRate += changeMultiplier;
    }
}
