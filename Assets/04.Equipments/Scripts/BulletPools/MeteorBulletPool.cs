using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBulletPool : ObjectPoolBase
{
    public static MeteorBulletPool Instance;

    private void Awake() {
        Instance = this;
    }
}
