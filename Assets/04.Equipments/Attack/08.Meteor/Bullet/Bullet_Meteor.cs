using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Meteor : EffectBullet
{
    const float EXPLODE_RADIUS = 3.0f;

    [HideInInspector]
    public bool isGTAEMeteor = false;
    public GameObject GTAE = null;
    public float GTAERemainSec = 2.0f;
    public float DotDamageSec = 1.0f;

    private bool isDotDamage = false;
    public float dotDamage;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        pool = MeteorBulletPool.Instance;
    }

    private void OnEnable()
    {
        if(audioClips.Length != 0)  audioSource.PlayOneShot(audioClips[0]);
    }

    private void IsGroundEvent()
    {
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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.parent.position, EXPLODE_RADIUS);

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
        ReturnMeteor(this.transform.parent.gameObject, DotDamageSec);
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
        ReturnMeteor(this.transform.parent.gameObject);
    }


    public void ReturnMeteor(GameObject obj)
    {
        pool.ReturnObject(obj);
    }
    public void ReturnMeteor(GameObject obj, float sec)
    {
        pool.ReturnObject(obj, sec);
    }
}
