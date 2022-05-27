using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSizeChange : AbilityChange
{
    public override void ApplyChange()
    {
        playerInfo.attackSize += playerInfo.attackSize * (changeMultiplier / 100f);
    }
}
