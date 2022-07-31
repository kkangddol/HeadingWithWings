using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherBulletPool : ObjectPoolBase
{
    public static FeatherBulletPool Instance;

    private void Awake() {
        Instance = this;
    }
}
