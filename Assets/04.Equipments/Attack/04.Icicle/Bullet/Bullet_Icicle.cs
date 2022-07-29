using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Icicle : Bullet
{
    const string ENEMY = "ENEMY";
    [HideInInspector]
    public float speedMultiplier = 0.0f;
    [HideInInspector]
    public float slowDuration = 0.0f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ENEMY))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            other.GetComponent<EnemyFreezingHandler>().SlowMove(speedMultiplier, slowDuration);
            Destroy(gameObject);
        }
    }
}
