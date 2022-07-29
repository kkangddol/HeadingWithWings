using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject silverPrefab;
    public GameObject goldPrefab;
    public GameObject healItemPrefab;
    Transform itemParent;
    private void Start() {
        itemParent = GameObject.FindWithTag("ITEMPARENT").GetComponent<Transform>();
    }

    public void DropItem()
    {
        float silverDropRate = GameManager.Instance.heightSilverDropRate;
        float goldDropRate = GameManager.Instance.heightGoldDropRate;
        float healItemDropRate = GameManager.Instance.healItemDropRate;
        float nothingDropRate = GameManager.Instance.nothingDropRate;

        float randomX = Random.Range(-1f, 1f);

        float[] probs = {silverDropRate, goldDropRate, healItemDropRate, nothingDropRate};

        switch(Choose(probs))
        {
            case 0:
            {
                GameObject item = Instantiate(silverPrefab, transform.position + (Vector3.right * randomX), Quaternion.identity, itemParent);
                break;
            }
            case 1:
            {
                GameObject item = Instantiate(goldPrefab, transform.position + (Vector3.right * randomX), Quaternion.identity, itemParent);
                break;
            }
            case 2:
            {
                GameObject item = Instantiate(healItemPrefab, transform.position + (Vector3.right * randomX), Quaternion.identity, itemParent);
                break;
            }
            case 3:
                break;
        }
    }

    int Choose (float[] probs) {

        float total = 0;

        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i= 0; i < probs.Length; i++) {
            if (randomPoint < probs[i]) {
                return i;
            }
            else {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
