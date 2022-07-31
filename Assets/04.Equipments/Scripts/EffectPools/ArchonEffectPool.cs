using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchonEffectPool : ObjectPoolBase
{
    public static ArchonEffectPool Instance;

    private void Awake() {
        Instance = this;
        instance = Instance;
    }
}
