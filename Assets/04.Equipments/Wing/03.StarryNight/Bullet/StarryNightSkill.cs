using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Black hole
public class StarryNightSkill : MonoBehaviour
{
    public float range = 3f;
    public float gravity = 2f;
    public float damage = 2f;

    private const string ENEMY = "ENEMY";
    private bool isDotDamage = false;

    private void Start() {
        StartCoroutine(SwitchDotDamageState());
    }

    private void Update()
    {
        BlackHole();
    }

    private void BlackHole()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, range);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (!hitCollider.CompareTag(ENEMY))
                continue;

            hitCollider.transform.position = Vector2.MoveTowards(hitCollider.transform.position, this.transform.position, gravity * Time.deltaTime);
            if(isDotDamage)  hitCollider.GetComponent<EnemyTakeDamage>().TakeDamage(hitCollider.transform, damage, 0f);
        }
    }

    IEnumerator SwitchDotDamageState()
    {
        while(true)
        {
            isDotDamage = true;
            yield return null;
            isDotDamage = false;
            yield return new WaitForSeconds(1f);
        }
    }
}
