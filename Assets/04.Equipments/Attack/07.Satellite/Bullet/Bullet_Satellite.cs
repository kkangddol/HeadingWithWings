using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Satellite : Bullet
{
    const string ENEMY = "ENEMY";

    private void Update()
    {
        this.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ENEMY))
        {
            HitEffect(other.transform.position);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
        }
    }
}
