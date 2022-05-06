using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<BulletController> poolingObjectQueue = new Queue<BulletController>();

    private void Awake()
    {
        Instance = this;

        Initialize(3);
    }

    private void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

//enemyprototype 형식 추상화해서 바꿀것
    private BulletController CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<BulletController>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }


//생성된 오브젝트 관리하는 오브젝트 하나 만들어서 부모로 사용할것?
    public static BulletController GetObject()
    {
        if(Instance.poolingObjectQueue.Count > 0)
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

    public static void ReturnObject(BulletController obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
        obj.transform.position.Set(0, 0, 0);
    }

}
