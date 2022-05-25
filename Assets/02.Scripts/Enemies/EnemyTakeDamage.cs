using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private Rigidbody rigid;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Color originalColor;
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
        originalColor = skinnedMeshRenderer.material.color;
    }
    public void TakeDamage(Transform hitTr, float damage, float knockbackSize)
    {
        Vector3 reactVec = transform.position - hitTr.position;
        enemyInfo.healthPoint -= damage;
        GameObject dText = Instantiate(damageText, hitTr.position + Vector3.up, hitTr.rotation);
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

        skinnedMeshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        skinnedMeshRenderer.material.color = originalColor;
    }

    void CheckDead()
    {
        if(enemyInfo.healthPoint <= 0)
        {
            enemyInfo.IsDead = true;
        }
    }
}
