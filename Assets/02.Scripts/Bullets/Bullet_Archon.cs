using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Archon : Bullet
{
    [HideInInspector]
    public float splashRange = 3.0f;

    public void SplashDamage(Transform target)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(target.position, splashRange);
        foreach (var hitCollider in hitColliders)
        {
            EnemyTakeDamage temp = hitCollider.GetComponent<EnemyTakeDamage>();
            if (temp != null)
            {
                temp.TakeDamage(temp.transform, damage, knockbackSize);
            }
        }
    }
}
