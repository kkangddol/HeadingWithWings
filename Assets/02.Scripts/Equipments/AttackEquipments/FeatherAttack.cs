using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherAttack : Equipment
{
    PlayerInfo playerInfo;
    DetectEnemy detectEnemy;
    const string ENEMY = "ENEMY";
    public Bullets bullet;
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
    }

    void Fire()
    {
        Transform targetTransform = detectEnemy.FindNearestEnemy(ENEMY);
        if(targetTransform == transform)
        {
            return;
        }
        Bullets newBullet = Instantiate(bullet,transform.position,transform.rotation);
        newBullet.transform.LookAt(targetTransform);
        newBullet.damage = playerInfo.damage * (damageMultiplier / 100f);
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
                damageMultiplier = 100;
                attackDelayMultiplier = 100;
                break;
            }
            case 2:
            {
                damageMultiplier = 105;
                attackDelayMultiplier = 95;
                break;
            }
            case 3:
            {
                damageMultiplier = 110;
                attackDelayMultiplier = 90;
                break;
            }
            case 4:
            {
                damageMultiplier = 115;
                attackDelayMultiplier = 85;
                break;
            }
            case 5:
            {
                damageMultiplier = 120;
                attackDelayMultiplier = 80;
                break;
            }
            default:
                break;
        }
    }
}
