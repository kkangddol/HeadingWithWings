using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    PlayerInfo playerInfo;
    SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void TakeDamage(float damage)
    {
        playerInfo.HealthPoint = playerInfo.HealthPoint - damage;
        skinnedMeshRenderer.material.color = Color.red;
        Invoke("EndTakeDamage", 0.1f);
    }

    public void EndTakeDamage()
    {
        skinnedMeshRenderer.material.color = Color.white;
    }
}
