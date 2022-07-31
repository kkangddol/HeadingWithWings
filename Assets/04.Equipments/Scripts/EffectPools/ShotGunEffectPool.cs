using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunEffectPool : ObjectPoolBase
{
    public static ShotGunEffectPool Instance;

    private void Awake()
    {
        Instance = this;
        instance = Instance;
    }
}
