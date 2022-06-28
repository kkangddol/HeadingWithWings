using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Whale : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    [SerializeField]
    public float healthPoint;
    public float bossDamage;
    public float bossMoveSpeed;
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
        bossDamage = 0;
        GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        GetComponent<EnemyDropItem>().DropItem();
        Destroy(gameObject, 0.1f);
    }
}
