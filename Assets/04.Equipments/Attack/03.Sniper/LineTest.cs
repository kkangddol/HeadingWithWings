using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    private LineRenderer lr = null;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.SetPosition(0, GameObject.Find("PlayerController").transform.position);
        Vector3 dir = GameObject.Find("01 Enemy_Crow").transform.position - GameObject.Find("PlayerController").transform.position;
        lr.SetPosition(1, GameObject.Find("PlayerController").transform.position + dir * 2f);
    }
}
