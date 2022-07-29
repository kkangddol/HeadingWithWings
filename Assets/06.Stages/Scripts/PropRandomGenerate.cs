using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomGenerate : MonoBehaviour
{
    public GameObject[] naturePrefabs;
    public int offsetX;
    public float tempGenSize;
    public float tempGenRatio;
    int propCount;

    private void Awake()
    {
        propCount = (int)((tempGenSize*tempGenSize) * (tempGenRatio/100));
        for(int i = 0; i < propCount; i++)
        {
            GenerateRandomNature();
        }

        //StartCoroutine("Work");
    }

    IEnumerator Work()
    {
        int index = 0;


        while(index < propCount)
        {
            yield return null;
            GenerateRandomNature();
            index++;
        }
    }

    private void GenerateRandomNature()
    {
        int randomPrefabIndex = (int)Random.Range(0,naturePrefabs.Length);
        float randomX = Random.Range(-tempGenSize - offsetX,tempGenSize + offsetX);
        float randomY = Random.Range(-tempGenSize,tempGenSize);
        GameObject newPrefabs = Instantiate(naturePrefabs[randomPrefabIndex], new Vector3(randomX, randomY, 0), Quaternion.identity);
        newPrefabs.transform.SetParent(transform);
    }
}
