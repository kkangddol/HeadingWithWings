using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleBulletPool : ObjectPoolBase
{
    public static IcicleBulletPool Instance;

    private void Awake() {
        Instance = this;
    }
}
