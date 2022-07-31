using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupidProjectilePool : ObjectPoolBase
{
    public static CupidProjectilePool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
