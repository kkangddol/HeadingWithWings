using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeriodicMovement : MonoBehaviour, IEnemyStopHandler
{
    private EnemyInfo enemyInfo;
    bool isStop = false;
    public bool IsStop
    {
        get{ return isStop;}
        set{ isStop = value;}
    }
    public float moveCoolTime = 3f;
    bool isMoving = false;
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
            if(!isMoving)
            {
                MoveToTarget();
            }
        }
    }

    void Tracking()
    {
        moveDirection = (GameManager.playerRigidbody.position - rigid.position).normalized;
    }

    void MoveToTarget()
    {
        Tracking();
        isMoving = true;
        rigid.AddForce(moveDirection * enemyInfo.enemyMoveSpeed, ForceMode2D.Impulse);
        Invoke("ResetMoving", moveCoolTime + Random.Range(-0.5f, 0.5f));
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

    // IEnumerator EnemySetSlow(float speedMultiplier, float duration)
    // {
    //     agent.speed = enemySpeed * speedMultiplier;
    //     yield return new WaitForSeconds(duration);
    //     agent.speed = enemySpeed;
    // }

    // public void SlowMove(float speedMultiplier, float duration = 1.5f)
    // {
    //     StartCoroutine(EnemySetSlow(speedMultiplier, duration));
    // }
}
