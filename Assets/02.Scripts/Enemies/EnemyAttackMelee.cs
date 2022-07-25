using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMelee : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    private EnemyInfo enemyInfo;
    public float attackDelay = 0.1f;
    bool isAttacked = false;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(isAttacked) return;

        if(other.tag == PLAYER)
        {
            isAttacked = true;
            other.GetComponent<PlayerTakeDamage>().TakeDamage(enemyInfo.enemyDamage);
            StartCoroutine(CoolDown());
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == PLAYER)
        {
            other.GetComponent<PlayerTakeDamage>().EndTakeDamage();
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacked = false;
    }

}
