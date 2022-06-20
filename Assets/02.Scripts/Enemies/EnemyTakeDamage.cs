using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private EnemyInfo enemyInfo;
    private Rigidbody2D rigid;
    private SpriteRenderer skinnedMeshRenderer;
    private Color originalColor;
    public GameObject damageText;
    private WaitForSeconds reactTime;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        rigid = GetComponent<Rigidbody2D>();
        skinnedMeshRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = skinnedMeshRenderer.material.color;
        reactTime = new WaitForSeconds(0.1f);
    }
    public void TakeDamage(Transform hitTr, float damage, float knockbackSize)
    {
        Vector2 reactVec = transform.position - hitTr.position;
        enemyInfo.healthPoint -= damage;
        GameObject dText = Instantiate(damageText, hitTr.position + Vector3.up, Quaternion.identity);
        dText.GetComponent<TextPopup>().SetDamage((int)damage);

        StartCoroutine(ProcessForDamage(reactVec,knockbackSize));
    }

    IEnumerator ProcessForDamage(Vector2 reactVec, float knockbackSize)
    {
        yield return ReactForDamage(reactVec, knockbackSize);
        CheckDead();
    }

    IEnumerator ReactForDamage(Vector2 reactVec, float knockbackSize)
    {
        reactVec = reactVec.normalized;
        //reactVec -= transform.forward;
        rigid.AddForce(reactVec * knockbackSize, ForceMode2D.Impulse);

        skinnedMeshRenderer.material.color = Color.red;

        yield return reactTime;

        skinnedMeshRenderer.material.color = originalColor;
        rigid.velocity = Vector2.zero;
    }

    void CheckDead()
    {
        if(enemyInfo.healthPoint <= 0 && !enemyInfo.IsDead)
        {
            enemyInfo.IsDead = true;
        }
    }
}
