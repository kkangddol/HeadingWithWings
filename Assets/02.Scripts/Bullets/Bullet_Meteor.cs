using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Meteor : Bullet
{
    const string TERRAIN = "TERRAIN";
    const float EXPLODE_RADIUS = 3.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TERRAIN))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, EXPLODE_RADIUS);

            foreach (var hitCollider in hitColliders)
            {
                EnemyTakeDamage enemy = hitCollider.gameObject.GetComponent<EnemyTakeDamage>();
                if (enemy == null)
                {
                    continue;
                }

                enemy.TakeDamage(transform, damage, knockbackSize);
            }
            Destroy(gameObject);
        }
    }
}
