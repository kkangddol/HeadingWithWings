using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private Rigidbody rigid;
    SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject damageText;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        rigid = GetComponent<Rigidbody>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    public void TakeDamage(Transform hitTr, int damage, float knockbackSize)
    {
        Vector3 reactVec = transform.position - hitTr.position;
        enemyInfo.healthPoint -= damage;
        GameObject dText = Instantiate(damageText, hitTr.position, hitTr.rotation);
        dText.GetComponent<TextPopup>().SetDamage((int)damage);

        StartCoroutine(ProcessForDamage(reactVec,knockbackSize));
    }

    IEnumerator ProcessForDamage(Vector3 reactVec, float knockbackSize)
    {
        yield return ReactForDamage(reactVec, knockbackSize);
        CheckDead();
    }

    IEnumerator ReactForDamage(Vector3 reactVec, float knockbackSize)
    {
        reactVec = reactVec.normalized;
        reactVec -= transform.forward;
        rigid.AddForce(reactVec * knockbackSize, ForceMode.Impulse);

        Color tempColor = skinnedMeshRenderer.material.color;

        skinnedMeshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        skinnedMeshRenderer.material.color = tempColor;
    }

    void CheckDead()
    {
        if(enemyInfo.healthPoint <= 0)
        {
            enemyInfo.IsDead = true;
        }
    }
}
