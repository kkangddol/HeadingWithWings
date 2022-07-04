using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Icicle : Bullet
{
    const string ENEMY = "ENEMY";
    [HideInInspector]
    public float speedMultiplier = 0;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            other.GetComponent<EnemyMovement>().SlowMove(speedMultiplier);
            Destroy(gameObject);
        }
    }
}
