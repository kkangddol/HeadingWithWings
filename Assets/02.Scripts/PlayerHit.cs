using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    PlayerInfo playerInfo;
    SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "ENEMY")
        {
            playerInfo.hp -= other.GetComponent<EnemyPrototype>().damage;
            skinnedMeshRenderer.material.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "ENEMY")
        {
            skinnedMeshRenderer.material.color = Color.white;
        }
    }
}
