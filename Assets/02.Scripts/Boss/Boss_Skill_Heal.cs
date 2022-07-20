using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Heal : MonoBehaviour, IBoss_Skill
{
    //스스로의 체력을 회복합니다.
    public void ActivateSkill()
    {
        Boss_Skill_Manager.animator.SetTrigger("reset");
    }
}
