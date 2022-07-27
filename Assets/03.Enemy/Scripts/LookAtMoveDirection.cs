using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMoveDirection : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void FixedUpdate() {
        if(rb.velocity.magnitude == 0) return;
        Vector2 diff = rb.velocity;// - rb.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
    }
}
