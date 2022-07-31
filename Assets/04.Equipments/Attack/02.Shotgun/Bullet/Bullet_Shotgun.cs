using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun : EffectBullet
{
    private void Start() {
        pool = ShotGunBulletPool.Instance;
    }

    private void OnEnable()
    {
        Invoke("ReturnBullet", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(ENEMY))
        {
            HitEffect(ShotGunEffectPool.Instance, other.transform.position);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
        }
    }
}
