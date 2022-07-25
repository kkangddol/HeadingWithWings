using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIrregularMovement : MonoBehaviour
{
    private Rigidbody2D playerRigid;
    private EnemyInfo enemyInfo;
    bool isStop = false;
    public bool IsStop
    {
        get{ return isStop;}
        set{ isStop = value;}
    }
    public float moveCoolTime = 1.5f;
    bool isMoving = false;
    Rigidbody2D rigid;
    Vector2 moveDirection;
    public float maxSpread = 0.5f;
    public float moveTime = 0.2f;

    private void Start()
    {
        Initialize();
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
            if(!isMoving)
            {
                MoveToTarget();
            }
        }
    }

    void Tracking()
    {
        moveDirection = (playerRigid.position - rigid.position).normalized;
        moveDirection += new Vector2(Random.Range(-maxSpread,maxSpread), Random.Range(-maxSpread,maxSpread));
    }

    void MoveToTarget()
    {
        Tracking();
        isMoving = true;
        rigid.AddForce(moveDirection * enemyInfo.enemyMoveSpeed, ForceMode2D.Impulse);
        Invoke("ResetMoving", moveCoolTime + Random.Range(-0.5f, 0.5f));
        Invoke("StopShort", moveTime);
    }

    void StopShort()
    {
        rigid.velocity = Vector2.zero;
    }

    void ResetMoving()
    {
        isMoving = false;
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
