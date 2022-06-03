using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float knockbackSize;
}
