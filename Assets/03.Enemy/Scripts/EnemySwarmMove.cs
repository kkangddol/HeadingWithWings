using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySwarmMove : MonoBehaviour, IEnemyStopHandler
{
    private Rigidbody2D playerRigid;
    private EnemyInfo enemyInfo;
    bool isStop = false;
    public bool IsStop
    {
        get{ return isStop;}
        set{ isStop = value;}
    }
    Vector2 currentPos;
    Rigidbody2D rigid;
    Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        Initialize();
        Tracking();
    }
    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        rigid = GetComponent<Rigidbody2D>();
        playerRigid = GameObject.FindWithTag("PLAYER").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if(!isStop)
        {
            MoveToTarget();
        }
        if(Vector2.Distance(rigid.position,playerRigid.position) >= 15)
        {
            Tracking();
        }
    }

    public void Tracking()
    {
        moveDirection = (playerRigid.position - rigid.position).normalized;
    }

    void MoveToTarget()
    {
        rigid.AddForce(moveDirection * enemyInfo.moveSpeed);
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
