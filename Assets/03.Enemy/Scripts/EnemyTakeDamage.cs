using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour, ITakeBossAttack
{
    private EnemyInfo enemyInfo;
    private Rigidbody2D rigid;
    private SpriteRenderer skinnedMeshRenderer;
    private Color originalColor;
    public GameObject damageText;
    private float reactTime;
    bool isHit;
    AudioSource audioSource;
    public AudioClip[] audioClips;

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
        reactTime = 0.1f;
        isHit = false;
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(audioClips[0]);
        reactVec = reactVec.normalized;
        rigid.AddForce(reactVec * knockbackSize, ForceMode2D.Impulse);
        skinnedMeshRenderer.material.color = Color.red;

        Invoke("endReact", reactTime);
    }

    void endReact()
    {
        skinnedMeshRenderer.material.color = originalColor;

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

    public void TakeBossAttack(Transform hitTr, float damage, float knockbackSize)
    {
        TakeDamage(hitTr, damage, knockbackSize);
    }
}
