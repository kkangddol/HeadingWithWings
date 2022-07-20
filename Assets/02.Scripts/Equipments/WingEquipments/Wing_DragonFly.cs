using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing_DragonFly : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    PlayerMoveController playerMoveController;
    private bool isCoolDown = false;
    [HideInInspector] public float coolTime;
    Rigidbody2D rb;

    public float damageMultiplier;
    public float skillDelayMultiplier;
    public float knockbackSize = 0.1f;

    private bool isDashStarted = false;
    public bool IsDashStarted
    {
        get{return isDashStarted;}
        set
        {
            isDashStarted = value;
            if(value == true)
            {
                CancelInvoke("StartCoolDown");
                Invoke("StartCoolDown", timeLimit);
            }
        }
    }

    public int dashCount = 1;
    [SerializeField] private int currentDashCount;
    public float dashSpeed = 30f;
    float timeLimit = 1;

    List<IEnumerator> DashCoroutines;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        playerMoveController = GameObject.FindWithTag("PLAYER").GetComponent<PlayerMoveController>();
        rb = GameObject.FindWithTag("PLAYER").GetComponent<Rigidbody2D>();
        currentDashCount = dashCount;
    }

    public void ActivateSkill()
    {
        //특수 돌진
        if(isCoolDown) return;

        playerMoveController.StopMove();
        IsDashStarted = true;
        currentDashCount--;
        rb.AddForce(playerInfo.headVector.normalized * dashSpeed, ForceMode2D.Impulse);
        Invoke("StopShort", 0.1f);

        StartCoroutine(BodyTackle());

        if(currentDashCount <= 0)
        {
            StartCoolDown();
            return;
        }
    }

    IEnumerator BodyTackle()
    {
        float dashTime = 0.1f;
        while(dashTime > 0)
        {
            yield return null;
            dashTime -= Time.deltaTime;

            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.8f);
            foreach(var target in hit)
            {
                if(target.CompareTag("ENEMY"))
                {
                    target.GetComponent<EnemyTakeDamage>().TakeDamage(transform, playerInfo.damage * damageMultiplier, knockbackSize);
                }
            }
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawSphere(transform.position, 0.8f);
    // }

    void StopShort()
    {
        rb.velocity = Vector2.zero;
        playerMoveController.ResumeMove();
    }

    void StartCoolDown()
    {
        isCoolDown = true;
        IsDashStarted = false;
        currentDashCount = 0;
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        float coolTime = playerInfo.skillDelay * skillDelayMultiplier;
        while(isCoolDown)
        {
            yield return null;
            coolTime -= Time.deltaTime;
            if(coolTime <= 0)
            {
                isCoolDown = false;
                currentDashCount = dashCount;
                break;
            }
        }
    }


    public override void SetLevel(int newLevel)
    {
        this.level = newLevel;

        //220527 하드코딩이므로 DataManager 이용할 것.
        //잠자리 날개의 대쉬 횟수를 증가시켜 줌
        //잠자리 날개의 쿨타임을 감소시켜 줌
        //잠자리 날개의 공격력을 증가시켜 줌
        //만렙시 뭘 줘야할까
        switch(level)
        {
            case 1:
            {
                break;
            }
            case 2:
            {
                break;
            }
            case 3:
            {
                break;
            }
            case 4:
            {
                break;
            }
            case 5:
            {
                break;
            }
            default:
                break;
        }
    }
}
