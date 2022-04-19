using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float damage = 3.0f;    //총알공격력
    public float speed = 500.0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * speed);
        Destroy(gameObject, 10.0f);
    }

    //사용하지않는 update함수는 지워야 최적화된다
}
