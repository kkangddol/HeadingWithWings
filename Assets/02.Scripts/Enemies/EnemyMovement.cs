using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        Initialize();
        StartCoroutine(EnemySetDestination());
    }

    private void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyInfo = GetComponent<EnemyInfo>();
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
    }
    public void ResumeMove()
    {
        StartCoroutine(EnemySetDestination());
    }
}
