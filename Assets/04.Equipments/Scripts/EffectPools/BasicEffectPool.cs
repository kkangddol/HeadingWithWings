using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEffectPool : ObjectPoolBase
{
    public static BasicEffectPool Instance;

    private void Awake()
    {
        Instance = this;
        instance = Instance;
    }
}
