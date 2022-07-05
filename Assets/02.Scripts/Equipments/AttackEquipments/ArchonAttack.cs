using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchonAttack : Equipment
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
    public float splashRange;

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
        Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.damage = playerInfo.damage * damageMultiplier;
        newBullet.knockbackSize = knockbackSize;
        ((Bullet_Archon)newBullet).splashRange = splashRange;
        newBullet.GetComponent<Rigidbody2D>().AddForce((targetTransform.position - transform.position).normalized * bulletSpeed, ForceMode2D.Impulse);
        isCoolDown = true;
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
        switch (level)
        {
            case 1:
                {
                    damageMultiplier = 0.50f;
                    attackDelayMultiplier = 2.50f;
                    splashRange = 3.0f;
                    break;
                }
            case 2:
                {
                    damageMultiplier = 0.55f;
                    attackDelayMultiplier = 2.40f;
                    break;
                }
            case 3:
                {
                    damageMultiplier = 0.60f;
                    attackDelayMultiplier = 2.35f;
                    break;
                }
            case 4:
                {
                    damageMultiplier = 0.65f;
                    attackDelayMultiplier = 2.30f;
                    break;
                }
            case 5:
                {
                    damageMultiplier = 0.70f;
                    attackDelayMultiplier = 2.25f;
                    splashRange *= 2;
                    break;
                }
            default:
                break;
        }
    }
}

