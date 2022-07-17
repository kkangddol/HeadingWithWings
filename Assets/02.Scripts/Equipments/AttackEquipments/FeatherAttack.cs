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
    }

    void Fire()
    {
        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        newBullet.transform.rotation = Utilities.LookAt2(this.transform, targetTransform);
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
                bullet = FeatherBullet;
                damageMultiplier = 0.10f;
                attackDelayMultiplier = 1.00f;
                break;
            }
            case 2:
            {
                bullet = FeatherBullet;
                damageMultiplier = 0.15f;
                attackDelayMultiplier = 0.95f;
                break;
            }
            case 3:
            {
                bullet = FeatherBullet;
                damageMultiplier = 0.20f;
                attackDelayMultiplier = 0.90f;
                break;
            }
            case 4:
            {
                bullet = FeatherBullet;
                damageMultiplier = 0.25f;
                attackDelayMultiplier = 0.85f;
                break;
            }
            case 5:
            {
                bullet = PenetrateFeatherBullet;
                damageMultiplier = 0.30f;
                attackDelayMultiplier = 0.80f;
                break;
            }
            default:
                break;
        }
    }
}
