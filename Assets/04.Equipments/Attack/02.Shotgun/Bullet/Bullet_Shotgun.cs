using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun : EffectBullet
{
    const string ENEMY = "ENEMY";

    private void Start() {
        Destroy(gameObject, 0.5f);
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
