using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Heal : MonoBehaviour, IBoss_Skill
{
    //스스로의 체력을 회복합니다.
    Boss_Skill_Manager skillManager;
    public Transform effectPrefab;
    private EnemyInfo enemyInfo = null;
    private Vector3 offset = Vector3.zero;
    public AudioClip[] audioClips;
    private void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        offset = new Vector3(0f, -0.4f, 0f) * this.transform.localScale.x;
    }

    public void ActivateSkill()
    {
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        skillManager.audioSource.PlayOneShot(audioClips[0]);
        Destroy(Instantiate(effectPrefab, this.transform.position + offset, Quaternion.identity, this.transform).gameObject, 5.5f);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(1.25f);
            enemyInfo.SumHealthPoint(125);
        }

        Boss_Skill_Manager.isSkillEnd = true;
        Boss_Skill_Manager.animator.SetTrigger("reset");
    }
}
