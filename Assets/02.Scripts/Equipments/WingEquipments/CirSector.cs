using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CirSector : MonoBehaviour
{
    public Transform target;

    public float angleRange = 45f;
    public float distance = 5f;
    public bool isCollision = false;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    Vector3 direction;

    float dotValue = 0f;

    void Update ()
    {
        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
        direction = target.position - transform.position;
        if (direction.magnitude < distance)
        {
            if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
                isCollision = true;
            else
                isCollision = false;
        }
        else
            isCollision = false;

    }

    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange/2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange/2, distance);

    }
}