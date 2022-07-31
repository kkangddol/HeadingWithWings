using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBulletPool : ObjectPoolBase
{
    public static MissileBulletPool Instance;

    private void Awake() {
        Instance = this;
    }
}
