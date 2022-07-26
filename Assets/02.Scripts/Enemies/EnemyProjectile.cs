using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    const string IRONWALL = "IRONWALL";
    public float damage;

    private void Start()
    {
        Invoke("DestroyProjectile", 10.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(PLAYER))
        {
            other.GetComponent<PlayerTakeDamage>().TakeDamage(damage);
            DestroyProjectile();
        }
        if(other.CompareTag(IRONWALL))
        {
            other.GetComponent<Bullet_IronWall>().TakeDamage();
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
