using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position
{
    // xz ¡¬«•±‚¡ÿ
    public static Vector3 GetRandomInCircle(Vector3 center, float radius)
    {
        Vector3 randomPos = new Vector3(0.0f, 0.0f, 0.0f);
        randomPos.x = Random.Range(center.x - radius, center.x + radius);
        float zRange = Mathf.Sqrt(radius*radius - Mathf.Pow(randomPos.x - center.x, 2)) + center.z;
        randomPos.z = Random.Range(-zRange, zRange);

        return randomPos;
    }
}
