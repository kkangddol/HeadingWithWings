using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour, ITakeBossAttack
{
    PlayerInfo playerInfo;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public bool isGodMode = false;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        if(isGodMode) return;
        playerInfo.HealthPoint = playerInfo.HealthPoint - damage;
        spriteRenderer.material.color = Color.red;
        Invoke("EndTakeDamage", 0.1f);
    }

    public void EndTakeDamage()
    {
        spriteRenderer.material.color = Color.white;
    }

    public void TakeBossAttack(Transform hitTr, float damage, float knockbackSize)
    {
        TakeDamage(damage);
        Vector2 reactVec = transform.position - hitTr.position;

        rigid.velocity = Vector2.zero;
        reactVec = reactVec.normalized;
        rigid.AddForce(reactVec * knockbackSize, ForceMode2D.Impulse);
    }
}
