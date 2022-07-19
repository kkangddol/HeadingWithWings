using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, IEnemyStopHandler
{
    private EnemyInfo enemyInfo;
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
}
