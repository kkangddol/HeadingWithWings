using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Dash : MonoBehaviour, IBoss_Skill
{
    //플레이어 방향으로 직선으로 돌진하는 스킬입니다.
    //EnemyAttackMelee와는 별개로 OverlapSphere 이용해서 돌진의 데미지를 따로 입혀주면 좋을 것 같습니다.
    //돌진은 점프와 마찬가지로 피아식별을 하지 않습니다.
    //피아식별무시는 PlayerTakeDamage와 EnemyTakeDamage가 상속받는 ITakeBossAttack 이라는 인터페이스로 모두 데미지를 줄 수 있습니다
    public void ActivateSkill()
    {
        Boss_Skill_Manager.animator.SetTrigger("reset");
    }
}
