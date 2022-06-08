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
    public float bulletSpeed;

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
        Transform targetTransform = detectEnemy.FindNearestEnemy(ENEMY);
        if(targetTransform == transform)
        {
            return;
        }
        Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * (damageMultiplier / 100);
        newBullet.knockbackSize = 3f;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    IEnumerator FireCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(playerInfo.attackDelay * (attackDelayMultiplier / 100f));
            Fire();
        }
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
