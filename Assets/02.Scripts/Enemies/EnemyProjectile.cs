using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    public float damage;

    private void Start()
    {
        Invoke("DestroyProjectile", 10.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == PLAYER)
        {
            other.GetComponent<PlayerTakeDamage>().TakeDamage(damage);
            //Destroy(gameObject);
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        CancelInvoke();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject);
    }
}
