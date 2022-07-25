using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;
    public float headShotChance = 0;


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

    void Fire()
    {
        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        ((Bullet_Sniper)newBullet).headShotChance = headShotChance;
        newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode2D.Impulse);
        isCoolDown = true;
        StartCoroutine(CoolDown());
    }

    IEnumerator FireCycle()
    {
        while(true)
        {
            yield return null;
            targetTransform = detectEnemy.FindNearestEnemy(ENEMY);

            if(targetTransform == transform) continue;

            if(Vector2.Distance(transform.position, targetTransform.position) > attackRange) continue;

            if(!isCoolDown)
            {
                Fire();
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
        switch(level)
        {
            case 1:
            {
                damageMultiplier = 0.80f;
                attackDelayMultiplier = 10.00f;
                headShotChance = 0;
                break;
            }
            case 2:
            {
                damageMultiplier = 1.20f;
                attackDelayMultiplier = 9.80f;
                headShotChance = 0;
                break;
            }
            case 3:
            {
                damageMultiplier = 1.60f;
                attackDelayMultiplier = 9.60f;
                headShotChance = 0;
                break;
            }
            case 4:
            {
                damageMultiplier = 2.00f;
                attackDelayMultiplier = 9.40f;
                headShotChance = 0;
                break;
            }
            case 5:
            {
                damageMultiplier = 2.40f;
                attackDelayMultiplier = 9.20f;
                headShotChance = 5;
                break;
            }
            default:
                break;
        }
    }
}
