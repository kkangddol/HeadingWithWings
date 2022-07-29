using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Sniper : Bullet
{
    const string ENEMY = "ENEMY";
    [HideInInspector]
    public float headShotChance = 0;
    public float headShotDamageMultiplier = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(ENEMY))
        {
            float randomNumber = Random.Range(0f,100f);
            if(randomNumber < headShotChance)
            {
                damage *= headShotDamageMultiplier;
            }
            HitEffect(other.transform.position);
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            Destroy(gameObject);
        }
    }
}
