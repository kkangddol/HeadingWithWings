using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorAttack : MonoBehaviour
{
    public static MeteorAttack Instance;

    Queue<Meteor> poolingObjectQueue = new Queue<Meteor>();

    public GameObject meteorPrefab;

    public int meteorAmount = 10;
    private float meteorSpeed = 30f;
    private float meteorHeight = 10;
    private float meteorAttackRadius = 5f;

    private void Start()
    {
        Instance = this;

        Initialize(meteorAmount);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }
    private Meteor CreateNewObject()
    {
        var newObj = Instantiate(meteorPrefab).GetComponent<Meteor>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }
    public static Meteor GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Meteor obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
        obj.transform.position.Set(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            StartCoroutine(StartMeteor());
    }

    IEnumerator StartMeteor()
    {
        if (meteorPrefab == null)
        {
            yield return null;
        }
        for(int i = 0; i < meteorAmount; i++)
        {
            Meteor meteor = GetObject();
            meteor.transform.position = Position.GetRandomInCircle(transform.position, meteorAttackRadius) + Vector3.up * meteorHeight;
            meteor.transform.LookAt(Position.GetRandomInCircle(transform.position, meteorAttackRadius));
            meteor.GetComponent<Rigidbody>().AddForce(meteor.transform.forward * meteorSpeed, ForceMode.Impulse);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
