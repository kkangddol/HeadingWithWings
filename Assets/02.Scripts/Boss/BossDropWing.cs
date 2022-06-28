using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropWing : MonoBehaviour
{
    public GameObject heightItemPrefab;
    public GameObject healItemPrefab;
    public void DropWing()
    {
        float heightItemDropRate = GameManager.Instance.heightItemDropRate;
        float healItemDropRate = GameManager.Instance.healItemDropRate;

        float randomX;

        float randomNumber = Random.Range(0f,100f);
        if(randomNumber < heightItemDropRate)
        {
            randomX = Random.Range(-1f, 1f);
            GameObject item = Instantiate(heightItemPrefab, transform.position + (Vector3.right * randomX), Quaternion.identity);
        }

        randomNumber = Random.Range(0f,100f);
        if(randomNumber < healItemDropRate)
        {
            randomX = Random.Range(-1f, 1f);
            GameObject item = Instantiate(healItemPrefab, transform.position + (Vector3.right * randomX), Quaternion.identity);
        }
    }
}
