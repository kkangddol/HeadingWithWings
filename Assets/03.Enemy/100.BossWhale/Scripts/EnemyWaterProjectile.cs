using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterProjectile : EnemyProjectile
{
    private void Start()
    {
        pool = WhaleProjectilePool.Instance;
    }
}
