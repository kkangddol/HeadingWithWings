using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Summon : MonoBehaviour, IBoss_Skill
{
    EnemyInfo enemyInfo;
    Boss_Skill_Manager skillManager;
    IEnemyStopHandler stopHandler;

    public GameObject summon;
    public GameObject effect;
    bool isEffect = false;
    public AudioClip[] audioClips;

    private void Start() {
        enemyInfo = GetComponent<EnemyInfo>();
        stopHandler = GetComponent<IEnemyStopHandler>();
        skillManager = GetComponent<Boss_Skill_Manager>();
    }   
    public void ActivateSkill()
    {
        stopHandler.StopMove();

        GameObject newEffect = Instantiate(effect, transform.position + Vector3.down * 2 , Quaternion.identity);
        Instantiate(summon, transform.position + transform.right, Quaternion.identity);
        Instantiate(summon, transform.position - transform.right, Quaternion.identity);
        Destroy(newEffect, 1f);

        Boss_Skill_Manager.isSkillEnd = true;
        skillManager.audioSource.PlayOneShot(audioClips[0]);
        Boss_Skill_Manager.animator.SetTrigger("reset");
        Invoke("ResumeMove", 1f);
    }

    void ResumeMove()
    {
        stopHandler.ResumeMove();
    }
}
