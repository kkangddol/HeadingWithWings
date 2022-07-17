using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Manager : MonoBehaviour
{

    enum BossSkill
    {
        jump,
        summon
    }

    public static Animator animator;
    float trembleTime = 2f;

    IBoss_Skill[] skills;
    EnemyMovement enemyMovement;

    float skillCoolTime;
    bool isCoolTime = true;
    

    private void Start() {
        animator = GetComponentInChildren<Animator>();
        skills = GetComponents<IBoss_Skill>();
        enemyMovement = GetComponent<EnemyMovement>();
        skillCoolTime = Random.Range(0, 5f);
    }

    private void Update() {
        if(!isCoolTime)
        {
            SkillSequence();
        }

        skillCoolTime -= Time.deltaTime;

        isCoolTime = skillCoolTime <= 0 ? false : true;
    }

    public void SkillSequence()
    {
        GenerateRandomCoolTime();

        enemyMovement.StopMove();
        animator.SetBool("isReady", true);

        StartCoroutine(TrembleHandler());
    }

    IEnumerator TrembleHandler()
    {
        yield return new WaitForSeconds(trembleTime);
        
        ActivateRandomSkill();
    }

    private void ActivateRandomSkill()
    {
        animator.SetBool("isReady", false);


        int randomNumber = Random.Range(0, skills.Length);

        if(randomNumber == (int)BossSkill.jump)
        {
            animator.SetInteger("SkillNumber", (int)BossSkill.jump);
        }
        else if(randomNumber == (int)BossSkill.summon)
        {
            animator.SetInteger("SkillNumber", (int)BossSkill.summon);
        }

        skills[randomNumber].ActivateSkill();

        enemyMovement.ResumeMove();
    }

    void GenerateRandomCoolTime()
    {
        skillCoolTime = Random.Range(3f , 10f);
    }
}
