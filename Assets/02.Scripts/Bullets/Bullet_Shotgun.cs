using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun : Bullets
{
    const string ENEMY = "ENEMY";

    private void Start() {
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ENEMY)
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            Destroy(gameObject);
        }
    }
}
