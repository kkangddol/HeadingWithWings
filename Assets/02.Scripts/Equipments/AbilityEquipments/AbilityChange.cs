using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityChange : MonoBehaviour
{
    public float changeMultiplier;
    abstract public void ApplyChange();
}
