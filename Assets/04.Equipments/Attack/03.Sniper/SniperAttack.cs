using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 10300;
    const float TOTALTIME = 1.6f; // for sniping
    public LineRenderer laser = null;
    public GameObject scope = null;
    public Bullet bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public float headShotChance = 0;
    public float headShotDamageMultiplier;


    private Transform targetTransform;
    private bool isCoolDown = false;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag(PLAYER).GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Sniping()
    {
        isCoolDown = true;
        float snipingTime = TOTALTIME;
        float scaling = 0f;
        Coroutine co = null;
        laser.gameObject.SetActive(true);
        scope.SetActive(true);

        while (snipingTime > 0)
        {
            if (snipingTime <= 1f && co == null)
            {
                co = StartCoroutine(LaserBlink());
            }

            Vector3 dir = targetTransform.position - this.transform.position;
            laser.SetPosition(1, dir * 1.25f);
            scope.transform.position = targetTransform.position;

            scaling += 0.1f;
            snipingTime -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LaserBlink()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            laser.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            laser.gameObject.SetActive(false);
        }
        scope.SetActive(false);
        Fire();
    }

    void Fire()
    {
        Bullet bullet = GetBullet(SniperBulletPool.Instance);
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        bullet.transform.rotation = Utilities.LookAt2(this.transform, targetTransform);
        bullet.damage = playerInfo.damage * damageMultiplier;
        bullet.knockbackSize = knockbackSize;
        ((Bullet_Sniper)bullet).headShotChance = headShotChance;
        ((Bullet_Sniper)bullet).headShotChance = headShotDamageMultiplier;
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(CoolDown());
        audioSource.PlayOneShot(audioClips[0]);
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;
            if (!isCoolDown)
            {
                targetTransform = detectEnemy.FindStrongestEnemy(ENEMY);
                if (targetTransform == transform) continue;
                if (Vector2.Distance(transform.position, targetTransform.position) > attackRange) continue;
                StartCoroutine(Sniping());
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier - 2);
        audioSource.PlayOneShot(audioClips[1]);
        yield return new WaitForSeconds(2);
        isCoolDown = false;
    }

    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;
        damageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].damageMultiplier;
        attackDelayMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.AttackEquipDict[equipID + this.level].knockBackSize;
        headShotChance = GameManager.Data.AttackEquipDict[equipID + this.level].headShotChance;
        headShotDamageMultiplier = GameManager.Data.AttackEquipDict[equipID + this.level].headShotDamageMultiplier;
    }
}
