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
    private void OnTriggerEnter2D(Collider2D other)
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(gameObject);
    }
}
