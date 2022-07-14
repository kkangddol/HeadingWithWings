using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Quaternion LookAt2(Transform from, Transform to)
    {
        float angle = Mathf.Atan2(to.position.y - from.position.y, to.position.x - from.position.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
