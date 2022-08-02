using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBulletPool : ObjectPoolBase
{
    public static SniperBulletPool Instance;

    private void Awake() {
        Instance = this;
    }
}
