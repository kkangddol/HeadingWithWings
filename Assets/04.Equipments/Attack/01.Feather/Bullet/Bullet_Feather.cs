using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Feather : EffectBullet
{
    const string ENEMY = "ENEMY";

    private void Awake() {
        pool = FeatherBulletPool.Instance;
        Invoke("ReturnBullet", 5);
    }

    private void OnEnable()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(ENEMY))
        {
            HitEffect(BasicEffectPool.Instance, other.transform.position, effectColor);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            ReturnBullet();
        }
    }
}
