using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject heightItemPrefab;
    public GameObject healItemPrefab;
    public void DropItem()
    {
        float heightItemDropRate = GameManager.Instance.heightItemDropRate;
        float healItemDropRate = GameManager.Instance.healItemDropRate;

        int randomNumber = Random.Range(0,100);
        if(randomNumber <= heightItemDropRate)
        {
            Instantiate(heightItemPrefab,transform.position,transform.rotation);
        }

        randomNumber = Random.Range(0,100);
        if(randomNumber <= healItemDropRate)
        {
            Instantiate(healItemPrefab,transform.position,transform.rotation);
        }
    }
}
