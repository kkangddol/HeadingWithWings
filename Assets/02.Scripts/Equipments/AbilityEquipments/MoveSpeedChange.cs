using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedChange : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.moveSpeed += GameManager.playerInfo.moveSpeed * (changeMultiplier / 100f);
    }
}
