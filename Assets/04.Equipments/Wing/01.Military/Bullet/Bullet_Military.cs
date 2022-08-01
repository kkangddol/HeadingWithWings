using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Military : EffectBullet
{
    private void Start()
    {
        pool = MissileBulletPool.Instance;
    }

    private void OnEnable() 
    {
        Invoke("ReturnBullet", 0.43f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(ENEMY))
        {
            HitEffect(BasicEffectPool.Instance, other.transform.position, effectColor);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
        }
    }

}
