using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedChange : AbilityChange
{
    public override void ApplyChange()
    {
        playerInfo.moveSpeed += playerInfo.moveSpeed * (changeMultiplier / 100f);
    }
}
