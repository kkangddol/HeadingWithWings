using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPrototype : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public float hp;
    public float damage;
    public int knockbackSize = 5;

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



    // 사실 이 부분 자체를 투사체로 옮겨야함 피격자는 뭐가 날 때리는지 몰라야함
    // 그냥 쳐맞는거임
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ATTACK")
        {
            // 하얗게 1회 번쩍이면서 -> animator 활용?
            // 뒤로 넉백 -> 움직임 잠시 멈추고 뒤로 밀어야함
            // 넉백은 공격의 넉백 수치에 따라 다름
            Vector3 reactVec = transform.position - other.transform.position;
            int damage = (int)other.GetComponent<BulletController>().damage;
            hp -= damage;
            GameObject dText = Instantiate(damageText, other.transform.position, other.transform.rotation);
            dText.GetComponentInChildren<DamageText>().damage = damage;

            // 이 데미지값을 이용해서 텍스트 표시 호출
            

            StartCoroutine(ReactForAttack(reactVec));
            Destroy(other.gameObject);
        }
    }

    IEnumerator ReactForAttack(Vector3 reactVec)
    {
        if(hp <= 0)
        {
            damage = 0;
            agent.SetDestination(transform.position);
            skinnedMeshRenderer.material.color = Color.black;
            Destroy(gameObject,0.2f);
            yield return null;
        }
        reactVec = reactVec.normalized;
        reactVec -= transform.forward;
        rigid.AddForce(reactVec * knockbackSize, ForceMode.Impulse);

        skinnedMeshRenderer.material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        skinnedMeshRenderer.material.color = Color.white;


    }
}
