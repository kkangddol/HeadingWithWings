using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    protected const string PLAYER = "PLAYER";
    protected const string IRONWALL = "IRONWALL";
    public float damage;

    protected ObjectPoolBase pool = null;

    private void Start()
    {
        pool = BasicProjectilePool.Instance;
    }

    private void OnEnable()
    {
        Invoke("ReturnProjectile", 10.0f);
    }
    private void OnDisable() {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(PLAYER))
        {
            other.GetComponent<PlayerTakeDamage>().TakeDamage(damage);
            ReturnProjectile();
        }
        if(other.CompareTag(IRONWALL))
        {
            other.GetComponent<Bullet_IronWall>().TakeDamage();
            ReturnProjectile();
        }
    }

    private void ReturnProjectile()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pool.ReturnObject(this.gameObject);
    }
}
