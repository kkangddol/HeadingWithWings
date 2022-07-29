using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack_renew : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const int equipID = 300;
    const string ENEMY = "ENEMY";
    public LineRenderer laser = null;
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


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        detectEnemy = GetComponent<DetectEnemy>();
    }

    IEnumerator Sniping()
    {
        isCoolDown = true;
        float snipingTime = 1.6f;
        float scaling = 0f;
        Coroutine co = null;
        laser.gameObject.SetActive(true);

        while (snipingTime > 0)
        {
            if (snipingTime <= 1f && co == null)
            {
                co = StartCoroutine(LaserBlink());
            }

            Vector3 dir = targetTransform.position - this.transform.position;
            laser.SetPosition(1, dir * scaling);

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
            if (i == 2) Fire();
        }
    }

    void Fire()
    {
        Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        ((Bullet_Sniper)newBullet).headShotChance = headShotChance;
        ((Bullet_Sniper)newBullet).headShotChance = headShotDamageMultiplier;
        newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(CoolDown());
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;
            targetTransform = detectEnemy.FindNearestEnemy(ENEMY);

            if (targetTransform == transform) continue;

            if (Vector2.Distance(transform.position, targetTransform.position) > attackRange) continue;

            if (!isCoolDown)
            {
                StartCoroutine(Sniping());
            }
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(playerInfo.attackDelay * attackDelayMultiplier);
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
