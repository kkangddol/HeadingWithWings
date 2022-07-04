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
    private float reactTime;
    bool isHit;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if(!isHit) rigid.velocity = Vector2.zero;
    }

    private void Initialize()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        rigid = GetComponent<Rigidbody2D>();
        skinnedMeshRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = skinnedMeshRenderer.material.color;
        reactTime = 0.1f;
        isHit = false;
    }
    public void TakeDamage(Transform hitTr, float damage, float knockbackSize)
    {
        isHit = true;

        enemyInfo.healthPoint -= damage;
        float randomX = Random.Range(-0.5f,0.5f);
        GameObject dText = Instantiate(damageText, transform.position + (Vector3.up / 2) + (Vector3.right * randomX), Quaternion.identity);
        dText.GetComponent<TextPopup>().SetDamage((int)damage);

        Vector2 reactVec = transform.position - hitTr.position;

        ReactForDamage(reactVec, knockbackSize);
    }

    void ReactForDamage(Vector2 reactVec, float knockbackSize)
    {
        rigid.velocity = Vector2.zero;
        reactVec = reactVec.normalized;
        rigid.AddForce(reactVec * knockbackSize, ForceMode2D.Impulse);
        skinnedMeshRenderer.material.color = Color.red;

        Invoke("endReact", reactTime);
    }

    void endReact()
    {
        skinnedMeshRenderer.material.color = originalColor;
        rigid.velocity = Vector2.zero;

        CheckDead();
        isHit = false;
    }

    void CheckDead()
    {
        if(enemyInfo.healthPoint <= 0 && !enemyInfo.IsDead)
        {
            enemyInfo.IsDead = true;
        }
    }
}
