using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : MonoBehaviour
{
    public int meteorAmount = 10;
    private float meteorSpeed = 30f;
    private float meteorHeight = 10;
    private float meteorRangeRadius = 3f;

    private void Start()
    {
        MeteorPool.Instance.Initialize(meteorAmount);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            StartCoroutine(StartMeteor());
    }

    IEnumerator StartMeteor()
    {
        if (MeteorPool.Instance.meteorPrefab == null)
        {
            yield return null;
        }
        for(int i = 0; i < meteorAmount; i++)
        {
            Meteor meteor = MeteorPool.GetObject();

            meteor.transform.position = Position.GetRandomInCircle(transform.position, meteorRangeRadius) + Vector3.up * meteorHeight;
            meteor.transform.LookAt(Position.GetRandomInCircle(transform.position, meteorRangeRadius));
            Debug.Log($"MeteorLocal:{meteor.transform.localPosition}");
            Debug.Log($"MeteorWorld:{meteor.transform.position}");
            meteor.GetComponent<Rigidbody>().AddForce(meteor.transform.forward * meteorSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
