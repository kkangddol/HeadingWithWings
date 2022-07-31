using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_PenetrateFeather : EffectBullet
{
    const string ENEMY = "ENEMY";

    private void Start() {
        Destroy(gameObject, 5f);
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
