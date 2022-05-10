using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : MonoBehaviour
{
    public GameObject meteorPrefab;

    [SerializeField]
    private float meteorSpeed = 1000f;
    private float meteorHeight = 10;
    private float meteorAttackRadius = 5f;

    IEnumerator StartMeteor()
    {
        if (meteorPrefab == null)
        {
            yield break;
        }
        while(true)
        {
            GameObject meteor = GameObject.Instantiate(meteorPrefab);
            meteor.transform.position = Position.GetRandomInCircle(transform.position, meteorAttackRadius) + Vector3.up * meteorHeight;
            meteor.transform.LookAt(Position.GetRandomInCircle(transform.position, meteorAttackRadius));
            meteor.GetComponent<Rigidbody>().AddForce(meteor.transform.forward * meteorSpeed, ForceMode.Acceleration);

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator OneCycleMeteor()
    {
        StartCoroutine(StartMeteor());
        yield return new WaitForSeconds(3f);
    }

    private void Start()
    {
        StartCoroutine(OneCycleMeteor());
    }

    void Update()
    {
        
    }
}
