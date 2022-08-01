using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing_DragonFly : Equipment, ActiveWing
{
    const int EQUIPID = 20400;
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
    float timeLimit = 2;
    float timeElapsed = 0;

    public GameObject effect;

    [Tooltip("HitEffect Color")]
    public Color effectColor;

    private bool isAttacking = false;

    public GameObject skillButton;

    PlayerTakeDamage playerTakeDamage;

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
        playerTakeDamage = playerInfo.gameObject.GetComponent<PlayerTakeDamage>();
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


        playerTakeDamage.isGodMode = true;
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

    private void HitEffect(Vector3 hitPos)
    {
        GameObject obj = BasicEffectPool.Instance.GetObject();
        obj.transform.position = hitPos;
        obj.GetComponent<SpriteRenderer>().color = effectColor;
        BasicEffectPool.Instance.ReturnObject(obj, obj.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("ENEMY"))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, playerInfo.damage * damageMultiplier, knockbackSize);
            HitEffect(other.transform.position);
        }
    }

    void StopShort()
    {
        playerTakeDamage.isGodMode = false;
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

    private void OnDestroy() {
        playerMoveController.ResumeMove();
        col.enabled = false;
        effect.SetActive(false);
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
        damageMultiplier = GameManager.Data.WingEquipDict[EQUIPID + this.level].damageMultiplier;
        skillDelayMultiplier = GameManager.Data.WingEquipDict[EQUIPID + this.level].delayMultiplier;
        knockbackSize = GameManager.Data.WingEquipDict[EQUIPID + this.level].knockBackSize;
        maxDashCount = GameManager.Data.WingEquipDict[EQUIPID + this.level].maxDashCount;
    }


}
