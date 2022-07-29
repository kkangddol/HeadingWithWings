using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Impact : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    const string ENEMY = "ENEMY";
    private EnemyInfo enemyInfo;
    public float attackDelay = 1f;
    public float damageMultiplier = 2f;
    bool isAttacked = false;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isAttacked) return;

        if(other.tag == PLAYER || other.tag == ENEMY)
        {
            isAttacked = true;
            other.GetComponent<PlayerTakeDamage>().TakeDamage(enemyInfo.meleeDamage * damageMultiplier);
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacked = false;
    }

}
