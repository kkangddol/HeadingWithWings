using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Shotgun : Bullets
{
    const string ENEMY = "ENEMY";
    void Start()
    {
        knockbackSize = 10;
        damage = GameManager.Data.WingDict[5].damagePerLevels[0];
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
