using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IEnemyStopHandler
{
    private EnemyInfo enemyInfo;
    private SpriteRenderer enemySprite;
    private float enemySpeed = 0.0f;
    private Color freezeColor = new Color(0f, 1f, 1f, 0.95f);
    bool isStop = false;
    public bool IsStop
    {
        get{ return isStop;}
        set{ isStop = value;}
    }
    Vector2 currentPos;
    Rigidbody2D rigid;
    Vector2 moveDirection;



    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        enemySpeed = enemyInfo.enemyMoveSpeed;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(!isStop)
        {
            MoveToTarget();
        }
    }

    // void FollowTarget()
    // {
    //     //transform.position = Vector2.MoveTowards(transform.position, GameManager.playerTransform.position, enemyInfo.enemyMoveSpeed * Time.deltaTime);
    //     currentPos = Vector2.MoveTowards(transform.position, GameManager.playerTransform.position, Time.fixedDeltaTime * enemyInfo.enemyMoveSpeed);
    //     rigid.MovePosition(new Vector2(currentPos.x, currentPos.y));
    // }

    void Tracking()
    {
        moveDirection = (GameManager.playerRigidbody.position - rigid.position).normalized;
    }

    void MoveToTarget()
    {
        Tracking();
        rigid.AddForce(moveDirection * enemyInfo.enemyMoveSpeed);
    }

    public void StopMove()
    {
        isStop = true;
        rigid.velocity = Vector2.zero;
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
