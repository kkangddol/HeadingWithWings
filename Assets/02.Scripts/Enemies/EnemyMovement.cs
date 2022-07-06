using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    public bool isStop = false;
    private float enemySpeed = 0.0f;

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemySpeed = enemyInfo.enemyMoveSpeed;
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

    IEnumerator EnemySetSlow(float speedMultiplier, float duration)
    {
        // 느려지는 동안 색 변경도 이 안에 추가
        enemyInfo.enemyMoveSpeed = enemySpeed * speedMultiplier;
        yield return new WaitForSeconds(duration);
        enemyInfo.enemyMoveSpeed = enemySpeed;
    }

    public void SlowMove(float speedMultiplier, float duration = 1.5f)
    {
        StartCoroutine(EnemySetSlow(speedMultiplier, duration));
    }
}
