using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteAttack : MonoBehaviour
{
    private GameObject go1 = null;
    private GameObject go2 = null;

    private bool isGo1 = false;
    private bool isGo2 = false;
    // Start is called before the first frame update
    void Start()
    {
        go1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go1.transform.SetParent(this.transform);
        go1.transform.localPosition = Vector3.right * 2f;
        this.transform.rotation = Quaternion.AngleAxis(-45, Vector3.forward);

        go2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go2.transform.SetParent(this.transform);
        go2.transform.localPosition = Vector3.left * 2f;
        this.transform.rotation = Quaternion.AngleAxis(45, Vector3.forward);
    }

    private void Fire()
    {
        float angle = 360 * 0.5f;
        for (int i = 1; i <= 2; i++)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.right * 2f;
            this.transform.rotation = Quaternion.AngleAxis(angle*i, Vector3.forward);
        }
    }

    private void Update()
    {
        this.transform.RotateAround(this.transform.position, this.transform.forward, Time.deltaTime * 1000f);
    }
}
