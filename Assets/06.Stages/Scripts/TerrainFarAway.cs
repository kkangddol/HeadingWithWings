using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFarAway : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * 0.01f);
    }
}
