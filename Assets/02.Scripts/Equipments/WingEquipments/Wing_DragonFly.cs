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
        set{isDashStarted = value;}
    }

    public int maxDashCount = 2;
    [SerializeField] private int dashCount;
    public int DashCount
    {
        get{return dashCount;}
        set
        {
            dashCount = value;
            skillButton.GetComponent<SkillStackHandler>().SetStackText(value);
        }
    }
    public float dashSpeed = 30f;
    public float dashTime = 0.5f;
    float timeLimit = 1;
    float timeElapsed = 0;

    public GameObject effect;

    private bool isAttacking = false;

    public GameObject skillButton;

    public GameObject hitEffect;

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
        DashCount = maxDashCount;
    }

    public void SetButton(GameObject button)
    {
        skillButton = button;
        skillButton.GetComponent<SkillStackHandler>().SetStackText(maxDashCount);
    }

    public void ActivateSkill()
    {
        //특수 돌진
        if(isCoolDown) return;

        if(isAttacking) return;

        isAttacking = true;
        playerMoveController.StopMove();
        IsDashStarted = true;
        timeElapsed = 0;
        DashCount--;
        playerRigid.AddForce(playerInfo.headVector.normalized * dashSpeed, ForceMode2D.Impulse);
        effect.transform.rotation = Quaternion.Euler(0, 0, playerInfo.headAngle);
        Invoke("StopShort", dashTime);

        effect.SetActive(true);
        col.enabled = true;

        if(DashCount <= 0)
        {
            StartCoolDown();
            return;
        }
    }

    private void Update() {
        if(IsDashStarted)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed >= timeLimit)
            {
                StartCoolDown();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("ENEMY"))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, playerInfo.damage * damageMultiplier, knockbackSize);
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
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
        DashCount = 0;
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        float coolTime = playerInfo.skillDelay * skillDelayMultiplier;
        skillButton.GetComponent<SkillCoolTimeHandler>().SetCoolTime(coolTime);
        skillButton.GetComponent<SkillCoolTimeHandler>().StartCoolTime();
        while(isCoolDown)
        {
            yield return null;
            coolTime -= Time.deltaTime;
            if(coolTime <= 0)
            {
                isCoolDown = false;
                DashCount = maxDashCount;
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
