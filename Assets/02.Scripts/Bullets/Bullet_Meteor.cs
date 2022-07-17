using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Meteor : Bullet
{
    const float EXPLODE_RADIUS = 3.0f;

    [HideInInspector]
    public bool isGTAEMeteor = false;
    public SpriteRenderer sr = null;
    public GameObject GTAE = null;
    public float GTAERemainSec = 2.0f;
    public float DotDamageSec = 1.0f;

    private bool isDotDamage = false;

    private void IsGroundEvent()
    {
        sr.enabled = false;

        if(isGTAEMeteor)
        {
            GTAE.SetActive(true);
            StartCoroutine(GTAEExplosion());
        }
        else
        {
            Explosion();
        }
    }

    private void GiveDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, EXPLODE_RADIUS);

        foreach (Collider2D hitCollider in hitColliders)
        {
            EnemyTakeDamage temp = hitCollider.gameObject.GetComponent<EnemyTakeDamage>();
            if (temp == null) continue;

            temp.TakeDamage(this.transform, damage, knockbackSize);
        }
    }

    private void Explosion()
    {
        GiveDamage();
        Destroy(this.transform.parent.gameObject);
    }

    IEnumerator GTAEExplosion()
    {
        isDotDamage = true;
        StartCoroutine(DotDamage());
        yield return new WaitForSeconds(GTAERemainSec);
        isDotDamage = false;
    }

    IEnumerator DotDamage()
    {
        while(isDotDamage)
        {
            GiveDamage();
            yield return new WaitForSeconds(DotDamageSec);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
