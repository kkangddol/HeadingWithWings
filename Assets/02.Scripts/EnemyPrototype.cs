using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPrototype : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public float hp;
    public float enemyDamage;


    private Transform targetTransform;
    private Rigidbody rigid;
    SkinnedMeshRenderer skinnedMeshRenderer;

    public GameObject damageText;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetTransform = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    private void Update()
    {
        agent.SetDestination(targetTransform.position);
    }

    public void TakeDamage(Transform hitTr, int damage, int knockbackSize){
        Vector3 reactVec = transform.position - hitTr.position;

        hp -= damage;
        GameObject dText = Instantiate(damageText, hitTr.position, hitTr.rotation);
        dText.GetComponent<TextPopup>().SetDamage((int)damage);

        StartCoroutine(ReactForAttack(reactVec,knockbackSize));
    }
    
    IEnumerator ReactForAttack(Vector3 reactVec, int knockbackSize)
    {
        if(hp <= 0)
        {
            enemyDamage = 0;
            //agent.SetDestination(transform.position);
            agent.isStopped = true;
            skinnedMeshRenderer.material.color = Color.black;
            //Destroy(gameObject,0.2f);
            yield return new WaitForSeconds(.1f);
            EnemyPrototypePool.ReturnObject(this);
        }
        reactVec = reactVec.normalized;
        reactVec -= transform.forward;
        rigid.AddForce(reactVec * knockbackSize, ForceMode.Impulse);

        skinnedMeshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        skinnedMeshRenderer.material.color = Color.white;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            other.GetComponent<PlayerTakeDamage>().TakeDamage(enemyDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PLAYER")
        {
            other.GetComponent<PlayerTakeDamage>().EndTakeDamage();
        }
    }
}
