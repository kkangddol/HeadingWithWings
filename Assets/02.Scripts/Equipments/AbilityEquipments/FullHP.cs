using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHP : AbilityChange
{
    public override void ApplyChange()
    {
        GameManager.playerInfo.HealthPoint = GameManager.playerInfo.MaxHealthPoint;
    }
}
