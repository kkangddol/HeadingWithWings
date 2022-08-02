using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Satellite : EffectBullet
{
    void OnEnable() {}

    private void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ENEMY))
        {
            HitEffect(SatelliteEffectPool.Instance, other.transform.position);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
        }
    }
}
