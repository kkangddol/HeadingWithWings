using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPool : MonoBehaviour
{
    public static MeteorPool Instance;
    public GameObject meteorPrefab;
    Queue<Meteor> poolingObjectQueue = new Queue<Meteor>();

    private void Start()
    {
        Instance = this;
    }

    public void Initialize(int initCount)
    {
        if(meteorPrefab == null)
        {
            Debug.Log("Meteor Prefab is None!");
            return;
        }

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
        obj.transform.rotation.Set(0, 0, 0, 0);
    }
}
