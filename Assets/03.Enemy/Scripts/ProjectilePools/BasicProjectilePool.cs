using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectilePool : ObjectPoolBase
{
    public static BasicProjectilePool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
