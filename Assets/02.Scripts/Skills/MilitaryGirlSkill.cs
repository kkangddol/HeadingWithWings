using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryGirlSkill : MonoBehaviour
{
    const string ENEMY = "ENEMY";
    public float damage;
    public float knockbackSize;
    public float attackInterval = 0.5f;
    bool isDelay = false;

    public void Init()
    {
        isDelay = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(isDelay) return;

        isDelay = true;

        if(other.tag == ENEMY)
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
        }

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackInterval);
        isDelay = false;
    }
}
