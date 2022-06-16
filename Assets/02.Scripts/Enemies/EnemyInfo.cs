using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInfo : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    [SerializeField]
    private int monsterID;
    public int MonsterID { get { return monsterID; } set { } }
    public float healthPoint;
    public float enemyDamage;
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

    public Transform targetTransform;

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
        targetTransform = GameManager.playerInfo.GetComponent<Transform>();
    }

    private void EnemyDie()
    {
        GameManager.Instance.KillCount++;
        enemyDamage = 0;
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        GetComponent<EnemyDropItem>().DropItem();
        Destroy(gameObject, 0.1f);
    }
}
