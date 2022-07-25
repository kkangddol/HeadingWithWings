using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Quaternion LookAt2(Transform from, Transform to)
    {
        float angle = GetAngle(from, to);
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public static Quaternion LookAt2(Transform from, Vector3 to)
    {
        float angle = GetAngle(from, to);
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static float GetAngle(Transform from, Transform to)
    {
        return Mathf.Atan2(to.position.y - from.position.y, to.position.x - from.position.x) * Mathf.Rad2Deg;
    }
    public static float GetAngle(Transform from, Vector3 to)
    {
        return Mathf.Atan2(to.y - from.position.y, to.x - from.position.x) * Mathf.Rad2Deg;
    }
}
