using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    PlayerInfo playerInfo;
    SkinnedMeshRenderer skinnedMeshRenderer;
    public HealthBar healthBar;

    private void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }


    //얘가 맞지말고 때리는쪽이 때리는걸로
    //여기는 수정 필요함 22/04/24 일단 막짜놓은거라
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "ENEMY")
        {
            playerInfo.hp -= other.GetComponent<EnemyPrototype>().damage;
            healthBar.SetHealth((int)playerInfo.hp);
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
