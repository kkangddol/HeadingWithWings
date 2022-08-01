using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public const string PLAYER = "PLAYER";
    public const string ENEMY = "ENEMY";

    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float knockbackSize;

    [HideInInspector]
    public ObjectPoolBase pool = null;

    private void Start() 
    {
        pool = FeatherBulletPool.Instance;
    }

    private void OnEnable() 
    {
        Invoke("ReturnBullet", 5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void ReturnBullet()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pool.ReturnObject(this.gameObject);
    }
}
