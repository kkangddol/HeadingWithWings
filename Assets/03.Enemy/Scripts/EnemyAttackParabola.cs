using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackParabola : EnemyRangeAttackBase
{
    private Transform playerTransform;
    private EnemyInfo enemyInfo;
    private IEnemyStopHandler stopHandler;
    public int projectileCount = 1;
    private bool isAttacking;
    private bool isInRange = false;
    public bool IsInRange
    {
        get
        {
            return isInRange;
        }
        set
        {
            isInRange = value;
        }
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(CheckRange());
    }

    private void Update()
    {
        if (IsInRange)
        {
            stopHandler.StopMove();

            if (isAttacking) return;

            isAttacking = true;
            Fire();
            StartCoroutine(FireDelay());
        }
        else if (!IsInRange && stopHandler.IsStop)
        {
            stopHandler.ResumeMove();
        }
    }

    void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        playerTransform = enemyInfo.playerTransform;
        stopHandler = GetComponent<IEnemyStopHandler>();
        //attackRange = GameManager.Data.MonsterDict[enemyInfo.monsterID];
        //fireDelay = GameManager.Data.MonsterDict[int.Parse(gameObject.name)].projectileFireDelay;
        isInRange = false;
        isAttacking = false;
    }

    IEnumerator CheckRange()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);
        while (!enemyInfo.IsDead)
        {
            yield return waitTime;
            if (Vector2.Distance(transform.position, playerTransform.position) <= enemyInfo.attackRange)
            {
                IsInRange = true;
            }
            else
            {
                IsInRange = false;
            }
        }
    }

    void Fire()
    {
        EnemyProjectile projectile = GetProjectile(CupidProjectilePool.Instance);
        projectile.gameObject.SetActive(false);
        projectile.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        projectile.damage = enemyInfo.projectileDamage;
        ((EnemyParabolaProjectile)projectile).startPos = this.transform.position;
        ((EnemyParabolaProjectile)projectile).targetPos = playerTransform.position;
        projectile.gameObject.SetActive(true);
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(enemyInfo.projectileFireDelay);
        isAttacking = false;
    }
    
}
