using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilWingSkill : MonoBehaviour
{
    const string ENEMY = "ENEMY";
    public float damage;
    public float knockbackSize;
    public float attackInterval = 0.3f;
    bool isDelay = false;

    public void Init()
    {
        isDelay = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDelay) return;

        isDelay = true;

        if (other.CompareTag(ENEMY))
        {
            other.GetComponent<EnemyTakeDamage>().TakeDamage(transform, damage, knockbackSize);
            GameManager.playerInfo.HealthPoint += damage * 0.1f;
        }

        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackInterval);
        isDelay = false;
    }
}
