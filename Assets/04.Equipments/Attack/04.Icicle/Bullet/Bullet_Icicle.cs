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
            
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 2f);
            foreach(var target in targets)
            {
                if(target.CompareTag("ENEMY"))
                {
                    HitEffect(BasicEffectPool.Instance, other.transform.position, effectColor);
                    target.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
                    target.GetComponent<EnemyFreezingHandler>().SlowMove(speedMultiplier, slowDuration);
                }
            }
            ReturnBullet();            
        }
    }
}
