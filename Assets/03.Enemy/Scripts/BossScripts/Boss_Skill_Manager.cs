using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Manager : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    public static Animator animator;
    float trembleTime = 2f;

    IBoss_Skill[] skills;
    IEnemyStopHandler stopHandler;

    public float skillCoolTime;
    public float skillDamage;
    public float skillKnockBackSize;
    public float skillProjectileDamage;
    public float skillProjectileSpeed;
    bool isCoolTime = true;

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    
    public static bool isSkillEnd = true;

    private void Awake() {
        enemyInfo = GetComponent<EnemyInfo>();
        animator = GetComponentInChildren<Animator>();
        skills = GetComponents<IBoss_Skill>();
        stopHandler = GetComponent<IEnemyStopHandler>();
        skillCoolTime = Random.Range(0, 5f);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if(!isCoolTime)
        {
            SkillSequence();
        }

        if(isSkillEnd)
        {
            skillCoolTime -= Time.deltaTime;

            isCoolTime = skillCoolTime <= 0 ? false : true;
        }
    }

    public void DataInit()
    {
        skillCoolTime = GameManager.Data.MonsterDict[enemyInfo.MonsterID].skillCoolTime;
        skillDamage = GameManager.Data.MonsterDict[enemyInfo.MonsterID].skillDamage;
        skillKnockBackSize = GameManager.Data.MonsterDict[enemyInfo.MonsterID].skillKnockBackSize;
        skillProjectileDamage = GameManager.Data.MonsterDict[enemyInfo.MonsterID].skillProjectileDamage;
        skillProjectileSpeed = GameManager.Data.MonsterDict[enemyInfo.MonsterID].skillProjectileSpeed;
    }

    public void SkillSequence()
    {
        GenerateRandomCoolTime();
        isSkillEnd = false;

        stopHandler.StopMove();
        animator.SetBool("isReady", true);

        StartCoroutine(TrembleHandler());
    }

    IEnumerator TrembleHandler()
    {
        audioSource.PlayOneShot(audioClips[0]);
        yield return new WaitForSeconds(trembleTime);
        audioSource.Stop();
        ActivateRandomSkill();
    }

    private void ActivateRandomSkill()
    {
        animator.SetBool("isReady", false);

        int randomNumber = Random.Range(0, skills.Length);
        animator.SetInteger("SkillNumber", randomNumber);
        skills[randomNumber].ActivateSkill();

        stopHandler.ResumeMove();
    }

    void GenerateRandomCoolTime()
    {
        isCoolTime = true;
        skillCoolTime = Random.Range(3f , 8f);
    }
}