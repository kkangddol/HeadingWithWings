using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private SpriteRenderer enemySprite;
    public bool isStop = false;
    private float enemySpeed = 0.0f;
    private Color freezeColor = new Color(0f, 1f, 1f, 0.95f);

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
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
        if(speedMultiplier == 0)
        {
            enemySprite.color = freezeColor;
        }  
        enemyInfo.enemyMoveSpeed = enemySpeed * speedMultiplier;
        yield return new WaitForSeconds(duration);
        enemyInfo.enemyMoveSpeed = enemySpeed;
        enemySprite.color = Color.white;
    }

    public void SlowMove(float speedMultiplier, float duration = 1.5f)
    {
        StartCoroutine(EnemySetSlow(speedMultiplier, duration));
    }
}
