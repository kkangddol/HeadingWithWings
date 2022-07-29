using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject textObject;
    [SerializeField] private Transform textParent;
    Queue<GameObject> textQueue = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        Initialize(500);
    }

    private void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++)
        {
            textQueue.Enqueue(CreateNewObject(textObject, textParent));
        }
    }

    private GameObject CreateNewObject(GameObject obj, Transform parent)
    {
        var newObj = Instantiate(obj, parent);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public GameObject GetTextObject()
    {
        if(Instance.textQueue.Count > 0)
        {
            var obj = Instance.textQueue.Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            obj.GetComponent<TextPopup>().Init();
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(textObject, textParent);
            newObj.SetActive(true);
            newObj.transform.SetParent(null);
            newObj.GetComponent<TextPopup>().Init();
            return newObj;
        }
    }

    public void ReturnTextObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(textParent);
        Instance.textQueue.Enqueue(obj);
        obj.transform.position.Set(0, 0, 0);
    }

}
