using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCloudTemp : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * 0.01f);
    }
}
