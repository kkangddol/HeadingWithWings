using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_SprayFeather : MonoBehaviour, IBoss_Skill
{
    //플레이어 방향 또는 전 방향으로 페가수스의 깃털을 흩뿌립니다. (탄막패턴)
    //깃털을 흩뿌리는것은 ShotgunAttack.cs 스크립트와 MilitaryGirlWing.cs 의 공격 코드를 참고하시면 좋을 것 같습니다!
    public void ActivateSkill()
    {
        Boss_Skill_Manager.animator.SetTrigger("reset");
    }
}
