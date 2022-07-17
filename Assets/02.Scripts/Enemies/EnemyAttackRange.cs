using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private EnemyMovement enemyMovement;
    public EnemyProjectile enemyProjectile;
    public float projectileDamage;
    public float projectileSpeed;
    public float fireDelay;
    public float attackRange;
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
        if(IsInRange)
        {
            enemyMovement.StopMove();

            if(isAttacking) return;
            
            isAttacking = true;
            Fire();
            StartCoroutine(FireDelay());
        }
        else if(!IsInRange && enemyMovement.isStop)
        {
            enemyMovement.ResumeMove();
        }
    }

    void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyMovement = GetComponent<EnemyMovement>();
        //attackRange = GameManager.Data.MonsterDict[enemyInfo.monsterID];
        //fireDelay = GameManager.Data.MonsterDict[int.Parse(gameObject.name)].projectileFireDelay;
        isInRange = false;
        isAttacking = false;
    }

    IEnumerator CheckRange()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);
        while(!enemyInfo.IsDead)
        {
            yield return waitTime;
            if(Vector2.Distance(transform.position, GameManager.playerTransform.position) <= attackRange)
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
        EnemyProjectile newProjectile = Instantiate<EnemyProjectile>(enemyProjectile, transform.position, transform.rotation);
        newProjectile.transform.LookAt(GameManager.playerTransform);
        //newProjectile.damage = GameManager.Data.MonsterDict[enemyInfo.MonsterID].projectileDamage;
        newProjectile.damage = projectileDamage;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(newProjectile.transform.forward * projectileSpeed, ForceMode2D.Impulse);
        //newProjectile.GetComponent<Rigidbody2D>().AddForce(enemyInfo.targetTransform.position.normalized * projectileSpeed, ForceMode2D.Impulse);
    }

    // IEnumerator FireCycle()
    // {
    //     while(isInRange)
    //     {
    //         yield return null;
    //         if(!isAttacking)
    //         {
    //             isAttacking = true;
    //             Fire();
    //             StartCoroutine(FireDelay());
    //         }
    //     }
    // }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        isAttacking = false;
    }
}
