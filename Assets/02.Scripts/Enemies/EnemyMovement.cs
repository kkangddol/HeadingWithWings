using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    public bool isStop = false;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }

    private void Update() {
        if(!isStop)
        {
            FollowTarget();
        }
    }

    private void FixedUpdate() {
        if(transform.position.x - enemyInfo.targetTransform.position.x > 0)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else if(transform.position.x - enemyInfo.targetTransform.position.x < 0)
            GetComponentInChildren<SpriteRenderer>().flipX = true;
    }

    void FollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, enemyInfo.targetTransform.position, enemyInfo.enemyMoveSpeed * Time.deltaTime);
    }

    public void StopMove()
    {
        isStop = true;
    }
    public void ResumeMove()
    {
        isStop = false;
    }
}
