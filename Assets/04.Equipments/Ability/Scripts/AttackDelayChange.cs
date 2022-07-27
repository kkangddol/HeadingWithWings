using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDelayChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.attackDelay += GameManager.playerInfo.attackDelay * (changeMultiplier / 100f);
    }
}
