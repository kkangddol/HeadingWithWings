using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBase : MonoBehaviour
{
    public int initCount = 50;

    [SerializeField] private GameObject poolingObject;
    private Transform poolParent;
    Queue<GameObject> poolQueue = new Queue<GameObject>();

    private void Start()
    {
        Initialize(initCount);
    }

    private void Initialize(int count)
    {
        poolParent = GetComponent<Transform>(); // Self
        for (int i = 0; i < count; i++)
        {
            poolQueue.Enqueue(CreateNewObject(poolingObject, poolParent));
        }
    }

    private GameObject CreateNewObject(GameObject obj, Transform parent)
    {
        var newObj = Instantiate(obj, parent);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            var obj = poolQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject(poolingObject, poolParent);
            newObj.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(poolParent);
        poolQueue.Enqueue(obj);
        obj.transform.position.Set(0, 0, 0);
        obj.SetActive(false);
    }

    public void ReturnObject(GameObject obj, float sec)
    {
        StartCoroutine(ReturnDelay(obj, sec));
    }

    IEnumerator ReturnDelay(GameObject obj, float sec)
    {
        yield return new WaitForSeconds(sec);

        obj.transform.SetParent(poolParent);
        poolQueue.Enqueue(obj);
        obj.transform.position.Set(0, 0, 0);
        obj.SetActive(false);
    }
}
