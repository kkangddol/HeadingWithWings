using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private int monsterID;
    public int MonsterID { get { return monsterID; } set { } }
    public float healthPoint;
    public float enemyDamage;
    public float enemyMoveSpeed;
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
        Initialize();
    }

    void Initialize()
    {
        //monsterID = int.Parse(gameObject.name);
        IsDead = false;
        //healthPoint = GameManager.Data.MonsterDict[monsterID].monsterHp;
        //enemyDamage = GameManager.Data.MonsterDict[monsterID].collisionDamage;
    }

    private void EnemyDie()
    {
        GameManager.Instance.KillCount++;
        enemyDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        GetComponent<EnemyDropItem>().DropItem();
        Destroy(gameObject, 0.1f);
    }

    public void ChainDie()
    {
        enemyDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        Destroy(gameObject, 0.1f);
    }
}
