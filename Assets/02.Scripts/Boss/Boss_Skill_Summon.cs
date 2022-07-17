using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Summon : MonoBehaviour, IBoss_Skill
{
    EnemyInfo enemyInfo;
    EnemyMovement enemyMovement;

    public GameObject summon;
    public GameObject effect;
    bool isEffect = false;

    private void Start() {
        enemyInfo = GetComponent<EnemyInfo>();
        enemyMovement = GetComponent<EnemyMovement>();
    }   
    public void ActivateSkill()
    {
        enemyMovement.StopMove();

        GameObject newEffect = Instantiate(effect, transform.position + Vector3.down * 2 , Quaternion.identity);
        Instantiate(summon, transform.position + transform.right, Quaternion.identity);
        Instantiate(summon, transform.position - transform.right, Quaternion.identity);
        Destroy(newEffect, 1f);
        
        Boss_Skill_Manager.animator.SetTrigger("reset");
        Invoke("ResumeMove", 1f);
    }

    void ResumeMove()
    {
        enemyMovement.ResumeMove();
    }
}
