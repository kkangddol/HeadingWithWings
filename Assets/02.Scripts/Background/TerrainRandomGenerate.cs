using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainRandomGenerate : MonoBehaviour
{
    public GameObject[] naturePrefabs;
    public float tempGenSize = 50;
    public float tempGenRatio = 50;
    int repeat = 200;

    private void Awake()
    {
        repeat = (int)((tempGenSize*tempGenSize) * (tempGenRatio/100));
        for(int i = 0; i < repeat; i++)
        {
            GenerateRandomNature();
        }

        //StartCoroutine("Work");
    }

    IEnumerator Work()
    {
        int index = 0;


        while(index < repeat)
        {
            yield return null;
            GenerateRandomNature();
            index++;
        }
    }

    private void GenerateRandomNature()
    {
        int randomPrefabIndex = (int)Random.Range(0,naturePrefabs.Length);
        float randomX = Random.Range(-tempGenSize,tempGenSize);
        float randomZ = Random.Range(-tempGenSize,tempGenSize);
        GameObject newPrefabs = Instantiate(naturePrefabs[randomPrefabIndex], new Vector3(randomX,-10,randomZ), Quaternion.identity);
        newPrefabs.transform.SetParent(transform);
    }
}
