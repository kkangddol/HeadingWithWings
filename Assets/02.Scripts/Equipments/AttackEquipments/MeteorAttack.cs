using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    const int MAXMETEOR = 6;
    public Bullet bullet;
    public Bullet meteorBullet;
    // Ground Target Area of Effect
    public Bullet GTAEMeteorBullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;

    public int meteorCount;

    private float meteorHeight = 10.0f;
    private float meteorRangeRadius = 3.0f;
    private WaitForSeconds meteorInterval = new WaitForSeconds(0.25f);
    private Transform[] targetTransform;
    private bool isCoolDown = false;


    private void Start()
    {
        Initialize();
        StartCoroutine(FireCycle());
    }

    void Initialize()
    {
        playerInfo = GameManager.playerInfo;
        detectEnemy = GetComponent<DetectEnemy>();
        bullet = meteorBullet;
    }

    IEnumerator Fire()
    {
        isCoolDown = true;

        for (int i = 0; i < meteorCount; i++)
        {
            Bullet newBullet = Instantiate(bullet, 
            Position.GetRandomInCircle(transform.position, meteorRangeRadius) + Vector3.up * meteorHeight,
            Quaternion.identity);
            newBullet.transform.LookAt(targetTransform[i].position);
            newBullet.damage = playerInfo.damage * damageMultiplier;
            newBullet.knockbackSize = knockbackSize;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode.Impulse);

            yield return meteorInterval;
        }
        
        StartCoroutine(CoolDown());
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;
            targetTransform = detectEnemy.FindNearestEnemies(ENEMY, meteorCount, attackRange);

            if (!isCoolDown)
            {
                StartCoroutine(Fire());
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

        //220527 하드코딩이므로 DataManager 이용할 것.
        switch (level)
        {
            case 1:
                {
                    bullet = meteorBullet;
                    damageMultiplier = 0.35f;
                    attackDelayMultiplier = 5.00f;
                    meteorCount = 2;
                    break;
                }
            case 2:
                {
                    bullet = meteorBullet;
                    damageMultiplier = 0.40f;
                    attackDelayMultiplier = 4.90f;
                    meteorCount = 3;
                    break;
                }
            case 3:
                {
                    bullet = meteorBullet;
                    damageMultiplier = 0.45f;
                    attackDelayMultiplier = 4.80f;
                    meteorCount = 4;
                    break;
                }
            case 4:
                {
                    bullet = meteorBullet;
                    damageMultiplier = 0.50f;
                    attackDelayMultiplier = 4.70f;
                    meteorCount = 5;
                    break;
                }
            case 5:
                {
                    bullet = GTAEMeteorBullet;
                    damageMultiplier = 0.55f;
                    attackDelayMultiplier = 4.60f;
                    meteorCount = MAXMETEOR;
                    break;
                }
            default:
                break;
        }
    }
}
