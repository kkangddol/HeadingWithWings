using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing_DragonFly : Equipment, ActiveWing
{
    PlayerInfo playerInfo;
    PlayerMoveController playerMoveController;
    private bool isCoolDown = false;
    [HideInInspector] public float coolTime;
    Rigidbody2D playerRigid;
    Collider2D col;

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
    public float dashTime = 0.5f;
    float timeLimit = 1;

    public GameObject effect;

    private bool isAttacking = false;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        playerInfo = GameObject.FindWithTag("PLAYER").GetComponent<PlayerInfo>();
        playerMoveController = GameObject.FindWithTag("PLAYER").GetComponent<PlayerMoveController>();
        playerRigid = GameObject.FindWithTag("PLAYER").GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        currentDashCount = dashCount;
    }

    public void ActivateSkill()
    {
        //특수 돌진
        if(isCoolDown) return;

        if(isAttacking) return;

        isAttacking = true;
        playerMoveController.StopMove();
        IsDashStarted = true;
        currentDashCount--;
        playerRigid.AddForce(playerInfo.headVector.normalized * dashSpeed, ForceMode2D.Impulse);
        effect.transform.rotation = Quaternion.Euler(0, 0, playerInfo.headAngle);
        Invoke("StopShort", dashTime);

        effect.SetActive(true);
        col.enabled = true;

        if(currentDashCount <= 0)
        {
            StartCoolDown();
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("ENEMY"))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, playerInfo.damage * damageMultiplier, knockbackSize);
        }
    }

    void StopShort()
    {
        playerRigid.velocity = Vector2.zero;
        playerMoveController.ResumeMove();
        col.enabled = false;
        effect.SetActive(false);
        isAttacking = false;
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
