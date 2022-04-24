using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private PlayerInfo player;
    public int damage;    //총알공격력  //생성할때 ShootingController에서 넘겨받음
    public float speed = 500.0f;
    public int knockbackSize = 5;
    private Rigidbody rb;

    void Start()
    {
        //player = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        //damage = player.damage;
        rb = GetComponent<Rigidbody>();
        //rb.AddRelativeForce(Vector3.forward * speed); //일반 instantiate 및 instantiateComponent
        rb.AddRelativeForce(transform.forward * speed); //instantiateGeneric
        Destroy(gameObject, 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ENEMY")
        {
            other.GetComponent<EnemyPrototype>().TakeDamage(transform, damage, knockbackSize);
            Destroy(gameObject);
        }
    }

    //사용하지않는 update함수는 지워야 최적화된다
}
