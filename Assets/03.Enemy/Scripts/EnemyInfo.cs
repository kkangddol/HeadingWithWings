using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public Transform playerTransform;
    public Sprite dieSprite = null;

    [SerializeField]
    private int monsterID;

    public int MonsterID { get { return monsterID; } set { monsterID = value; } }
    private float maxHealthPoint;
    public float healthPoint;
    public float moveSpeed;
    public float meleeDamage;
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileFireDelay;
    public float attackRange;

    private bool isDead;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
            if(value)
            {
                EnemyDie();
            }
        }
    }

    bool isChainDie = false;


    void Start()
    {
        playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        IsDead = false;
    }

    private void FixedUpdate() {
        LookAtPlayer2D();
    }

    public void SetID(int id)
    {
        MonsterID = id;
    }

    public void DataInit()
    {
        this.name = GameManager.Data.MonsterDict[MonsterID].monsterName;
        maxHealthPoint = GameManager.Data.MonsterDict[MonsterID].monsterHp;
        healthPoint = maxHealthPoint;
        moveSpeed = GameManager.Data.MonsterDict[MonsterID].moveSpeed;
        meleeDamage = GameManager.Data.MonsterDict[MonsterID].meleeDamage;
        projectileDamage = GameManager.Data.MonsterDict[MonsterID].projectileDamage;
        projectileSpeed = GameManager.Data.MonsterDict[MonsterID].projectileSpeed;
        projectileFireDelay = GameManager.Data.MonsterDict[MonsterID].projectileFireDelay;
        attackRange = GameManager.Data.MonsterDict[MonsterID].attackRange;
    }

    private void EnemyDie()
    {
        GameManager.Instance.KillCount++;
        meleeDamage = 0;
        StartCoroutine(DieAnimation());
    }

    IEnumerator DieAnimation()
    {
        this.tag = "Untagged";
        GetComponent<IEnemyStopHandler>().StopMove();

        var stopAttack = GetComponent<EnemyRangeAttackBase>();
        if(stopAttack != null)  stopAttack.StopFire();
        var stopSkill = GetComponent<Boss_Skill_Manager>();
        if(stopSkill != null)  stopSkill.enabled = false;

        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        
        SpriteRenderer temp = GetComponentInChildren<SpriteRenderer>();
        temp.sprite = dieSprite;
        // 이전은 10번 반복이였음
        for (int i = 0; i < 3; i++)
        {
            temp.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            temp.color = Color.white * 0.5f;
            yield return new WaitForSeconds(0.1f);
        }

        temp.color = Color.clear;
        if(!isChainDie)
        {
            GetComponent<EnemyDropItem>().DropItem();
        }
        else
        {
            isChainDie = false;
        }
        GameManager.Instance.stageManager.enemies.Remove(this);
        Destroy(this.gameObject, 0.1f);
    }

    public void ChainDie()
    {
        meleeDamage = 0;
        isChainDie = true;
        StartCoroutine(DieAnimation());
    }

    private void LookAtPlayer2D()
    {
        float relativeX = transform.position.x - playerTransform.position.x;
        
        if(relativeX > 0)
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        else if(relativeX < 0)
            GetComponentInChildren<SpriteRenderer>().flipX = true;
    }

    public void SumHealthPoint(float value)
    {
        healthPoint += value;
        if(healthPoint > maxHealthPoint)
        {
            healthPoint = maxHealthPoint;
        }
    }
}
