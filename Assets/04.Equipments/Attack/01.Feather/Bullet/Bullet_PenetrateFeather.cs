using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_PenetrateFeather : EffectBullet
{
    [HideInInspector]
    public bool isPenetrate = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(ENEMY))
        {
            HitEffect(BasicEffectPool.Instance, other.transform.position, effectColor);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            if(!isPenetrate)  ReturnBullet();
        }
    }
}
