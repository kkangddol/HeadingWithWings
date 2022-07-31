using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Meteor : EffectBullet
{
    const float EXPLODE_RADIUS = 3.0f;

    [HideInInspector]
    public bool isGTAEMeteor = false;
    public SpriteRenderer sr = null;
    public GameObject GTAE = null;
    public float GTAERemainSec = 2.0f;
    public float DotDamageSec = 1.0f;

    private bool isDotDamage = false;
    public float dotDamage;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClips[0]);
    }

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

    private void GiveDamage(float damage)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, EXPLODE_RADIUS);

        foreach (Collider2D hitCollider in hitColliders)
        {
            EnemyTakeDamage temp = hitCollider.gameObject.GetComponent<EnemyTakeDamage>();
            if (temp == null) continue;

            HitEffect(MeteorEffectPool.Instance, temp.transform.position);
            temp.TakeDamage(this.transform, damage, knockbackSize);
        }
    }

    private void Explosion()
    {
        audioSource.PlayOneShot(audioClips[1]);
        GiveDamage(this.damage);
        Destroy(this.transform.parent.gameObject, DotDamageSec);
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
            GiveDamage(dotDamage);
            yield return new WaitForSeconds(DotDamageSec);
        }
        Destroy(this.transform.parent.gameObject);
    }
}
