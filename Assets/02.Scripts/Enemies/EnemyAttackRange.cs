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
            if(isInRange == true)
            {
                enemyMovement.StopMove();
                StartCoroutine(FireCycle());
            }
            else
            {
                StopCoroutine(FireCycle());
                enemyMovement.ResumeMove();
            }
        }
    }
    private Transform targetTransform;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyMovement = GetComponent<EnemyMovement>();
        isInRange = false;
    }

    IEnumerator CheckRange()
    {
        while(!enemyInfo.IsDead)
        {
            yield return null;
            if(Vector3.Distance(transform.position, targetTransform.position) <= attackRange)
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
        newProjectile.transform.LookAt(enemyInfo.targetTransform);
        newProjectile.damage = enemyInfo.enemyDamage;
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * 15f, ForceMode.Impulse);
    }

    IEnumerator FireCycle()
    {
        while(isInRange)
        {
            yield return new WaitForSeconds(fireDelay);
            Fire();
        }
    }
}
