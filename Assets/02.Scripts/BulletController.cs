using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    const string ENEMY = "ENEMY";
    public int damage;    //총알공격력  //생성할때 ShootingController에서 넘겨받음
    public int knockbackSize;

    public void ActivateBullet()
    {
        GetComponent<TrailRenderer>().enabled = true;
        Invoke("DestroyBullet", 10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ENEMY)
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            //Destroy(gameObject);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        CancelInvoke();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<TrailRenderer>().enabled = false;
        BulletPool.ReturnObject(this);
    }

    //사용하지않는 update함수는 지워야 최적화된다
}
