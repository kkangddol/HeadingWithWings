using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteEffectPool : ObjectPoolBase
{
    public static SatelliteEffectPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
