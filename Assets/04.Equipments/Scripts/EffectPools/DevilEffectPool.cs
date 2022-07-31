using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilEffectPool : ObjectPoolBase
{
    public static DevilEffectPool Instance;

    private void Awake()
    {
        Instance = this;
        instance = Instance;
    }
}
