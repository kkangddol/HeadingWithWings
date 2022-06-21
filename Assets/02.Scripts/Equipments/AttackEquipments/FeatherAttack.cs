using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    public Bullet bullet;
    public Bullet FeatherBullet;
    public Bullet PenetrateFeatherBullet;
    public float damageMultiplier;
    public float attackDelayMultiplier;
    public float attackRange;
    public float knockbackSize;
    public float bulletSpeed;

    private Transform targetTransform;
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
        bullet = FeatherBullet;
    }

    void Fire()
    {
        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.damage = playerInfo.damage * (damageMultiplier / 100f);
        newBullet.knockbackSize = knockbackSize;
        newBullet.GetComponent<Rigidbody2D>().AddForce((targetTransform.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
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
        yield return new WaitForSeconds(playerInfo.attackDelay * (attackDelayMultiplier / 100f));
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
                bullet = FeatherBullet;
                damageMultiplier = 10;
                attackDelayMultiplier = 100;
                break;
            }
            case 2:
            {
                bullet = FeatherBullet;
                damageMultiplier = 15;
                attackDelayMultiplier = 95;
                break;
            }
            case 3:
            {
                bullet = FeatherBullet;
                damageMultiplier = 20;
                attackDelayMultiplier = 90;
                break;
            }
            case 4:
            {
                bullet = FeatherBullet;
                damageMultiplier = 25;
                attackDelayMultiplier = 85;
                break;
            }
            case 5:
            {
                bullet = PenetrateFeatherBullet;
                damageMultiplier = 30;
                attackDelayMultiplier = 80;
                break;
            }
            default:
                break;
        }
    }
}
