using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private NavMeshAgent agent;

    private void Start()
    {
        Initialize();
        StartCoroutine(EnemySetDestination());
    }

    private void FixedUpdate() {
        if(transform.position.x - enemyInfo.targetTransform.position.x > 0)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else
            GetComponentInChildren<SpriteRenderer>().flipX = true;
    }

    private void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyInfo = GetComponent<EnemyInfo>();
        agent.speed = GameManager.Data.MonsterDict[enemyInfo.MonsterID].monsterSpeed;
    }

    IEnumerator EnemySetDestination()
    {
        while(!enemyInfo.IsDead)
        {
            yield return null;
            agent.SetDestination(enemyInfo.targetTransform.position);
        }
    }

    public void StopMove()
    {
        StopCoroutine(EnemySetDestination());
        agent.isStopped = true;
    }
    public void ResumeMove()
    {
        agent.isStopped = false;
        StartCoroutine(EnemySetDestination());
    }
}
