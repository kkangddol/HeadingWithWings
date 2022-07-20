using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelayChange : AbilityChange
{
    public override void ApplyChange()
    {
        playerInfo.attackDelay += playerInfo.attackDelay * (changeMultiplier / 100f);
    }
}
