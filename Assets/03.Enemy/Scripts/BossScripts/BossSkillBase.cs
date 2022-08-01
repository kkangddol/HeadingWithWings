using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillBase : EnemyRangeAttackBase
{
    public void StopSkill()
    {
        this.enabled = false;
    }
}
