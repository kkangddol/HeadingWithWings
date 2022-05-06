using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;    //총알공격력  //생성할때 ShootingController에서 넘겨받음
    public float speed = 1.0f;
    public int knockbackSize = 5;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    public void ActivateBullet()
    {
        Invoke("DestroyBullet", 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ENEMY")
        {
            other.GetComponent<EnemyPrototype>().TakeDamage(transform, damage, knockbackSize);
            //Destroy(gameObject);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        
        BulletPool.ReturnObject(this);

    }

    //사용하지않는 update함수는 지워야 최적화된다
}
