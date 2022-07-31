using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Icicle : EffectBullet
{
    [HideInInspector]
    public float speedMultiplier = 0.0f;
    [HideInInspector]
    public float slowDuration = 0.0f;

    private void Start()
    {
        pool = IcicleBulletPool.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ENEMY))
        {
            HitEffect(BasicEffectPool.Instance, other.transform.position, effectColor);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            other.GetComponent<EnemyFreezingHandler>().SlowMove(speedMultiplier, slowDuration);
            ReturnBullet();
        }
    }
}
