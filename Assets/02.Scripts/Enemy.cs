using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
    public float hp;
    public float damage;

    private Transform targetTransform;
    private Rigidbody rigid;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetTransform = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        agent.destination = targetTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ATTACK")
        {
            // 하얗게 1회 번쩍이면서 -> animator 활용?
            // 뒤로 넉백 -> 움직임 잠시 멈추고 뒤로 밀어야함
            // 넉백은 공격의 넉백 수치에 따라 다름
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        yield return null;
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;

        rigid.AddForce(reactVec);
        
    }
}
