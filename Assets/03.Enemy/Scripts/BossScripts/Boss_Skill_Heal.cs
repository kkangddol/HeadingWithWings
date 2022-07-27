using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Heal : MonoBehaviour, IBoss_Skill
{
    //스스로의 체력을 회복합니다.
    Boss_Skill_Manager skillManager;
    public Transform effectPrefab;
    private EnemyInfo enemyInfo = null;
    private Vector3 offset = Vector3.down * 0.5f;
    public AudioClip[] audioClips;
    private void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();

        ActivateSkill();
    }

    public void ActivateSkill()
    {
        Boss_Skill_Manager.animator.SetTrigger("reset");

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
    }
}
