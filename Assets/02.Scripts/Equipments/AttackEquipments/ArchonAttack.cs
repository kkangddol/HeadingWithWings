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

    private Vector3 toTargetNormal = Vector3.zero;
    private float toTargetAngle = 0.0f;
    private Vector3 Scaling = Vector3.one;
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
        bullet.damage = playerInfo.damage * damageMultiplier;
        bullet.knockbackSize = knockbackSize;
        ((Bullet_Archon)bullet).splashRange = splashRange;
        StartCoroutine(BulletScaling());
        ((Bullet_Archon)bullet).SplashDamage(targetTransform);
        isCoolDown = true;
        StartCoroutine(CoolDown());
    }

    IEnumerator BulletScaling()
    {
        toTargetNormal = (targetTransform.position - this.transform.position).normalized;
        toTargetAngle = Mathf.Atan2(toTargetNormal.y, toTargetNormal.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(toTargetAngle, Vector3.forward);
        Scaling.x = Vector2.Distance(targetTransform.position, this.transform.position);
        transform.localScale = Scaling;

        yield return new WaitForSeconds(0.2f);

        transform.localScale = Vector3.zero;
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            yield return null;

            if(isCoolDown)  continue;

            targetTransform = detectEnemy.FindNearestEnemy(ENEMY);

            if (targetTransform == transform)  continue;

            if (Vector2.Distance(transform.position, targetTransform.position) > attackRange)  continue;

            Fire();
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
                    attackDelayMultiplier = 1.50f;
                    splashRange = 3.0f;
                    break;
                }
            case 2:
                {
                    damageMultiplier = 0.55f;
                    attackDelayMultiplier = 1.40f;
                    break;
                }
            case 3:
                {
                    damageMultiplier = 0.60f;
                    attackDelayMultiplier = 1.30f;
                    break;
                }
            case 4:
                {
                    damageMultiplier = 0.65f;
                    attackDelayMultiplier = 1.20f;
                    break;
                }
            case 5:
                {
                    damageMultiplier = 0.70f;
                    attackDelayMultiplier = 1.10f;
                    splashRange *= 2;
                    break;
                }
            default:
                break;
        }
    }
}

