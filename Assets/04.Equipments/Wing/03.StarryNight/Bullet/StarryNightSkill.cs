using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Black hole
public class StarryNightSkill : MonoBehaviour
{
    public float range = 3f;
    public float gravity = 2f;

    private const string ENEMY = "ENEMY";

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
        }
    }
}
