using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    PlayerInfo playerInfo;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        playerInfo.HealthPoint = playerInfo.HealthPoint - damage;
        spriteRenderer.material.color = Color.red;
        Invoke("EndTakeDamage", 0.1f);
    }

    public void EndTakeDamage()
    {
        spriteRenderer.material.color = Color.white;
    }
}
