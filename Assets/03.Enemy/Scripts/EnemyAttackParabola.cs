using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackParabola : EnemyRangeAttackBase
{
    private Transform playerTransform;
    private EnemyInfo enemyInfo;
    private IEnemyStopHandler stopHandler;
    public EnemyProjectile enemyProjectile;
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
        for (int i = 0; i < projectileCount; i++)
        {
            EnemyProjectile newProjectile = Instantiate<EnemyProjectile>(enemyProjectile, transform.position, Quaternion.identity);
            newProjectile.damage = enemyInfo.projectileDamage;
            ((EnemyParabolaProjectile)newProjectile).startPos = this.transform.position;
            ((EnemyParabolaProjectile)newProjectile).targetPos = playerTransform.position;
        }
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(enemyInfo.projectileFireDelay);
        isAttacking = false;
    }
    
}
