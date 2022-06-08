using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSizeChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.attackSize += GameManager.playerInfo.attackSize * (changeMultiplier / 100f);
    }
}
