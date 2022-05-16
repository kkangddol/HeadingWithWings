using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AmmunitionBelt : Bullets
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ENEMY")
        {
            other.GetComponent<EnemyPrototype>().TakeDamage(transform, damage, knockbackSize);
            Destroy(gameObject);
        }
    }
}
