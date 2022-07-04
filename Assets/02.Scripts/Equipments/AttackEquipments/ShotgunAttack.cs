using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAttack : Equipment
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
    public int pelletCount;
    public float maxSpread;

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

    public void Fire()
    {
        Vector2 enemyDirection = (targetTransform.position - transform.position).normalized;
        for(int i = 0; i < pelletCount; i++)
        {
            Bullet newPellet = Instantiate(bullet,transform.position,transform.rotation);
            newPellet.damage = playerInfo.damage * (damageMultiplier / 100f);
            newPellet.knockbackSize = this.knockbackSize;   //스크립터블 오브젝트로 처리할 예정
            // newPellet.transform.LookAt(targetTransform);
            Vector2 pelletDirection = enemyDirection + new Vector2(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread));
            newPellet.GetComponent<Rigidbody2D>().AddForce(pelletDirection * bulletSpeed, ForceMode2D.Impulse);
        }
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
                damageMultiplier = 3;
                attackDelayMultiplier = 300;
                pelletCount = 4;
                break;
            }
            case 2:
            {
                damageMultiplier = 4;
                attackDelayMultiplier = 290;
                pelletCount = 4;
                break;
            }
            case 3:
            {
                damageMultiplier = 5;
                attackDelayMultiplier = 280;
                pelletCount = 4;
                break;
            }
            case 4:
            {
                damageMultiplier = 6;
                attackDelayMultiplier = 270;
                pelletCount = 4;
                break;
            }
            case 5:
            {
                damageMultiplier = 6;
                attackDelayMultiplier = 270;
                pelletCount = 8;
                break;
            }
            default:
                break;
        }
    }
}
