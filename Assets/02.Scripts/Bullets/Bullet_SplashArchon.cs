using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_SplashArchon : Bullet
{
    const string ENEMY = "ENEMY";
    [HideInInspector]
    public float splashRange = 3.0f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY))
        {
            Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, splashRange);
            foreach (var hitCollider in hitColliders)
            {
                EnemyTakeDamage temp = hitCollider.GetComponent<EnemyTakeDamage>();
                if(temp != null)
                {
                    temp.TakeDamage(temp.transform, damage, knockbackSize);
                }
            }
            Destroy(gameObject);
        }
    }
}
