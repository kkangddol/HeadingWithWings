using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBulletPool : ObjectPoolBase
{
    public static ShotGunBulletPool Instance;

    private void Awake() {
        Instance = this;
    }
}
