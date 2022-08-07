using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Lightning : ArchonEffectBullet
{
    [HideInInspector]
    public float splashRange = 3.0f;
    public ParticleSystem effect = null;

    void OnEnable() {}

    public void SplashDamage(Transform target)
    {
        Instantiate(effect, target.position, Quaternion.identity);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(target.position, splashRange);
        foreach (var hitCollider in hitColliders)
        {
            EnemyTakeDamage temp = hitCollider.GetComponent<EnemyTakeDamage>();
            if (temp != null)
            {
                HitEffect(ArchonEffectPool.Instance, temp.transform.position);
                temp.TakeDamage(temp.transform, damage, knockbackSize);
            }
        }
    }
}
