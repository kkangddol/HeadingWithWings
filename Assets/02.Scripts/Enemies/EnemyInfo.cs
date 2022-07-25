using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private Transform playerTransform;
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


    void Start()
    {
        playerTransform = GameObject.FindWithTag("PLAYER").GetComponent<Transform>();
        IsDead = false;
        maxHealthPoint = healthPoint;
    }

    public void DataInit()
    {
        maxHealthPoint = GameManager.Data.MonsterDict[MonsterID].monsterHp;
        moveSpeed = GameManager.Data.MonsterDict[MonsterID].moveSpeed;
    }

    public void SetID(int id)
    {
        MonsterID = id;
    }

    private void FixedUpdate() {
        LookAtPlayer2D();
    }

    private void EnemyDie()
    {
        GameManager.Instance.KillCount++;
        meleeDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        GetComponent<EnemyDropItem>().DropItem();
        Destroy(gameObject, 0.1f);
    }

    public void ChainDie()
    {
        meleeDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        Destroy(gameObject, 0.1f);
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
