using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEquipmentTest : MonoBehaviour
{
    public CircleCollider2D c2d = null;
    private Vector3 clickPos = Vector3.zero;
    private Vector3 lossyScaling = Vector3.one;
    private Vector3 Scaling = Vector3.one;

    void Start()
    {
        lossyScaling = c2d.transform.lossyScale;
        c2d.transform.SetParent(transform, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 v3 = (clickPos - this.transform.position).normalized;
            float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Scaling.x = Vector2.Distance(clickPos, this.transform.position);
            transform.localScale = Scaling;
        }
        // transform.localScale = Vector3.one;
    }
}
