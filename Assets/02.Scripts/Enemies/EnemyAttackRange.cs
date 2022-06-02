using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private EnemyMovement enemyMovement;
    public EnemyProjectile enemyProjectile;
    public float attackRange;
    public float fireDelay;
    private bool isAttacking;
    private Coroutine fireCoroutine;
    private bool isInRange;
    public bool IsInRange
    {
        get
        {
            return isInRange;
        }
        set
        {
            isInRange = value;

            if(isInRange == true && fireCoroutine == null)
            {
                enemyMovement.StopMove();
                fireCoroutine = StartCoroutine(FireCycle());
            }
            else if(isInRange == false)
            {
                enemyMovement.ResumeMove();
                fireCoroutine = null;
            }
            
        }
    }

    private void Start()
    {
        Initialize();
        StartCoroutine(CheckRange());
    }

    void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyMovement = GetComponent<EnemyMovement>();
        // attackRange = GameManager.Data.MonsterDict[enemyInfo.monsterID];
        fireDelay = GameManager.Data.MonsterDict[int.Parse(gameObject.name)].projectileFireDelay;
        attackRange = 10; // Test
        isInRange = false;
        isAttacking = false;
        fireCoroutine = null;
    }

    IEnumerator CheckRange()
    {
        while(!enemyInfo.IsDead)
        {
            yield return null;
            if(Vector3.Distance(transform.position, enemyInfo.targetTransform.position) <= attackRange)
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
        transform.LookAt(enemyInfo.targetTransform);
        EnemyProjectile newProjectile = Instantiate<EnemyProjectile>(enemyProjectile, transform.position, transform.rotation);
        newProjectile.transform.LookAt(enemyInfo.targetTransform);
        newProjectile.damage = GameManager.Data.MonsterDict[enemyInfo.MonsterID].projectileDamage;
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * 15f, ForceMode.Impulse);
    }

    IEnumerator FireCycle()
    {
        while(isInRange)
        {
            yield return null;
            if(!isAttacking)
            {
                isAttacking = true;
                Fire();
                StartCoroutine(FireDelay());
            }
        }
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireDelay);
        isAttacking = false;
    }
}
