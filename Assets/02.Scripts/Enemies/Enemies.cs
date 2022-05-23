using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemies : MonoBehaviour
{
    const string PLAYER = "PLAYER";
    private NavMeshAgent agent;
    private Animator animator;
    public int healthPoint;
    public float enemyDamage;
    private Transform targetTransform;
    private Rigidbody rigid;
    SkinnedMeshRenderer skinnedMeshRenderer;

    public GameObject damageText;

    private bool isDead;



    void Start()
    {
        Initialize();
        StartCoroutine(EnemySetDestination());
    }

    void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        targetTransform = GameObject.FindGameObjectWithTag(PLAYER).GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        isDead = false;
    }

    IEnumerator EnemySetDestination()
    {
        while(!isDead)
        {
            yield return null;
            agent.SetDestination(targetTransform.position);
        }
    }

    public void TakeDamage(Transform hitTr, int damage, int knockbackSize)
    {
        Vector3 reactVec = transform.position - hitTr.position;
        healthPoint -= damage;
        GameObject dText = Instantiate(damageText, hitTr.position, hitTr.rotation);
        dText.GetComponent<TextPopup>().SetDamage((int)damage);

        StartCoroutine(ProcessForDamage(reactVec,knockbackSize));
    }

    IEnumerator ProcessForDamage(Vector3 reactVec, int knockbackSize)
    {
        yield return ReactForDamage(reactVec, knockbackSize);
        CheckDead();
    }

    IEnumerator ReactForDamage(Vector3 reactVec, int knockbackSize)
    {
        reactVec = reactVec.normalized;
        reactVec -= transform.forward;
        rigid.AddForce(reactVec * knockbackSize, ForceMode.Impulse);

        skinnedMeshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        skinnedMeshRenderer.material.color = Color.white;
    }

    void CheckDead()
    {
        if(healthPoint <= 0)
        {
            isDead = true;
            enemyDamage = 0;
            agent.isStopped = true;
            skinnedMeshRenderer.material.color = Color.black;
            ///EnemyPrototypePool.ReturnObject(this);
            Destroy(gameObject, 0.1f);
        }
    }

}
