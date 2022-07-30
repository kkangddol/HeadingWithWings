using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackBase : MonoBehaviour
{
    public void StopFire()
    {
        this.enabled = false;
    }
}
