using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ground Target Area of Effect
public class GTAEMeteor_old : Bullet
{
    const string ENEMY = "ENEMY";
    const string TERRAIN = "TERRAIN";
    const float EXPLODE_RADIUS = 3.0f;
    private Vector3 GTAEPos;
    private WaitForSeconds DotInterval = new WaitForSeconds(0.5f);
    private bool isGTAE = false;

    IEnumerator GTAE()
    {
        isGTAE = true;
        StartCoroutine(DotDamage());
        yield return new WaitForSeconds(2.0f);
        isGTAE = false;
        Destroy(this);
    }
    IEnumerator DotDamage()
    {
        while(isGTAE)
        {
            Collider[] hitColliders = Physics.OverlapSphere(GTAEPos, EXPLODE_RADIUS);

            foreach (var hitCollider in hitColliders)
            {
                EnemyTakeDamage enemy = hitCollider.gameObject.GetComponent<EnemyTakeDamage>();
                if (enemy == null)
                {
                    continue;
                }

                enemy.TakeDamage(enemy.transform, damage, knockbackSize);
            }
            
            yield return DotInterval;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

            if(isGTAE == true)
                return;

            GTAEPos = other.transform.position;
            GTAEPos.y = 0.0f;
            StartCoroutine(GTAE());
        }
    }
}
