using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTakeRangeChange : AbilityChange
{
    public override void ApplyChange()
    {
        playerInfo.itemTakeRange += playerInfo.itemTakeRange * (changeMultiplier / 100f);
    }
}
