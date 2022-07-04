using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Jump : MonoBehaviour, Boss_Skill
{
    EnemyInfo enemyInfo;

    private void Start() {
        enemyInfo = GetComponent<EnemyInfo>();
    }   
    public void ActivateSkill()
    {

    }
}
