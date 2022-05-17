using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInfo : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    public int healthPoint;
    public int enemyDamage;
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
            if(isDead == true)
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
        isDead = false;
        targetTransform = GameObject.FindGameObjectWithTag(PLAYER).GetComponent<Transform>();
    }

    private void EnemyDie()
    {
        enemyDamage = 0;
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.black;
        Destroy(gameObject, 0.1f);
    }
}
