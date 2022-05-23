using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    private EnemyInfo enemyInfo;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == PLAYER)
        {
            other.GetComponent<PlayerTakeDamage>().TakeDamage(enemyInfo.enemyDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == PLAYER)
        {
            other.GetComponent<PlayerTakeDamage>().EndTakeDamage();
        }
    }
}
