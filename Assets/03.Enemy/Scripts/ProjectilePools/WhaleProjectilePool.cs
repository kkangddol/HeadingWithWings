using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleProjectilePool : ObjectPoolBase
{
    public static WhaleProjectilePool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
