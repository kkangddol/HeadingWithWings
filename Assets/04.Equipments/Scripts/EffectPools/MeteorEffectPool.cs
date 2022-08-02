using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorEffectPool : ObjectPoolBase
{
    public static MeteorEffectPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
